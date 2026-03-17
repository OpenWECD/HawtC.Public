//**********************************************************************************************************************************
//LICENSING
// Copyright(C) 2021, 2025  TG Team,Key Laboratory of Jiangsu province High-Tech design of wind turbine,WTG,WL,赵子祯
//
//    This file is part of OpenWECD.HydroL
//
// Licensed under the Boost Software License - Version 1.0 - August 17th, 2003
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.HawtC.cn/licenses.txt
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT
// SHALL THE COPYRIGHT HOLDERS OR ANYONE DISTRIBUTING THE SOFTWARE BE LIABLE
// FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//
// HydroL 模块初始化子程序
// 负责根据输入参数初始化 HydroL 计算所需的所有结构体和波浪数据。
//
//**********************************************************************************************************************************

using OpenWECD.HydroL.WaveL;
using OpenWECD.IO.Log;
using System;

namespace OpenWECD.HydroL
{
    /// <summary>
    /// HydroL 模块初始化子程序
    /// </summary>
    public static class HydroL_INI
    {
        /// <summary>
        /// 初始化 HydroL_AllOuts 结构体（按输出节点数量分配内存）
        /// </summary>
        public static HydroL_AllOuts HDR_INIAllOuts(HydroL1 p)
        {
            int NSparOut = p.NSparOutNdnum;
            var allOuts = new HydroL_AllOuts
            {
                SpnSpaFxi = new double[NSparOut],
                SpnSpaFyi = new double[NSparOut],
                SpnSpaFzi = new double[NSparOut],
                SpnSpaMxi = new double[NSparOut],
                SpnSpaMyi = new double[NSparOut],
                SpnSpaMzi = new double[NSparOut],
            };
            return allOuts;
        }

        /// <summary>
        /// 初始化 HydroL 模块运行时参数结构体
        /// </summary>
        public static HydroL_ParameterType HDR_INIParameterType(HydroL1 p)
        {
            var param = new HydroL_ParameterType
            {
                WtrDens  = p.WtrDens,
                WtrDpth  = p.WtrDpth,
                GRAVACC  = p.GRAVACC,
                NMembers = p.NJoints > 1 ? p.NJoints - 1 : 0,
            };
            return param;
        }

        /// <summary>
        /// 根据 HydroL1 输入参数构建 Spar 平台的节点信息数组
        /// 将节点坐标与截面属性合并，为 Morison 方程准备输入。
        /// 默认 Cd=1.0，Cm=2.0（可在此处从外部输入或查表扩展）
        /// </summary>
        public static SparMemberNode[] BuildSparNodes(HydroL1 p,
                                                        double Cd = 1.0,
                                                        double Cm = 2.0)
        {
            int N = p.NJoints;
            if (N <= 0) return Array.Empty<SparMemberNode>();

            var nodes = new SparMemberNode[N];
            for (int i = 0; i < N; i++)
            {
                // 查找轴向系数（根据 JointAxID）
                int axID  = p.JointAxID[i];
                double axCd = 0.0, axCa = 0.0, axCp = 1.0;
                if (p.AxCoefID != null)
                {
                    for (int k = 0; k < p.NAxCoef; k++)
                    {
                        if (p.AxCoefID[k] == axID)
                        {
                            axCd = p.AxCd[k];
                            axCa = p.AxCa[k];
                            axCp = p.AxCp[k];
                            break;
                        }
                    }
                }

                // 查找截面直径（使用相邻节点对应的属性，简化为按轴向系数 ID 对应 PropSetID）
                double diam = 0.0;
                if (p.PropD != null)
                {
                    int propIdx = Math.Min(axID - 1, p.NPropSets - 1);
                    if (propIdx >= 0) diam = p.PropD[propIdx];
                }

                nodes[i] = new SparMemberNode
                {
                    z        = p.Jointzi[i] + p.MSL2SWL,
                    Diameter = diam,
                    Area     = Math.PI / 4.0 * diam * diam,
                    Cd       = Cd,
                    Cm       = Cm,
                    Cp       = axCp,
                    AxCd     = axCd,
                    AxCa     = axCa,
                };
            }
            return nodes;
        }

        /// <summary>
        /// 预计算波浪运动学（生成 WaveKinematics 时历数据）
        /// 根据 WaveMod 自动选择规则波或不规则波生成方法。
        /// </summary>
        /// <param name="p">HydroL1 输入参数</param>
        /// <param name="nodes">Spar 平台节点数组（用于确定计算位置的 z 坐标）</param>
        /// <returns>波浪运动学时历</returns>
        public static WaveKinematics HDR_INIWaveKinematics(HydroL1 p, SparMemberNode[] nodes)
        {
            double[] nodeZ = new double[nodes.Length];
            for (int i = 0; i < nodes.Length; i++)
                nodeZ[i] = nodes[i].z;

            return p.WaveMod switch
            {
                WaveMod_Type.None    => GenerateStillWater(p, nodeZ),
                WaveMod_Type.Regular => WaveL_Subs.GenerateRegularWave(p, nodeZ),
                WaveMod_Type.JONSWAP => WaveL_Subs.GenerateIrregularWave(p, nodeZ, WaveMod_Type.JONSWAP),
                WaveMod_Type.PM      => WaveL_Subs.GenerateIrregularWave(p, nodeZ, WaveMod_Type.PM),
                _ => throw new NotSupportedException($"WaveMod {p.WaveMod} not supported"),
            };
        }

        /// <summary>
        /// 生成静水（无波浪）运动学（全零时历）
        /// </summary>
        private static WaveKinematics GenerateStillWater(HydroL1 p, double[] nodeZ)
        {
            int NSteps = (int)(p.WaveTMax / p.WaveDT) + 1;
            int NNodes = nodeZ.Length;
            var wk = new WaveKinematics
            {
                NSteps   = NSteps,
                DT       = p.WaveDT,
                Time     = new double[NSteps],
                WaveElev = new double[NSteps],
                WaveVxi  = new double[NNodes, NSteps],
                WaveVzi  = new double[NNodes, NSteps],
                WaveAxi  = new double[NNodes, NSteps],
                WaveAzi  = new double[NNodes, NSteps],
                DynP     = new double[NNodes, NSteps],
            };
            for (int it = 0; it < NSteps; it++)
                wk.Time[it] = it * p.WaveDT;
            return wk;
        }

        /// <summary>
        /// 构建海流速度插值函数
        /// 使用多项式最小二乘拟合或分段线性插值（由 CurrentPolyOrder 决定）
        /// </summary>
        public static Func<double, double> BuildCurrentVelFunc(HydroL1 p)
        {
            if (p.CurrentCoordinate == null || p.CurrentCoordinate.Length == 0)
                return _ => 0.0;

            var zArr = p.CurrentCoordinate;
            var vArr = p.CurrentVelocity;
            int N = Math.Min(zArr.Length, vArr.Length);
            if (N == 0) return _ => 0.0;
            if (N == 1) return _ => vArr[0];

            // 分段线性插值（安全、精度足够）
            return z =>
            {
                if (z <= zArr[0]) return vArr[0];
                if (z >= zArr[N - 1]) return vArr[N - 1];
                int i = Array.BinarySearch(zArr, 0, N, z);
                if (i >= 0) return vArr[i];
                i = ~i;
                double alpha = (z - zArr[i - 1]) / (zArr[i] - zArr[i - 1]);
                return vArr[i - 1] + alpha * (vArr[i] - vArr[i - 1]);
            };
        }

        /// <summary>
        /// 初始化 HydroL_RtHndSideType（右端项结构体）
        /// </summary>
        public static HydroL_RtHndSideType HDR_INIRtHndSideType(HydroL1 p, HydroL_ParameterType param)
        {
            var rths = new HydroL_RtHndSideType
            {
                HydroL_IO_Out = new HydroL_IO_Outs(p, param),
            };
            return rths;
        }
    }
}
