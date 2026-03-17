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
// Morison 方程计算子程序模块
// 实现了基于 Morison 方程的波浪与海流联合载荷计算，适用于 Spar 型浮式平台。
// Morison 方程：F = ρ·Cm·V·ü + 0.5·ρ·Cd·A·|u-v|·(u-v)
//   第一项：惯性力（Froude-Krylov 力 + 附加质量力）
//   第二项：拖曳力（速度平方项）
//
//**********************************************************************************************************************************

using System;
using OpenWECD.HydroL.WaveL;

namespace OpenWECD.HydroL.Morison
{
    /// <summary>
    /// Morison 方程计算子程序
    /// </summary>
    public static class Morison_Subs
    {
        /// <summary>
        /// 计算单个圆柱构件微元上的 Morison 方程水平载荷（单位长度力，N/m）
        /// F_x = ρ·Cm·(π/4·D²)·ü_w + 0.5·ρ·Cd·D·|u_w - v_b|·(u_w - v_b)
        /// </summary>
        /// <param name="rho">水的密度 (kg/m^3)</param>
        /// <param name="Cd">拖曳力系数</param>
        /// <param name="Cm">惯性力系数 (= 1 + Ca，附加质量系数 Ca)</param>
        /// <param name="D">构件直径 (m)</param>
        /// <param name="u_w">水质点水平速度 (m/s)</param>
        /// <param name="a_w">水质点水平加速度 (m/s^2)</param>
        /// <param name="v_b">构件（平台）水平速度 (m/s)</param>
        /// <param name="a_b">构件（平台）水平加速度 (m/s^2)</param>
        /// <returns>单位长度水平力 (N/m)</returns>
        public static double MorisonHorizontalForce(
            double rho, double Cd, double Cm, double D,
            double u_w, double a_w, double v_b, double a_b)
        {
            double A_sec = Math.PI / 4.0 * D * D;  // 截面积 (m^2)
            double rel_vel = u_w - v_b;
            double rel_acc = a_w - a_b;

            // 惯性力项（Froude-Krylov + 附加质量）
            double F_inertia = rho * Cm * A_sec * a_w
                               - rho * (Cm - 1.0) * A_sec * a_b;
            // 拖曳力项
            double F_drag = 0.5 * rho * Cd * D * Math.Abs(rel_vel) * rel_vel;

            return F_inertia + F_drag;
        }

        /// <summary>
        /// 计算单个圆柱构件微元上的 Morison 方程垂向载荷（单位长度力，N/m）
        /// </summary>
        public static double MorisonVerticalForce(
            double rho, double Cd, double Cm, double D,
            double u_w, double a_w, double v_b, double a_b)
        {
            // 竖向与水平向形式相同，使用相同的 Cm, Cd
            return MorisonHorizontalForce(rho, Cd, Cm, D, u_w, a_w, v_b, a_b);
        }

        /// <summary>
        /// 对 Spar 平台所有构件节点积分，计算总水动力载荷（作用于参考点的6自由度力/力矩向量）
        /// 使用梯形积分法（沿构件轴向积分）
        /// </summary>
        /// <param name="p">HydroL 参数</param>
        /// <param name="nodes">Spar 构件节点信息数组</param>
        /// <param name="wk">波浪运动学时历</param>
        /// <param name="t">当前时刻 (s)</param>
        /// <param name="platformDisp">平台6自由度位移 [surge,sway,heave,roll,pitch,yaw] (m,m,m,rad,rad,rad)</param>
        /// <param name="platformVel">平台6自由度速度</param>
        /// <param name="platformAcc">平台6自由度加速度</param>
        /// <param name="currentVelAtZ">获取指定深度z处海流速度的函数 (m/s)</param>
        /// <returns>合力向量 [Fx, Fy, Fz, Mx, My, Mz] (N, N-m)</returns>
        public static double[] IntegrateMorisonLoads(
            HydroL1 p,
            SparMemberNode[] nodes,
            WaveKinematics wk,
            double t,
            double[] platformDisp,
            double[] platformVel,
            double[] platformAcc,
            Func<double, double> currentVelAtZ)
        {
            double rho = p.WtrDens;
            double g = p.GRAVACC;
            double[] F = new double[6];  // [Fx, Fy, Fz, Mx, My, Mz]

            int NNodes = nodes.Length;
            if (NNodes < 2) return F;

            // 沿 Spar 轴（z 轴）按节点积分（梯形法）
            for (int j = 0; j < NNodes - 1; j++)
            {
                // 取相邻两节点
                var n1 = nodes[j];
                var n2 = nodes[j + 1];

                // 微元段长度（沿 z 轴）
                double dz = Math.Abs(n2.z - n1.z);

                // 中点参数（用于梯形积分中点估计）
                double z_mid = 0.5 * (n1.z + n2.z);
                double D_mid = 0.5 * (n1.Diameter + n2.Diameter);
                double Cd_mid = 0.5 * (n1.Cd + n2.Cd);
                double Cm_mid = 0.5 * (n1.Cm + n2.Cm);

                // 插值获取中点处的波浪运动学
                double u_w = InterpolateNodeKin(wk, j, j + 1, 0.5, t, p.ConRead, KinType.Vx);
                double a_w = InterpolateNodeKin(wk, j, j + 1, 0.5, t, p.ConRead, KinType.Ax);
                double w_w = InterpolateNodeKin(wk, j, j + 1, 0.5, t, p.ConRead, KinType.Vz);
                double aw_z = InterpolateNodeKin(wk, j, j + 1, 0.5, t, p.ConRead, KinType.Az);

                // 平台运动引起的节点速度/加速度（简化：仅考虑平动，忽略转动耦合）
                double v_bx = platformVel[0];   // surge 方向速度
                double v_by = platformVel[1];   // sway 方向速度
                double v_bz = platformVel[2];   // heave 方向速度
                double a_bx = platformAcc[0];
                double a_by = platformAcc[1];
                double a_bz = platformAcc[2];

                // 海流速度（仅水平方向）
                double v_curr = currentVelAtZ(z_mid);

                // Morison 方程单位长度力（x 和 z 方向）
                double fx = MorisonHorizontalForce(rho, Cd_mid, Cm_mid, D_mid,
                                                   u_w + v_curr, a_w, v_bx, a_bx);
                // 横向（y 方向）：仅考虑结构运动引起的拖曳（对 Spar 型平台，横向波浪速度为零）
                double fy = MorisonHorizontalForce(rho, Cd_mid, Cm_mid, D_mid,
                                                   0.0, 0.0, v_by, a_by);
                double fz_morison = MorisonVerticalForce(rho, Cd_mid, Cm_mid, D_mid,
                                                          w_w, aw_z, v_bz, a_bz);

                // 静水浮力（仅对水线以下节点）
                double fz_buoy = 0.0;
                if (z_mid < 0.0 && z_mid >= -p.WtrDpth)
                {
                    fz_buoy = rho * g * Math.PI / 4.0 * D_mid * D_mid * n1.Cp;
                }

                double fz = fz_morison + fz_buoy;

                // 积分（梯形法：f*dz）
                F[0] += fx * dz;   // Fx
                F[1] += fy * dz;   // Fy
                F[2] += fz * dz;   // Fz

                // 力矩（相对于参考点 z=0）
                F[3] += fy * dz * (-z_mid);   // Mx = Fy * (-z) （绕 x 轴）
                F[4] += fx * dz * z_mid;       // My = Fx * z  （绕 y 轴）
                // F[5] = 0（轴向力矩）
            }

            return F;
        }

        /// <summary>
        /// 计算各输出节点处的单位长度 Morison 力（用于 SpnSpa* 输出）
        /// </summary>
        public static void ComputeNodeLoads(
            HydroL1 p,
            SparMemberNode[] nodes,
            WaveKinematics wk,
            double t,
            double[] platformVel,
            double[] platformAcc,
            Func<double, double> currentVelAtZ,
            out double[] Fx_nodes,
            out double[] Fy_nodes,
            out double[] Fz_nodes)
        {
            int N = nodes.Length;
            Fx_nodes = new double[N];
            Fy_nodes = new double[N];
            Fz_nodes = new double[N];
            double rho = p.WtrDens;
            double g = p.GRAVACC;

            for (int j = 0; j < N; j++)
            {
                var node = nodes[j];
                double u_w = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveVxi, j, t, p.ConRead);
                double a_w = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveAxi, j, t, p.ConRead);
                double w_w = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveVzi, j, t, p.ConRead);
                double aw_z = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveAzi, j, t, p.ConRead);
                double v_curr = currentVelAtZ(node.z);

                double fx = MorisonHorizontalForce(rho, node.Cd, node.Cm, node.Diameter,
                                                   u_w + v_curr, a_w, platformVel[0], platformAcc[0]);
                // 横向（y 方向）：仅考虑结构运动引起的拖曳（对 Spar 型平台，横向波浪速度为零）
                double fy = MorisonHorizontalForce(rho, node.Cd, node.Cm, node.Diameter,
                                                   0.0, 0.0, platformVel[1], platformAcc[1]);
                double fz_m = MorisonVerticalForce(rho, node.Cd, node.Cm, node.Diameter,
                                                    w_w, aw_z, platformVel[2], platformAcc[2]);
                double fz_b = 0.0;
                if (node.z < 0.0 && node.z >= -p.WtrDpth)
                    fz_b = rho * g * Math.PI / 4.0 * node.Diameter * node.Diameter * node.Cp;

                Fx_nodes[j] = fx;
                Fy_nodes[j] = fy;
                Fz_nodes[j] = fz_m + fz_b;
            }
        }

        // ==================== 辅助函数 ====================

        private enum KinType { Vx, Vz, Ax, Az }

        /// <summary>插值获取构件中间节点（两端节点线性插值）的运动学量</summary>
        private static double InterpolateNodeKin(
            WaveKinematics wk, int j1, int j2, double alpha,
            double t, bool conRead, KinType type)
        {
            double v1, v2;
            switch (type)
            {
                case KinType.Vx:
                    v1 = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveVxi, j1, t, conRead);
                    v2 = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveVxi, j2, t, conRead);
                    break;
                case KinType.Vz:
                    v1 = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveVzi, j1, t, conRead);
                    v2 = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveVzi, j2, t, conRead);
                    break;
                case KinType.Ax:
                    v1 = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveAxi, j1, t, conRead);
                    v2 = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveAxi, j2, t, conRead);
                    break;
                case KinType.Az:
                    v1 = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveAzi, j1, t, conRead);
                    v2 = WaveL.WaveL_Subs.InterpolateWave2D(wk.Time, wk.WaveAzi, j2, t, conRead);
                    break;
                default:
                    return 0.0;
            }
            return v1 + alpha * (v2 - v1);
        }

        /// <summary>
        /// 计算附加质量矩阵（对角项，用于运动方程）
        /// 返回 [ΔM11, ΔM22, ΔM33, ΔM44, ΔM55, ΔM66]（各方向附加质量/惯量）
        /// </summary>
        public static double[] ComputeAddedMassMatrix(
            double rho,
            SparMemberNode[] nodes)
        {
            double[] Ma = new double[6];
            int N = nodes.Length;
            for (int j = 0; j < N - 1; j++)
            {
                var n1 = nodes[j];
                var n2 = nodes[j + 1];
                double dz = Math.Abs(n2.z - n1.z);
                double D_mid = 0.5 * (n1.Diameter + n2.Diameter);
                double Ca_mid = 0.5 * ((n1.Cm - 1.0) + (n2.Cm - 1.0));
                double A_sec = Math.PI / 4.0 * D_mid * D_mid;
                double dm = rho * Ca_mid * A_sec * dz;
                double z_mid = 0.5 * (n1.z + n2.z);
                Ma[0] += dm;                    // surge 附加质量
                Ma[1] += dm;                    // sway  附加质量
                Ma[2] += rho * n1.AxCa * A_sec * dz;  // heave 轴向附加质量
                Ma[3] += dm * z_mid * z_mid;    // roll  附加惯量（近似）
                Ma[4] += dm * z_mid * z_mid;    // pitch 附加惯量（近似）
            }
            return Ma;
        }
    }
}
