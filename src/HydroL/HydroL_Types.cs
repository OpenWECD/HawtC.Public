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
// 该模块文件定义了水动力计算模块HydroL的所有数据类型结构体。
// HydroL实现了基于Morison方程的海上浮式风力机水动力载荷计算，
// 支持Spar平台，以及JONSWAP/PM不规则波浪谱。
//
//**********************************************************************************************************************************

using SourceGeneration.Reflection;
using System.Runtime.InteropServices;

namespace OpenWECD.HydroL
{
    /// <summary>
    /// 水动力计算模型选项
    /// </summary>
    public enum HydroMod_Type
    {
        /// <summary>生成波浪文件 .wave</summary>
        GenWaveFile = 0,
        /// <summary>Spar 平台计算</summary>
        Spar = 1,
    }

    /// <summary>
    /// 系泊系统控制模式
    /// </summary>
    public enum MoorLine_Type
    {
        /// <summary>不使用系泊系统</summary>
        None = 0,
        /// <summary>MoorDyn 系泊系统</summary>
        MoorDyn = 1,
        /// <summary>OpenMoor 系泊系统</summary>
        OpenMoor = 2,
        /// <summary>MoorL 系泊系统</summary>
        MoorL = 3,
    }

    /// <summary>
    /// 波浪模型选项
    /// </summary>
    public enum WaveMod_Type
    {
        /// <summary>无波浪（静水）</summary>
        None = 0,
        /// <summary>规则波（周期性）</summary>
        Regular = 1,
        /// <summary>JONSWAP 不规则波</summary>
        JONSWAP = 2,
        /// <summary>Pierson-Moskowitz 不规则波</summary>
        PM = 3,
    }

    /// <summary>
    /// 势流模型选项
    /// </summary>
    public enum PotMod_Type
    {
        /// <summary>无势流</summary>
        None = 0,
        /// <summary>波浪时域力</summary>
        WaveTimeDomain = 1,
        /// <summary>WAMIT 文件输入</summary>
        WAMIT = 2,
    }

    /// <summary>
    /// HydroL 主输入文件的数据结构
    /// </summary>
    [SourceReflection]
    [StructLayout(LayoutKind.Sequential)]
    public struct HydroL1
    {
        /// <summary>主输入文件路径</summary>
        public string HydroLfilepath;
        /// <summary>主输入文件内容（按行存储）</summary>
        public string[] HydroLData;

        // ==================== 基本控制参数 ====================

        /// <summary>水动力计算功能选项</summary>
        public HydroMod_Type HydroMod;
        /// <summary>系泊系统控制选项</summary>
        public MoorLine_Type MoorLine;

        // ==================== 环境参数 ====================

        /// <summary>水的密度 (kg/m^3)</summary>
        public double WtrDens;
        /// <summary>水深 (m)</summary>
        public double WtrDpth;
        /// <summary>静止水位和平均海平面之间的偏移量 (m)</summary>
        public double MSL2SWL;
        /// <summary>重力加速度 (m/s^2)</summary>
        public double GRAVACC;

        // ==================== 海流参数 ====================

        /// <summary>不同深度位置的坐标数组 (m)</summary>
        public double[] CurrentCoordinate;
        /// <summary>不同深度位置下的流速度数组 (m/s)</summary>
        public double[] CurrentVelocity;
        /// <summary>海流速度插值多项式阶数</summary>
        public int CurrentPolyOrder;

        // ==================== 波浪参数 ====================

        /// <summary>波浪模型选项</summary>
        public WaveMod_Type WaveMod;
        /// <summary>波浪计算时长 (s)</summary>
        public double WaveTMax;
        /// <summary>波浪计算时间步长 (s)</summary>
        public double WaveDT;
        /// <summary>有效波高 (m)</summary>
        public double WaveHs;
        /// <summary>峰值谱周期 (s)</summary>
        public double WaveTp;
        /// <summary>JONSWAP 峰形参数 (-) [0=自动计算]</summary>
        public double WavePkShp;
        /// <summary>波谱低截止频率 (rad/s)</summary>
        public double WvLowCOff;
        /// <summary>波谱高截止频率 (rad/s)</summary>
        public double WvHiCOff;
        /// <summary>波谱频率生成数量 (-)</summary>
        public int WvNumCOff;
        /// <summary>入射波传播方向 (degrees)</summary>
        public double WaveDir;
        /// <summary>波浪随机种子</summary>
        public int WaveSeed;
        /// <summary>是否使用正态分布振幅</summary>
        public bool WaveNDAmp;
        /// <summary>输出波浪文件格式 {1:Matlab, 2:Binary, 3:Excel}</summary>
        public int FileGes;
        /// <summary>波浪数据文件路径（若为DEFAULT则使用上方参数生成）</summary>
        public string WvKinFile;

        // ==================== 系泊线参数 ====================

        /// <summary>调用 MoorLine 的时间步长</summary>
        public double MoorLineDT;
        /// <summary>MoorDyn 输入文件路径</summary>
        public string MoorDynFile;
        /// <summary>OpenMoor 输入文件路径</summary>
        public string OpenMoorFile;

        // ==================== 浮式平台参数 ====================

        /// <summary>势流模型选项</summary>
        public PotMod_Type PotMod;
        /// <summary>是否继续读取超出时长的波浪文件</summary>
        public bool ConRead;
        /// <summary>势流数据文件根路径（WAMIT）</summary>
        public string PotFile;
        /// <summary>WAMIT 输出量纲化特征长度 (m)</summary>
        public double WAMITULEN;
        /// <summary>平台静水排水体积 (m^3)</summary>
        public double PtfmVol0;
        /// <summary>浮力中心在 xt 方向的偏移 (m)</summary>
        public double PtfmCOBxt;
        /// <summary>浮力中心在 yt 方向的偏移 (m)</summary>
        public double PtfmCOByt;

        // ==================== 附加刚度与阻尼 ====================

        /// <summary>附加预应力向量 [6] (N, N-m)</summary>
        public double[] AddF0;
        /// <summary>附加线性刚度矩阵 [6×6] (N/m, N/rad, N-m/m, N-m/rad)</summary>
        public double[,] AddCLin;
        /// <summary>静水力矩阵 [6×6]</summary>
        public double[,] AddCHydr0;
        /// <summary>附加线性阻尼矩阵 [6×6] (N/(m/s), N/(rad/s), N-m/(m/s), N-m/(rad/s))</summary>
        public double[,] AddBLin;

        // ==================== 轴向系数 ====================

        /// <summary>轴向系数数量</summary>
        public int NAxCoef;
        /// <summary>轴向系数 ID</summary>
        public int[] AxCoefID;
        /// <summary>轴向拖曳力系数</summary>
        public double[] AxCd;
        /// <summary>轴向附加质量系数</summary>
        public double[] AxCa;
        /// <summary>轴向压力系数</summary>
        public double[] AxCp;

        // ==================== 构件节点（目前仅支持Spar）====================

        /// <summary>节点数量</summary>
        public int NJoints;
        /// <summary>节点 ID</summary>
        public int[] JointID;
        /// <summary>节点 x 坐标 (m)</summary>
        public double[] Jointxi;
        /// <summary>节点 y 坐标 (m)</summary>
        public double[] Jointyi;
        /// <summary>节点 z 坐标 (m)</summary>
        public double[] Jointzi;
        /// <summary>节点轴向系数 ID</summary>
        public int[] JointAxID;
        /// <summary>节点重叠处理方式</summary>
        public int[] JointOvrlp;

        // ==================== 截面属性 ====================

        /// <summary>截面属性组数量</summary>
        public int NPropSets;
        /// <summary>截面属性 ID</summary>
        public int[] PropSetID;
        /// <summary>构件直径 (m)</summary>
        public double[] PropD;
        /// <summary>构件壁厚 (m)</summary>
        public double[] PropThck;

        // ==================== 输出设置 ====================

        /// <summary>是否生成 HydroL 输出文件</summary>
        public bool SumPrint;
        /// <summary>输出文件目录</summary>
        public string SumPath;
        /// <summary>输出 Spar 节点数量</summary>
        public int NSparOutNdnum;
        /// <summary>输出 Spar 节点编号数组</summary>
        public int[] SparOutNd;
        /// <summary>输出变量列表</summary>
        public string[] Outputs_OutList;
    }

    /// <summary>
    /// HydroL 模块参数（运行时计算参数）
    /// </summary>
    [SourceReflection]
    [StructLayout(LayoutKind.Sequential)]
    public struct HydroL_ParameterType
    {
        /// <summary>水的密度 (kg/m^3)</summary>
        public double WtrDens;
        /// <summary>水深 (m)</summary>
        public double WtrDpth;
        /// <summary>重力加速度 (m/s^2)</summary>
        public double GRAVACC;
        /// <summary>Spar 构件数量</summary>
        public int NMembers;
        /// <summary>各构件的节点数量</summary>
        public int[] MemberNodeNum;
    }

    /// <summary>
    /// HydroL 模块实时计算结果（右端项）
    /// </summary>
    [SourceReflection]
    [StructLayout(LayoutKind.Sequential)]
    public struct HydroL_RtHndSideType
    {
        /// <summary>水动力输出结果</summary>
        public HydroL_IO_Outs HydroL_IO_Out;
    }

    /// <summary>
    /// HydroL 全局输出变量存储结构体
    /// </summary>
    public struct HydroL_AllOuts
    {
        // ==================== 总水动力载荷（作用于参考点）====================

        /// <summary>水动力 x 方向力 (N)</summary>
        public double HydroFxi;
        /// <summary>水动力 y 方向力 (N)</summary>
        public double HydroFyi;
        /// <summary>水动力 z 方向力 (N)</summary>
        public double HydroFzi;
        /// <summary>水动力 x 方向力矩 (N-m)</summary>
        public double HydroMxi;
        /// <summary>水动力 y 方向力矩 (N-m)</summary>
        public double HydroMyi;
        /// <summary>水动力 z 方向力矩 (N-m)</summary>
        public double HydroMzi;

        // ==================== 平台参考点运动（位移）====================

        /// <summary>平台纵荡位移 (m)</summary>
        public double PRPSurge;
        /// <summary>平台横荡位移 (m)</summary>
        public double PRPSway;
        /// <summary>平台垂荡位移 (m)</summary>
        public double PRPHeave;
        /// <summary>平台横摇角 (deg)</summary>
        public double PRPRoll;
        /// <summary>平台纵摇角 (deg)</summary>
        public double PRPPitch;
        /// <summary>平台艏摇角 (deg)</summary>
        public double PRPYaw;

        // ==================== 平台参考点速度 ====================

        /// <summary>平台 x 方向平动速度 (m/s)</summary>
        public double PRPTVxi;
        /// <summary>平台 y 方向平动速度 (m/s)</summary>
        public double PRPTVyi;
        /// <summary>平台 z 方向平动速度 (m/s)</summary>
        public double PRPTVzi;
        /// <summary>平台 x 方向转动速度 (rad/s)</summary>
        public double PRPRVxi;
        /// <summary>平台 y 方向转动速度 (rad/s)</summary>
        public double PRPRVyi;
        /// <summary>平台 z 方向转动速度 (rad/s)</summary>
        public double PRPRVzi;

        // ==================== 平台参考点加速度 ====================

        /// <summary>平台 x 方向平动加速度 (m/s^2)</summary>
        public double PRPTAxi;
        /// <summary>平台 y 方向平动加速度 (m/s^2)</summary>
        public double PRPTAyi;
        /// <summary>平台 z 方向平动加速度 (m/s^2)</summary>
        public double PRPTAzi;
        /// <summary>平台 x 方向转动加速度 (rad/s^2)</summary>
        public double PRPRAxi;
        /// <summary>平台 y 方向转动加速度 (rad/s^2)</summary>
        public double PRPRAyi;
        /// <summary>平台 z 方向转动加速度 (rad/s^2)</summary>
        public double PRPRAzi;

        // ==================== Spar 构件节点载荷 ====================

        /// <summary>Spar 各输出节点 x 方向力 (N/m)</summary>
        public double[] SpnSpaFxi;
        /// <summary>Spar 各输出节点 y 方向力 (N/m)</summary>
        public double[] SpnSpaFyi;
        /// <summary>Spar 各输出节点 z 方向力 (N/m)</summary>
        public double[] SpnSpaFzi;
        /// <summary>Spar 各输出节点 x 方向力矩 (N-m/m)</summary>
        public double[] SpnSpaMxi;
        /// <summary>Spar 各输出节点 y 方向力矩 (N-m/m)</summary>
        public double[] SpnSpaMyi;
        /// <summary>Spar 各输出节点 z 方向力矩 (N-m/m)</summary>
        public double[] SpnSpaMzi;

        // ==================== 系泊载荷 ====================

        /// <summary>系泊 x 方向力 (N)</summary>
        public double MoorFxi;
        /// <summary>系泊 y 方向力 (N)</summary>
        public double MoorFyi;
        /// <summary>系泊 z 方向力 (N)</summary>
        public double MoorFzi;
        /// <summary>系泊 x 方向力矩 (N-m)</summary>
        public double MoorMxi;
        /// <summary>系泊 y 方向力矩 (N-m)</summary>
        public double MoorMyi;
        /// <summary>系泊 z 方向力矩 (N-m)</summary>
        public double MoorMzi;
    }

    /// <summary>
    /// 波浪时历数据结构体（用于存储预计算的波浪运动学信息）
    /// </summary>
    [SourceReflection]
    [StructLayout(LayoutKind.Sequential)]
    public struct WaveKinematics
    {
        /// <summary>时间步数</summary>
        public int NSteps;
        /// <summary>时间步长 (s)</summary>
        public double DT;
        /// <summary>时间序列 (s)</summary>
        public double[] Time;
        /// <summary>波面高程时历 (m)</summary>
        public double[] WaveElev;
        /// <summary>各节点水质点 x 方向速度时历 (m/s)  [节点, 时间步]</summary>
        public double[,] WaveVxi;
        /// <summary>各节点水质点 z 方向速度时历 (m/s)  [节点, 时间步]</summary>
        public double[,] WaveVzi;
        /// <summary>各节点水质点 x 方向加速度时历 (m/s^2)  [节点, 时间步]</summary>
        public double[,] WaveAxi;
        /// <summary>各节点水质点 z 方向加速度时历 (m/s^2)  [节点, 时间步]</summary>
        public double[,] WaveAzi;
        /// <summary>各节点动水压力时历 (Pa)  [节点, 时间步]</summary>
        public double[,] DynP;
    }

    /// <summary>
    /// HydroL 输出通道枚举（与 HDR_OutParChannel 字典中的通道编号对应）
    /// </summary>
    public enum HydroL_Loadchannels
    {
        /// <summary>总水动力载荷（作用于参考点）</summary>
        Total_Hydrodynamic_Loads = 0,
        /// <summary>平台参考点运动（位移、速度、加速度）</summary>
        Platform_Reference_Point_Motions = 1,
        /// <summary>Spar 构件节点载荷</summary>
        Spar_Member_Node_Loads = 2,
        /// <summary>系泊载荷</summary>
        Mooring_Loads = 3,
        /// <summary>波面信息</summary>
        Wave_Elevation = 4,
    }

    /// <summary>
    /// Spar 构件节点信息
    /// </summary>
    [SourceReflection]
    [StructLayout(LayoutKind.Sequential)]
    public struct SparMemberNode
    {
        /// <summary>节点 z 坐标 (m)（以静水面为零点，向上为正）</summary>
        public double z;
        /// <summary>构件直径 (m)</summary>
        public double Diameter;
        /// <summary>构件截面面积 (m^2)</summary>
        public double Area;
        /// <summary>拖曳力系数 Cd</summary>
        public double Cd;
        /// <summary>惯性力系数 Cm（= 1 + Ca）</summary>
        public double Cm;
        /// <summary>浮力压力系数 Cp</summary>
        public double Cp;
        /// <summary>轴向拖曳力系数 AxCd</summary>
        public double AxCd;
        /// <summary>轴向附加质量系数 AxCa</summary>
        public double AxCa;
    }
}
