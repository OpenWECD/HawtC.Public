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
// 波浪运动学计算子程序模块（WaveL）
// 实现了 JONSWAP 谱、Pierson-Moskowitz 谱和规则波的波浪生成算法，
// 以及基于线性波浪理论的水质点运动学（速度、加速度、动水压力）计算。
//
//**********************************************************************************************************************************

using System;

namespace OpenWECD.HydroL.WaveL
{
    /// <summary>
    /// 波浪运动学计算子程序模块
    /// </summary>
    public static class WaveL_Subs
    {
        private const double TwoPi = 2.0 * Math.PI;

        // ==================== JONSWAP 谱标准参数 ====================

        /// <summary>JONSWAP 谱峰值以下的谱宽参数（σ_a），DNV-RP-C205 标准值</summary>
        private const double SigmaA = 0.07;
        /// <summary>JONSWAP 谱峰值以上的谱宽参数（σ_b），DNV-RP-C205 标准值</summary>
        private const double SigmaB = 0.09;

        /// <summary>最小角频率（rad/s），用于防止 ω→0 时的数值奇异</summary>
        private const double MinAngularFreq = 1e-6;

        /// <summary>浅水判断阈值 k·d；当 k·d 大于此值时使用双曲函数，否则用线性近似</summary>
        private const double ShallowWaterThreshold = 1e-6;

        // ==================== 波浪谱计算 ====================

        /// <summary>
        /// 计算 JONSWAP 谱谱密度
        /// S(ω) = (αg²/ω⁵) * exp(−5/4*(ωp/ω)⁴) * γ^exp(−(ω-ωp)²/(2σ²ωp²))
        /// </summary>
        /// <param name="omega">角频率 (rad/s)</param>
        /// <param name="omegap">峰值角频率 (rad/s)</param>
        /// <param name="Hs">有效波高 (m)</param>
        /// <param name="gamma">峰形参数</param>
        /// <param name="g">重力加速度 (m/s^2)</param>
        /// <returns>谱密度 (m^2·s/rad)</returns>
        public static double JONSWAP_Spectrum(double omega, double omegap, double Hs, double gamma, double g)
        {
            if (omega <= 0.0) return 0.0;

            double sigma = omega <= omegap ? SigmaA : SigmaB;  // JONSWAP 谱宽参数，DNV-RP-C205
            double r = Math.Exp(-Math.Pow(omega - omegap, 2) / (2.0 * sigma * sigma * omegap * omegap));
            double alpha = ComputeJONSWAP_Alpha(Hs, omegap, g, gamma);
            double S = alpha * g * g / Math.Pow(omega, 5)
                       * Math.Exp(-1.25 * Math.Pow(omegap / omega, 4))
                       * Math.Pow(gamma, r);
            return S;
        }

        /// <summary>
        /// 计算 Pierson-Moskowitz (PM) 谱谱密度
        /// S(ω) = (αg²/ω⁵) * exp(−5/4*(ωp/ω)⁴)   等价于 γ=1 的 JONSWAP
        /// </summary>
        public static double PM_Spectrum(double omega, double omegap, double Hs, double g)
        {
            return JONSWAP_Spectrum(omega, omegap, Hs, 1.0, g);
        }

        /// <summary>
        /// 根据有效波高和峰值频率反算 JONSWAP 谱的 Phillips 参数 α
        /// 通过数值积分满足 Hs = 4√m0 的约束
        /// </summary>
        private static double ComputeJONSWAP_Alpha(double Hs, double omegap, double g, double gamma)
        {
            // 先以 α=1 计算 m0（零阶矩），再通过比例关系反算 α
            double m0_unit = IntegrateSpectrum_m0_unit(omegap, gamma, g);
            // Hs = 4 * sqrt(m0) => m0 = (Hs/4)^2 => alpha = (Hs/4)^2 / m0_unit
            double m0_target = (Hs / 4.0) * (Hs / 4.0);
            double alpha = (m0_unit > 0.0) ? m0_target / m0_unit : 1.0;
            return alpha;
        }

        /// <summary>
        /// 以 α=1 计算谱的零阶矩 m0（数值积分，梯形法）
        /// S_unit(ω) = g²/ω⁵ * exp(-1.25*(ωp/ω)^4) * γ^r
        /// </summary>
        private static double IntegrateSpectrum_m0_unit(double omegap, double gamma, double g,
                                                          int N = 2000)
        {
            double omegaMin = 0.1 * omegap;
            double omegaMax = 5.0 * omegap;
            double dw = (omegaMax - omegaMin) / N;
            double sum = 0.0;
            double S_prev = SpecUnit(omegaMin, omegap, gamma, g);
            for (int i = 1; i <= N; i++)
            {
                double w = omegaMin + i * dw;
                double S_curr = SpecUnit(w, omegap, gamma, g);
                sum += 0.5 * (S_prev + S_curr) * dw;
                S_prev = S_curr;
            }
            return sum;
        }

        private static double SpecUnit(double omega, double omegap, double gamma, double g)
        {
            if (omega <= 0.0) return 0.0;
            double sigma = omega <= omegap ? SigmaA : SigmaB;  // JONSWAP 谱宽参数
            double r = Math.Exp(-Math.Pow(omega - omegap, 2) / (2.0 * sigma * sigma * omegap * omegap));
            return g * g / Math.Pow(omega, 5)
                   * Math.Exp(-1.25 * Math.Pow(omegap / omega, 4))
                   * Math.Pow(gamma, r);
        }

        // ==================== 不规则波浪生成 ====================

        /// <summary>
        /// 生成不规则波浪时历（JONSWAP 或 PM 谱）
        /// 使用随机相位叠加法（Random Phase Method）
        /// </summary>
        /// <param name="p">输入参数</param>
        /// <param name="nodeZ">各计算节点的 z 坐标（以静水面为零点，向上为正）</param>
        /// <param name="waveMod">波浪模型（JONSWAP=2 或 PM=3）</param>
        /// <returns>波浪运动学时历</returns>
        public static WaveKinematics GenerateIrregularWave(
            HydroL1 p,
            double[] nodeZ,
            WaveMod_Type waveMod)
        {
            double g = p.GRAVACC;
            double d = p.WtrDpth;
            double omegap = TwoPi / p.WaveTp;
            double gamma = (p.WavePkShp <= 0.0) ? GetDefaultGamma(p.WaveHs, p.WaveTp) : p.WavePkShp;
            int Nfreq = p.WvNumCOff;
            double omegaLow = Math.Max(p.WvLowCOff, MinAngularFreq);  // 防止 ω→0 时的数值奇异
            double omegaHigh = p.WvHiCOff;
            double dOmega = (omegaHigh - omegaLow) / Nfreq;
            int NSteps = (int)(p.WaveTMax / p.WaveDT) + 1;
            int NNodes = nodeZ.Length;

            var rng = new Random(p.WaveSeed);

            // 生成各频率分量的振幅和相位
            double[] omega_arr = new double[Nfreq];
            double[] A_arr = new double[Nfreq];  // 波幅 (m)
            double[] phase_arr = new double[Nfreq];
            double[] k_arr = new double[Nfreq];  // 波数 (1/m)

            for (int i = 0; i < Nfreq; i++)
            {
                double w = omegaLow + (i + 0.5) * dOmega;
                omega_arr[i] = w;
                double S;
                if (waveMod == WaveMod_Type.JONSWAP)
                    S = JONSWAP_Spectrum(w, omegap, p.WaveHs, gamma, g);
                else
                    S = PM_Spectrum(w, omegap, p.WaveHs, g);

                double Ai;
                if (p.WaveNDAmp)
                {
                    // 正态分布振幅（高斯分布，均值=sqrt(2S*dw)，标准差=sqrt(S*dw)）
                    double sigma_a = Math.Sqrt(S * dOmega);
                    Ai = Math.Abs(SampleNormal(rng, sigma_a * Math.Sqrt(2.0), sigma_a));
                }
                else
                {
                    Ai = Math.Sqrt(2.0 * S * dOmega);
                }
                A_arr[i] = Ai;
                phase_arr[i] = rng.NextDouble() * TwoPi;
                k_arr[i] = DispersionRelation(w, d, g);
            }

            // 分配存储空间
            var wk = new WaveKinematics
            {
                NSteps = NSteps,
                DT = p.WaveDT,
                Time = new double[NSteps],
                WaveElev = new double[NSteps],
                WaveVxi = new double[NNodes, NSteps],
                WaveVzi = new double[NNodes, NSteps],
                WaveAxi = new double[NNodes, NSteps],
                WaveAzi = new double[NNodes, NSteps],
                DynP = new double[NNodes, NSteps],
            };

            double cosDir = Math.Cos(p.WaveDir * Math.PI / 180.0);
            double sinDir = Math.Sin(p.WaveDir * Math.PI / 180.0);

            for (int it = 0; it < NSteps; it++)
            {
                double t = it * p.WaveDT;
                wk.Time[it] = t;
                double elev = 0.0;
                for (int i = 0; i < Nfreq; i++)
                {
                    double w = omega_arr[i];
                    double A = A_arr[i];
                    double phi = phase_arr[i];
                    double psi = -w * t + phi;
                    elev += A * Math.Cos(psi);
                }
                wk.WaveElev[it] = elev;

                // 各节点水质点运动学（线性波浪理论）
                for (int jn = 0; jn < NNodes; jn++)
                {
                    double z = nodeZ[jn];
                    // 仅计算水面以下节点（Wheeler 延伸或截断到泥线）
                    double zClamped = Math.Max(Math.Min(z, 0.0), -d);
                    double vx = 0.0, vz = 0.0, ax = 0.0, az = 0.0, dp = 0.0;
                    double rho = p.WtrDens;

                    for (int i = 0; i < Nfreq; i++)
                    {
                        double w = omega_arr[i];
                        double A = A_arr[i];
                        double k = k_arr[i];
                        double phi = phase_arr[i];
                        double psi = -w * t + phi;

                        // 线性波浪理论：cosh(k(z+d))/cosh(kd)
                        double kd = k * d;
                        double kzd = k * (zClamped + d);
                        double coshRatio = (kd > ShallowWaterThreshold)
                                           ? Math.Cosh(kzd) / Math.Cosh(kd)
                                           : 1.0;
                        double sinhRatio = (kd > ShallowWaterThreshold)
                                           ? Math.Sinh(kzd) / Math.Cosh(kd)
                                           : (zClamped + d) / d;

                        double wAcosh = w * A * coshRatio;
                        double wAsinh = w * A * sinhRatio;

                        // 水平速度（沿波浪传播方向）
                        vx += wAcosh * Math.Cos(psi);
                        // 垂向速度
                        vz += wAsinh * Math.Sin(psi);
                        // 水平加速度
                        ax += w * wAcosh * Math.Sin(psi);
                        // 垂向加速度
                        az -= w * wAsinh * Math.Cos(psi);
                        // 动水压力 p = ρg*A*cosh(k(z+d))/cosh(kd)*cos(psi)
                        dp += rho * g * A * coshRatio * Math.Cos(psi);
                    }

                    wk.WaveVxi[jn, it] = vx * cosDir;
                    wk.WaveVzi[jn, it] = vz;
                    wk.WaveAxi[jn, it] = ax * cosDir;
                    wk.WaveAzi[jn, it] = az;
                    wk.DynP[jn, it] = dp;
                }
            }
            return wk;
        }

        /// <summary>
        /// 生成规则波浪时历（线性规则波）
        /// </summary>
        public static WaveKinematics GenerateRegularWave(HydroL1 p, double[] nodeZ)
        {
            double g = p.GRAVACC;
            double d = p.WtrDpth;
            double A = p.WaveHs / 2.0;   // 规则波振幅 = Hs/2
            double Tp = p.WaveTp;
            double omega = TwoPi / Tp;
            double k = DispersionRelation(omega, d, g);
            int NSteps = (int)(p.WaveTMax / p.WaveDT) + 1;
            int NNodes = nodeZ.Length;
            double cosDir = Math.Cos(p.WaveDir * Math.PI / 180.0);

            var wk = new WaveKinematics
            {
                NSteps = NSteps,
                DT = p.WaveDT,
                Time = new double[NSteps],
                WaveElev = new double[NSteps],
                WaveVxi = new double[NNodes, NSteps],
                WaveVzi = new double[NNodes, NSteps],
                WaveAxi = new double[NNodes, NSteps],
                WaveAzi = new double[NNodes, NSteps],
                DynP = new double[NNodes, NSteps],
            };

            for (int it = 0; it < NSteps; it++)
            {
                double t = it * p.WaveDT;
                wk.Time[it] = t;
                wk.WaveElev[it] = A * Math.Cos(-omega * t);

                for (int jn = 0; jn < NNodes; jn++)
                {
                    double z = Math.Max(Math.Min(nodeZ[jn], 0.0), -d);
                    double kd = k * d;
                    double kzd = k * (z + d);
                    double coshRatio = (kd > ShallowWaterThreshold)
                                       ? Math.Cosh(kzd) / Math.Cosh(kd)
                                       : 1.0;
                    double sinhRatio = (kd > ShallowWaterThreshold)
                                       ? Math.Sinh(kzd) / Math.Cosh(kd)
                                       : (z + d) / d;
                    double psi = -omega * t;

                    wk.WaveVxi[jn, it] = omega * A * coshRatio * Math.Cos(psi) * cosDir;
                    wk.WaveVzi[jn, it] = omega * A * sinhRatio * Math.Sin(psi);
                    wk.WaveAxi[jn, it] = omega * omega * A * coshRatio * Math.Sin(psi) * cosDir;
                    wk.WaveAzi[jn, it] = -omega * omega * A * sinhRatio * Math.Cos(psi);
                    wk.DynP[jn, it] = p.WtrDens * p.GRAVACC * A * coshRatio * Math.Cos(psi);
                }
            }
            return wk;
        }

        // ==================== 辅助函数 ====================

        /// <summary>
        /// 计算 JONSWAP 谱默认峰形参数 γ（根据 DNV 规范）
        /// </summary>
        /// 根据 DNV-RP-C205 Section 3.5.5，JONSWAP 峰形参数 γ 的推荐取值规则：
        /// - 当 Tp/√Hs ≤ 3.6 时，γ = 5.0（尖峰谱）
        /// - 当 Tp/√Hs ≥ 5.0 时，γ = 1.0（PM 谱退化）
        /// - 中间值按 exp(5.75 - 1.15·Tp/√Hs) 插值
        public static double GetDefaultGamma(double Hs, double Tp)
        {
            double ratio = Tp / Math.Sqrt(Hs);
            if (ratio <= 3.6) return 5.0;   // 尖峰谱，DNV 下限
            if (ratio >= 5.0) return 1.0;   // PM 谱退化，DNV 上限
            return Math.Exp(5.75 - 1.15 * ratio);
        }

        /// <summary>
        /// 通过线性波浪弥散关系求解波数 k（Newton-Raphson 迭代法）
        /// ω² = g·k·tanh(k·d)
        /// </summary>
        /// <param name="omega">角频率 (rad/s)</param>
        /// <param name="d">水深 (m)</param>
        /// <param name="g">重力加速度 (m/s^2)</param>
        /// <returns>波数 k (1/m)</returns>
        public static double DispersionRelation(double omega, double d, double g)
        {
            double omega2 = omega * omega;
            // 深水近似作为初值：k0 = ω²/g
            double k = omega2 / g;
            for (int iter = 0; iter < 50; iter++)
            {
                double kd = k * d;
                double tanhkd = Math.Tanh(kd);
                double f = g * k * tanhkd - omega2;
                double df = g * (tanhkd + kd * (1.0 - tanhkd * tanhkd));
                double dk = -f / df;
                k += dk;
                if (Math.Abs(dk) < 1e-10 * k) break;
            }
            return k;
        }

        /// <summary>
        /// 从正态分布采样（Box-Muller 变换）
        /// </summary>
        private static double SampleNormal(Random rng, double mean, double std)
        {
            double u1 = 1.0 - rng.NextDouble();
            double u2 = 1.0 - rng.NextDouble();
            double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(TwoPi * u2);
            return mean + std * z;
        }

        /// <summary>
        /// 对波浪运动学时历做线性插值，获取指定时刻 t 的值
        /// </summary>
        public static double InterpolateWave(double[] timeArray, double[] dataArray, double t, bool conRead)
        {
            int n = timeArray.Length;
            double tMax = timeArray[n - 1];
            if (conRead && t > tMax)
            {
                t = t % tMax;
            }
            t = Math.Max(timeArray[0], Math.Min(t, tMax));
            int i = Array.BinarySearch(timeArray, t);
            if (i >= 0) return dataArray[i];
            i = ~i;
            if (i == 0) return dataArray[0];
            if (i >= n) return dataArray[n - 1];
            double alpha = (t - timeArray[i - 1]) / (timeArray[i] - timeArray[i - 1]);
            return dataArray[i - 1] + alpha * (dataArray[i] - dataArray[i - 1]);
        }

        /// <summary>
        /// 对二维波浪运动学时历做线性插值，获取指定节点、指定时刻的值
        /// </summary>
        public static double InterpolateWave2D(double[] timeArray, double[,] dataArray, int nodeIdx, double t, bool conRead)
        {
            int n = timeArray.Length;
            double tMax = timeArray[n - 1];
            if (conRead && t > tMax)
            {
                t = t % tMax;
            }
            t = Math.Max(timeArray[0], Math.Min(t, tMax));
            int i = Array.BinarySearch(timeArray, t);
            if (i >= 0) return dataArray[nodeIdx, i];
            i = ~i;
            if (i == 0) return dataArray[nodeIdx, 0];
            if (i >= n) return dataArray[nodeIdx, n - 1];
            double alpha = (t - timeArray[i - 1]) / (timeArray[i] - timeArray[i - 1]);
            return dataArray[nodeIdx, i - 1] + alpha * (dataArray[nodeIdx, i] - dataArray[nodeIdx, i - 1]);
        }
    }
}
