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
// HydroL 模块的输入/输出子程序
// 负责读取 HydroL 主输入文件（.dat 格式），并写入输出文件。
//
//**********************************************************************************************************************************

using OpenWECD.IO.Interface1;
using OpenWECD.IO.IO;
using OpenWECD.IO.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenWECD.HydroL
{
    /// <summary>
    /// HydroL 输入/输出子程序类
    /// </summary>
    public class HydroL_IO_Subs : IModulInterYml<HydroL1>
    {
        /// <summary>
        /// 读取 HydroL 主输入文件（.dat 格式）
        /// </summary>
        /// <param name="path">HydroL 主文件路径</param>
        /// <returns>填充好的 HydroL1 结构体</returns>
        public static HydroL1 ReadHydroL_MainFile(string path)
        {
            var hdr = new HydroL1();
            CheckError.Filexists(path);
            hdr.HydroLfilepath = path;
            hdr.HydroLData = File.ReadAllLines(path);

            // 辅助函数：查找包含关键字的行索引
            int fd(string temp, bool error = true, bool show = true)
                => Otherhelper.GetMatchingLineIndexes(hdr.HydroLData, temp, path, error, show)[0];

            // ==================== 基本控制参数 ====================
            hdr.HydroMod = Otherhelper.ParseLine<HydroMod_Type>(hdr.HydroLData, path, fd(" HydroMod "));
            hdr.MoorLine = Otherhelper.ParseLine<MoorLine_Type>(hdr.HydroLData, path, fd(" MoorLine "));

            // ==================== 环境参数 ====================
            hdr.WtrDens  = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WtrDens "));
            hdr.WtrDpth  = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WtrDpth "));
            hdr.MSL2SWL  = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" MSL2SWL "));
            hdr.GRAVACC  = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" GRAVACC "));

            // ==================== 海流参数 ====================
            hdr.CurrentCoordinate = Otherhelper.ParseLine<double[]>(hdr.HydroLData, path,
                                        fd(" CurrentCoordinate "), fg1: ',');
            hdr.CurrentVelocity   = Otherhelper.ParseLine<double[]>(hdr.HydroLData, path,
                                        fd(" CurrentVelocity "), fg1: ',');
            hdr.CurrentPolyOrder  = Otherhelper.ParseLine<int>(hdr.HydroLData, path,
                                        fd(" CurrentPolyOrder "));

            // ==================== 波浪参数 ====================
            hdr.WaveMod   = Otherhelper.ParseLine<WaveMod_Type>(hdr.HydroLData, path, fd(" WaveMod "));
            hdr.WaveTMax  = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WaveTMax "));
            hdr.WaveDT    = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WaveDT "));
            hdr.WaveHs    = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WaveHs "));
            hdr.WaveTp    = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WaveTp "));
            hdr.WavePkShp = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WavePkShp "));
            hdr.WvLowCOff = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WvLowCOff "));
            hdr.WvHiCOff  = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WvHiCOff "));
            hdr.WvNumCOff = Otherhelper.ParseLine<int>(hdr.HydroLData, path, fd(" WvNumCOff "));
            hdr.WaveDir   = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WaveDir "));
            hdr.WaveSeed  = Otherhelper.ParseLine<int>(hdr.HydroLData, path, fd(" WaveSeed "));
            hdr.WaveNDAmp = Otherhelper.ParseLine<bool>(hdr.HydroLData, path, fd(" WaveNDAmp "));
            hdr.FileGes   = Otherhelper.ParseLine<int>(hdr.HydroLData, path, fd(" FileGes "));
            hdr.WvKinFile = Otherhelper.ParseLine<string>(hdr.HydroLData, path, fd(" WvKinFile "));

            // ==================== 系泊线参数 ====================
            if (hdr.MoorLine != MoorLine_Type.None)
            {
                hdr.MoorLineDT   = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" MoorLineDT "));
                hdr.MoorDynFile  = Otherhelper.ParseLine<string>(hdr.HydroLData, path, fd(" MoorDynFile "));
                hdr.OpenMoorFile = Otherhelper.ParseLine<string>(hdr.HydroLData, path, fd(" OpenMoorFile "));
                string baseDir = Path.GetDirectoryName(path)!;
                if (!Path.IsPathRooted(hdr.MoorDynFile))
                    hdr.MoorDynFile = Path.GetFullPath(Path.Combine(baseDir, hdr.MoorDynFile));
                if (!Path.IsPathRooted(hdr.OpenMoorFile))
                    hdr.OpenMoorFile = Path.GetFullPath(Path.Combine(baseDir, hdr.OpenMoorFile));
            }

            // ==================== 浮式平台参数 ====================
            hdr.PotMod    = Otherhelper.ParseLine<PotMod_Type>(hdr.HydroLData, path, fd(" PotMod "));
            hdr.ConRead   = Otherhelper.ParseLine<bool>(hdr.HydroLData, path, fd(" ConRead "));
            hdr.PotFile   = Otherhelper.ParseLine<string>(hdr.HydroLData, path, fd(" PotFile "));
            hdr.WAMITULEN = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" WAMITULEN "));
            hdr.PtfmVol0  = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" PtfmVol0 "));
            hdr.PtfmCOBxt = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" PtfmCOBxt "));
            hdr.PtfmCOByt = Otherhelper.ParseLine<double>(hdr.HydroLData, path, fd(" PtfmCOByt "));

            // ==================== 附加刚度/阻尼（6×1 向量与 6×6 矩阵）====================
            hdr.AddF0    = ParseDataVector(hdr.HydroLData[fd(" AddF0 ")], 6);
            hdr.AddCLin  = ParseDataMatrix(hdr.HydroLData, fd(" AddCLin "), 6);
            hdr.AddCHydr0= ParseDataMatrix(hdr.HydroLData, fd(" AddCHydr0 "), 6);
            hdr.AddBLin  = ParseDataMatrix(hdr.HydroLData, fd(" AddBLin "), 6);

            // ==================== 轴向系数 ====================
            hdr.NAxCoef = Otherhelper.ParseLine<int>(hdr.HydroLData, path, fd(" NAxCoef "));
            if (hdr.NAxCoef > 0)
            {
                hdr.AxCoefID = new int[hdr.NAxCoef];
                hdr.AxCd     = new double[hdr.NAxCoef];
                hdr.AxCa     = new double[hdr.NAxCoef];
                hdr.AxCp     = new double[hdr.NAxCoef];
                int headerLine = fd(" AxCoefID ");
                for (int i = 0; i < hdr.NAxCoef; i++)
                {
                    var parts = SplitDataLine(hdr.HydroLData[headerLine + 2 + i]);
                    hdr.AxCoefID[i] = int.Parse(parts[0]);
                    hdr.AxCd[i]     = double.Parse(parts[1]);
                    hdr.AxCa[i]     = double.Parse(parts[2]);
                    hdr.AxCp[i]     = double.Parse(parts[3]);
                }
            }

            // ==================== 构件节点 ====================
            hdr.NJoints = Otherhelper.ParseLine<int>(hdr.HydroLData, path, fd(" NJoints "));
            if (hdr.NJoints > 0)
            {
                hdr.JointID    = new int[hdr.NJoints];
                hdr.Jointxi    = new double[hdr.NJoints];
                hdr.Jointyi    = new double[hdr.NJoints];
                hdr.Jointzi    = new double[hdr.NJoints];
                hdr.JointAxID  = new int[hdr.NJoints];
                hdr.JointOvrlp = new int[hdr.NJoints];
                int headerLine = fd(" JointID ");
                for (int i = 0; i < hdr.NJoints; i++)
                {
                    var parts = SplitDataLine(hdr.HydroLData[headerLine + 2 + i]);
                    hdr.JointID[i]    = int.Parse(parts[0]);
                    hdr.Jointxi[i]    = double.Parse(parts[1]);
                    hdr.Jointyi[i]    = double.Parse(parts[2]);
                    hdr.Jointzi[i]    = double.Parse(parts[3]);
                    hdr.JointAxID[i]  = int.Parse(parts[4]);
                    hdr.JointOvrlp[i] = int.Parse(parts[5]);
                }
            }

            // ==================== 截面属性 ====================
            hdr.NPropSets = Otherhelper.ParseLine<int>(hdr.HydroLData, path, fd(" NPropSets "));
            if (hdr.NPropSets > 0)
            {
                hdr.PropSetID = new int[hdr.NPropSets];
                hdr.PropD     = new double[hdr.NPropSets];
                hdr.PropThck  = new double[hdr.NPropSets];
                int headerLine = fd(" PropSetID ");
                for (int i = 0; i < hdr.NPropSets; i++)
                {
                    var parts = SplitDataLine(hdr.HydroLData[headerLine + 2 + i]);
                    hdr.PropSetID[i] = int.Parse(parts[0]);
                    hdr.PropD[i]     = double.Parse(parts[1]);
                    hdr.PropThck[i]  = double.Parse(parts[2]);
                }
            }

            // ==================== 输出设置 ====================
            hdr.SumPrint = Otherhelper.ParseLine<bool>(hdr.HydroLData, path,
                               fd(" SumPrint "), moren: true, key: "SumPrint");
            if (hdr.SumPrint)
            {
                hdr.SumPath = Otherhelper.ParseLine<string>(hdr.HydroLData, path, fd(" SumPath "));
                string temp = hdr.SumPath + "1.out";
                CheckError.Filexists(path, ref temp, true, ".out", true);
                hdr.SumPath = temp.Replace("1.out", " ").Trim();

                hdr.NSparOutNdnum = Otherhelper.ParseLine<int>(hdr.HydroLData, path,
                                        fd(" NSparOutNdnum "), moren: 0);
                if (hdr.NSparOutNdnum > 0)
                {
                    hdr.SparOutNd = Otherhelper.ParseLine<int[]>(hdr.HydroLData, path,
                                        fd(" SparOutNd "), fg1: ',');
                    if (hdr.NSparOutNdnum > hdr.SparOutNd.Length)
                    {
                        LogHelper.ErrorLog("NSparOutNdnum > SparOutNd.Length",
                                           FunctionName: "ReadHydroL_MainFile");
                    }
                    hdr.SparOutNd = hdr.SparOutNd[0..hdr.NSparOutNdnum];
                }
                hdr.Outputs_OutList = Otherhelper.ReadOutputWord(
                                          hdr.HydroLData, fd(" OutList ") + 1, true);
            }

            return hdr;
        }

        // ==================== 辅助读取方法 ====================

        /// <summary>
        /// 将数据行按空白字符分割为字符串数组（过滤注释符 ! 后的内容）
        /// </summary>
        private static string[] SplitDataLine(string line)
        {
            int commentIdx = line.IndexOf('!');
            if (commentIdx >= 0) line = line[..commentIdx];
            return line.Split(new char[] { ' ', '\t', ',' },
                              StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 从单行数据中解析 N 个双精度浮点数（空格或逗号分隔）
        /// </summary>
        private static double[] ParseDataVector(string line, int N)
        {
            var parts = SplitDataLine(line);
            var vec = new double[N];
            for (int i = 0; i < N && i < parts.Length; i++)
                vec[i] = double.Parse(parts[i]);
            return vec;
        }

        /// <summary>
        /// 从起始行读取 N×N 矩阵（每行空格分隔，共 N 行）
        /// </summary>
        private static double[,] ParseDataMatrix(string[] data, int startLine, int N)
        {
            var mat = new double[N, N];
            for (int row = 0; row < N; row++)
            {
                var parts = SplitDataLine(data[startLine + row]);
                for (int col = 0; col < N && col < parts.Length; col++)
                    mat[row, col] = double.Parse(parts[col]);
            }
            return mat;
        }
    }

    /// <summary>
    /// HydroL 输出写入类
    /// </summary>
    public class HydroL_IO_Outs : HydroL_OutputParam
    {
        #region 初始化要求的信息
        private HydroL_AllOuts AllOuts;
        private HydroL1 HDR;
        #endregion

        private readonly bool SumPrint;
        private readonly int ChannelNum;
        private IO_ChannelType[] Hstchannel;

        /// <summary>
        /// 初始化 HydroL 输出写入器
        /// </summary>
        public HydroL_IO_Outs(HydroL1 hdr, HydroL_ParameterType p,
                               string title = "Time", string unit = "(s)")
        {
            this.HDR = hdr;
            this.SumPrint = hdr.SumPrint;
            AllOuts = HydroL_INI.HDR_INIAllOuts(hdr);

            if (SumPrint)
            {
                HDR_OutCheckOutPar();
                HDR_OutFomart(ref hdr.Outputs_OutList, 10);
                HDR_OutCheckParmExist(hdr.Outputs_OutList);

                Hstchannel = HDR_OutSetChannels(hdr, p);
                ChannelNum = Hstchannel.Length;
            }
        }

        /// <summary>
        /// 根据当前自由度和外力信息计算输出
        /// </summary>
        public void HDR_CalcOutput(double t, int i_t,
                                   HydroL_ParameterType p,
                                   in HydroL_RtHndSideType RtHS)
        {
        }

        /// <summary>
        /// 将计算结果输出到文件
        /// </summary>
        public void HDR_OutWriteResult(double t, int i_t)
        {
            if (!SumPrint) return;
            for (int i = 0; i < ChannelNum; i++)
            {
                Hstchannel[i].OutFile.Write(t);
                for (int j = 0; j < Hstchannel[i].VariablesNum; j++)
                {
                    var temp = Hstchannel[i].OutputVariables[j];
                    Hstchannel[i].OutFile.Write(HDR_GetParamOutput(temp.name, temp.J, AllOuts, temp.K));
                }
                Hstchannel[i].OutFile.WriteLine();
            }
        }

        /// <summary>
        /// 更新当前时步的输出变量
        /// </summary>
        public void HDR_OutUpdate(ref HydroL_AllOuts allOuts)
        {
            this.AllOuts = allOuts;
        }

        // ==================== 内部辅助方法 ====================

        /// <summary>
        /// 将文件中的变量名格式化为固定长度（以匹配字典键）
        /// </summary>
        private static void HDR_OutFomart(ref string[] list, int length)
        {
            for (int i = 0; i < list.Length; i++)
                HDR_OutFomart(ref list[i], length);
        }

        private static void HDR_OutFomart(ref string key, int length = 10)
        {
            if (key.Length < length)
                key = key.PadRight(length);
            else if (key.Length > length)
                LogHelper.ErrorLog("HDR_OutFomart Error! The outputkey length must be " + length,
                                   FunctionName: "HDR_OutFomart");
        }

        /// <summary>
        /// 检查输出参数字典长度是否正确
        /// </summary>
        private void HDR_OutCheckOutPar(int length = 10)
        {
            foreach (var item in HDR_OutParUnit)
            {
                if (item.Value.Length != length)
                {
                    LogHelper.ErrorLog("The length of " + item.Key + " is not " + length,
                                       FunctionName: "HDR_OutCheckOutPar");
                }
            }
        }

        /// <summary>
        /// 检查用户请求的输出变量是否存在
        /// </summary>
        private void HDR_OutCheckParmExist(string[] list)
        {
            for (int i = 0; i < list.Length; i++)
                HDR_OutCheckParmExist(list[i]);
        }

        private static void HDR_OutCheckParmExist(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                LogHelper.ErrorLog("The " + key + " is null or Empty",
                                   FunctionName: "HDR_OutCheckParmExist");
            }
            HDR_OutFomart(ref key);
            if (!HDR_OutParUnit.ContainsKey(key))
            {
                LogHelper.ErrorLog("The HydroL Outpar:\"" + key +
                                   "\" is not exist, please check HydroL main file",
                                   FunctionName: "HDR_OutCheckParmExist");
            }
        }

        /// <summary>
        /// 建立输出通道列表
        /// </summary>
        private IO_ChannelType[] HDR_OutSetChannels(HydroL1 hdr, HydroL_ParameterType p)
        {
            var outputchannels = new List<IO_ChannelType>();

            var channels = new List<int>();
            for (int i = 0; i < hdr.Outputs_OutList.Length; i++)
            {
                int cam = HDR_OutParChannel[hdr.Outputs_OutList[i]];
                if (channels.IndexOf(cam) == -1)
                    channels.Add(cam);
            }
            var temp = channels.ToArray();
            Array.Sort(temp);
            channels = temp.ToList();

            for (int i = 0; i < channels.Count; i++)
            {
                IO_ChannelType ChannelType = new IO_ChannelType();
                ChannelType.name = HDR_OutGetChannelName(channels[i]);
                ChannelType.ChannelNum = channels[i];
                var ChannelTypeV = new List<IO_OutputVariablesType>();

                for (int j = 0; j < hdr.Outputs_OutList.Length; j++)
                {
                    if (channels[i] != HDR_OutParChannel[hdr.Outputs_OutList[j]]) continue;

                    if (HDR_OutParDim[hdr.Outputs_OutList[j]] == 0)
                    {
                        // 无维度变量（标量）
                        IO_OutputVariablesType hst = new IO_OutputVariablesType
                        {
                            name       = hdr.Outputs_OutList[j],
                            title      = hdr.Outputs_OutList[j],
                            unit       = HDR_OutParUnit[hdr.Outputs_OutList[j]],
                            ChannelNum = channels[i],
                            ChannelName= HDR_OutGetChannelName(channels[i]),
                            J = 0,
                            K = 0,
                        };
                        ChannelTypeV.Add(hst);
                    }
                    else if (HDR_OutParDim[hdr.Outputs_OutList[j]] == 1)
                    {
                        // 按 Spar 输出节点展开
                        if (hdr.SparOutNd != null)
                        {
                            for (int nd = 0; nd < hdr.NSparOutNdnum; nd++)
                            {
                                IO_OutputVariablesType hst = new IO_OutputVariablesType
                                {
                                    name       = hdr.Outputs_OutList[j],
                                    title      = hdr.Outputs_OutList[j] + "N_" + hdr.SparOutNd[nd],
                                    unit       = HDR_OutParUnit[hdr.Outputs_OutList[j]],
                                    ChannelNum = channels[i],
                                    ChannelName= HDR_OutGetChannelName(channels[i]),
                                    J = hdr.SparOutNd[nd],
                                    K = 0,
                                };
                                ChannelTypeV.Add(hst);
                            }
                        }
                    }
                }

                ChannelType.OutputVariables = ChannelTypeV.ToArray();
                ChannelType.VariablesNum    = ChannelType.OutputVariables.Length;
                outputchannels.Add(ChannelType);
            }

            return outputchannels.ToArray();
        }

        /// <summary>
        /// 获取通道名称字符串
        /// </summary>
        public static string HDR_OutGetChannelName(int channelnum)
        {
            return Enum.GetName(typeof(HydroL_Loadchannels), channelnum)!.Replace('_', ' ') + " ";
        }

        /// <summary>
        /// 根据通道变量名和索引从 AllOuts 读取输出数值
        /// </summary>
        private static double HDR_GetParamOutput(string name, int J, HydroL_AllOuts allOuts, int K = 0)
        {
            string key = name.TrimEnd();
            return key switch
            {
                "HydroFxi"  => allOuts.HydroFxi,
                "HydroFyi"  => allOuts.HydroFyi,
                "HydroFzi"  => allOuts.HydroFzi,
                "HydroMxi"  => allOuts.HydroMxi,
                "HydroMyi"  => allOuts.HydroMyi,
                "HydroMzi"  => allOuts.HydroMzi,
                "PRPSurge"  => allOuts.PRPSurge,
                "PRPSway"   => allOuts.PRPSway,
                "PRPHeave"  => allOuts.PRPHeave,
                "PRPRoll"   => allOuts.PRPRoll,
                "PRPPitch"  => allOuts.PRPPitch,
                "PRPYaw"    => allOuts.PRPYaw,
                "PRPTVxi"   => allOuts.PRPTVxi,
                "PRPTVyi"   => allOuts.PRPTVyi,
                "PRPTVzi"   => allOuts.PRPTVzi,
                "PRPRVxi"   => allOuts.PRPRVxi,
                "PRPRVyi"   => allOuts.PRPRVyi,
                "PRPRVzi"   => allOuts.PRPRVzi,
                "PRPTAxi"   => allOuts.PRPTAxi,
                "PRPTAyi"   => allOuts.PRPTAyi,
                "PRPTAzi"   => allOuts.PRPTAzi,
                "PRPRAxi"   => allOuts.PRPRAxi,
                "PRPRAyi"   => allOuts.PRPRAyi,
                "PRPRAzi"   => allOuts.PRPRAzi,
                "SpnSpaFxi" => (allOuts.SpnSpaFxi != null && J < allOuts.SpnSpaFxi.Length)
                               ? allOuts.SpnSpaFxi[J] : 0.0,
                "SpnSpaFyi" => (allOuts.SpnSpaFyi != null && J < allOuts.SpnSpaFyi.Length)
                               ? allOuts.SpnSpaFyi[J] : 0.0,
                "SpnSpaFzi" => (allOuts.SpnSpaFzi != null && J < allOuts.SpnSpaFzi.Length)
                               ? allOuts.SpnSpaFzi[J] : 0.0,
                "SpnSpaMxi" => (allOuts.SpnSpaMxi != null && J < allOuts.SpnSpaMxi.Length)
                               ? allOuts.SpnSpaMxi[J] : 0.0,
                "SpnSpaMyi" => (allOuts.SpnSpaMyi != null && J < allOuts.SpnSpaMyi.Length)
                               ? allOuts.SpnSpaMyi[J] : 0.0,
                "SpnSpaMzi" => (allOuts.SpnSpaMzi != null && J < allOuts.SpnSpaMzi.Length)
                               ? allOuts.SpnSpaMzi[J] : 0.0,
                "MoorFxi"   => allOuts.MoorFxi,
                "MoorFyi"   => allOuts.MoorFyi,
                "MoorFzi"   => allOuts.MoorFzi,
                "MoorMxi"   => allOuts.MoorMxi,
                "MoorMyi"   => allOuts.MoorMyi,
                "MoorMzi"   => allOuts.MoorMzi,
                _           => 0.0,
            };
        }
    }
}
