//! 自动生成 HydroL 水动力计算模块的有效输出变量数量为:38
//**********************************************************************************************************************************
// LICENSING
// Copyright(C) 2021, 2025  TG Team,Key Laboratory of Jiangsu province High-Tech design of wind turbine,WTG,WL,ZZZ
//
//    This file is part of OpenWECD.HawtC.HydroL by 赵子祯, 2021, 2025
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
//**********************************************************************************************************************************

using System.Collections.Frozen;
using System.Collections.Generic;
using OpenWECD.IO.Interface1;
using OpenWECD.IO.Log;

namespace OpenWECD.HydroL
{
    /// <summary>
    /// HydroL 输出参数类
    /// </summary>
    public class HydroL_OutputParam
    {
        /// <summary>
        /// 输出参数的参数表，保存变量名与单位
        /// </summary>
        public static FrozenDictionary<string, string> HDR_OutParUnit = new Dictionary<string, string>()
        {
            // ---- 总水动力载荷 ----
            {"HydroFxi  ","(N)       "},
            {"HydroFyi  ","(N)       "},
            {"HydroFzi  ","(N)       "},
            {"HydroMxi  ","(N-m)     "},
            {"HydroMyi  ","(N-m)     "},
            {"HydroMzi  ","(N-m)     "},
            // ---- 平台参考点位移 ----
            {"PRPSurge  ","(m)       "},
            {"PRPSway   ","(m)       "},
            {"PRPHeave  ","(m)       "},
            {"PRPRoll   ","(deg)     "},
            {"PRPPitch  ","(deg)     "},
            {"PRPYaw    ","(deg)     "},
            // ---- 平台参考点平动速度 ----
            {"PRPTVxi   ","(m/s)     "},
            {"PRPTVyi   ","(m/s)     "},
            {"PRPTVzi   ","(m/s)     "},
            // ---- 平台参考点转动速度 ----
            {"PRPRVxi   ","(rad/s)   "},
            {"PRPRVyi   ","(rad/s)   "},
            {"PRPRVzi   ","(rad/s)   "},
            // ---- 平台参考点平动加速度 ----
            {"PRPTAxi   ","(m/s^2)   "},
            {"PRPTAyi   ","(m/s^2)   "},
            {"PRPTAzi   ","(m/s^2)   "},
            // ---- 平台参考点转动加速度 ----
            {"PRPRAxi   ","(rad/s^2) "},
            {"PRPRAyi   ","(rad/s^2) "},
            {"PRPRAzi   ","(rad/s^2) "},
            // ---- Spar 构件节点载荷 ----
            {"SpnSpaFxi ","(N/m)     "},
            {"SpnSpaFyi ","(N/m)     "},
            {"SpnSpaFzi ","(N/m)     "},
            {"SpnSpaMxi ","(N-m/m)   "},
            {"SpnSpaMyi ","(N-m/m)   "},
            {"SpnSpaMzi ","(N-m/m)   "},
            // ---- 系泊载荷 ----
            {"MoorFxi   ","(N)       "},
            {"MoorFyi   ","(N)       "},
            {"MoorFzi   ","(N)       "},
            {"MoorMxi   ","(N-m)     "},
            {"MoorMyi   ","(N-m)     "},
            {"MoorMzi   ","(N-m)     "},
            // ---- 波面高程 ----
            {"WaveElev  ","(m)       "},
            {"WaveElev1 ","(m)       "},
        }.ToFrozenDictionary();

        /// <summary>
        /// 输出参数的通道表，保存变量名与通道分组
        /// 通道 0：总水动力载荷
        /// 通道 1：平台参考点运动
        /// 通道 2：Spar 节点载荷（有维度）
        /// 通道 3：系泊载荷
        /// 通道 4：波面信息
        /// </summary>
        public static FrozenDictionary<string, int> HDR_OutParChannel = new Dictionary<string, int>()
        {
            {"HydroFxi  ",0},
            {"HydroFyi  ",0},
            {"HydroFzi  ",0},
            {"HydroMxi  ",0},
            {"HydroMyi  ",0},
            {"HydroMzi  ",0},
            {"PRPSurge  ",1},
            {"PRPSway   ",1},
            {"PRPHeave  ",1},
            {"PRPRoll   ",1},
            {"PRPPitch  ",1},
            {"PRPYaw    ",1},
            {"PRPTVxi   ",1},
            {"PRPTVyi   ",1},
            {"PRPTVzi   ",1},
            {"PRPRVxi   ",1},
            {"PRPRVyi   ",1},
            {"PRPRVzi   ",1},
            {"PRPTAxi   ",1},
            {"PRPTAyi   ",1},
            {"PRPTAzi   ",1},
            {"PRPRAxi   ",1},
            {"PRPRAyi   ",1},
            {"PRPRAzi   ",1},
            {"SpnSpaFxi ",2},
            {"SpnSpaFyi ",2},
            {"SpnSpaFzi ",2},
            {"SpnSpaMxi ",2},
            {"SpnSpaMyi ",2},
            {"SpnSpaMzi ",2},
            {"MoorFxi   ",3},
            {"MoorFyi   ",3},
            {"MoorFzi   ",3},
            {"MoorMxi   ",3},
            {"MoorMyi   ",3},
            {"MoorMzi   ",3},
            {"WaveElev  ",4},
            {"WaveElev1 ",4},
        }.ToFrozenDictionary();

        /// <summary>
        /// 输出参数的维度
        /// 0 = 无维度（标量）
        /// 1 = 仅有节点编号维度（数组，按 Spar 节点）
        /// </summary>
        public static FrozenDictionary<string, int> HDR_OutParDim = new Dictionary<string, int>()
        {
            {"HydroFxi  ",0},
            {"HydroFyi  ",0},
            {"HydroFzi  ",0},
            {"HydroMxi  ",0},
            {"HydroMyi  ",0},
            {"HydroMzi  ",0},
            {"PRPSurge  ",0},
            {"PRPSway   ",0},
            {"PRPHeave  ",0},
            {"PRPRoll   ",0},
            {"PRPPitch  ",0},
            {"PRPYaw    ",0},
            {"PRPTVxi   ",0},
            {"PRPTVyi   ",0},
            {"PRPTVzi   ",0},
            {"PRPRVxi   ",0},
            {"PRPRVyi   ",0},
            {"PRPRVzi   ",0},
            {"PRPTAxi   ",0},
            {"PRPTAyi   ",0},
            {"PRPTAzi   ",0},
            {"PRPRAxi   ",0},
            {"PRPRAyi   ",0},
            {"PRPRAzi   ",0},
            {"SpnSpaFxi ",1},
            {"SpnSpaFyi ",1},
            {"SpnSpaFzi ",1},
            {"SpnSpaMxi ",1},
            {"SpnSpaMyi ",1},
            {"SpnSpaMzi ",1},
            {"MoorFxi   ",0},
            {"MoorFyi   ",0},
            {"MoorFzi   ",0},
            {"MoorMxi   ",0},
            {"MoorMyi   ",0},
            {"MoorMzi   ",0},
            {"WaveElev  ",0},
            {"WaveElev1 ",0},
        }.ToFrozenDictionary();
    }
}
