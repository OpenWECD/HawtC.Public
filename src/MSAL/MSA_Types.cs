

//**********************************************************************************************************************************
// LICENSING
// Copyright(C) 2021, 2025  TG Team,Key Laboratory of Jiangsu province High-Tech design of wind turbine,WTG,WL,������
//                                      
//    This file is part of OpenWECD.MBD by ������, 2021, 2024
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
//**********************************************************************************************************************************
#define MBDLATFLOAT
////#define MBDLATDOUBLE
//#define BDLATMATHNET

using MathNet.Numerics.LinearAlgebra;
using OpenWECD.HawtC2;
using OpenWECD.IO.IO;
using OpenWECD.IO.Log;
using System.Runtime;
using System.Runtime.CompilerServices;
using SourceGeneration.Reflection;
using System.Runtime.InteropServices;


//# ʹ��Using���������ͣ�ת��ʱҪ��֤˫���Ȼ��ߵ�����
#if MBDLATDOUBLE

using Mat = OpenWECD.IO.Numerics.Matrix3S;
using Vec = OpenWECD.IO.Numerics.VecS;
using NUMT = System.Double;

#elif MBDLATFLOAT

using Mat = OpenWECD.IO.Numerics.Matrix3f;
using Vec = System.Numerics.Vector3;
using NUMT = System.Single;

#elif MBDLATMATHNET

using Mat=MathNet.Numerics.LinearAlgebra.Matrix<double>;
using Vec= MathNet.Numerics.LinearAlgebra.Vector<double>;
using NUMT = System.Double;

#endif


namespace OpenWECD.MSAL
{
    /// <remarks>
    /// ���е�MSAL���еĲ���
    /// </remarks>
    [SourceReflection]
    [StructLayout(LayoutKind.Sequential)]
    public struct MSAL1
    {
        public string MSALfilepath;

        public string[] MSALData;

        #region output

        /// <summary>
        /// �Ƿ���������ļ�
        /// </summary>
        public bool SumPrint;

        /// <summary>
        /// ���ģʽѡ���ǽڵ㡤���Ǳ���������ǽڵ㽫��������ڵ��������ļ�������������������������ļ���
        /// </summary>
        public bool AfSpanput;
        /// <summary>
        /// �����ҶƬ��ţ�Ĭ��ֻ��0
        /// </summary>
        public int[] BldOutSig;
        /// <summary>
        /// ���ɵ��ļ������ƣ�ע�����ļ��У�
        /// </summary>
        public string SumPath;

        /// <remarks>
        /// ҶƬ������������
        /// </remarks>
        public int NBlOuts;

        /// <remarks>
        /// �����ҶƬ�ڵ���
        /// </remarks>
        public int[] BlOutNd;

        /// <remarks>
        /// ���ܵ�����������
        /// </remarks>
        public int NTwOuts;

        /// <remarks>
        /// ��������ܽڵ���
        /// </remarks>
        public int[] TwOutNd;

        public string[] Outputs_OutList;

        #endregion  output
    }
    /// <summary>
    /// MSAL���еĲ����ṹ��
    /// </summary>
    [SourceReflection]
    [StructLayout(LayoutKind.Sequential)]
    public struct MSA_ParameterType
    {

    }
    /// <summary>
    /// MSAL���е�����ʱ�����ṹ��
    /// </summary>
    [SourceReflection]
    [StructLayout(LayoutKind.Sequential)]
    public struct MSA_RtHndSideType
    {
        public MSA_IO_Outs MSA_IO_Out;
    }
    /// <summary>
    /// 
    /// </summary>
    public struct MSA_AllOuts
    {
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���x�����ϵ�ҶƬK�ڵ�J�����������   unit:(%)
        /// </summary>
        public double[,] BkNjADxi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���y�����ϵ�ҶƬK�ڵ�J�����������   unit:(%)
        /// </summary>
        public double[,] BkNjADyi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���z�����ϵ�ҶƬK�ڵ�J�����������   unit:(%)
        /// </summary>
        public double[,] BkNjADzi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���x�����ϵ�ҶƬK�����������   unit:(%)
        /// </summary>
        public double[] BldKADxi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���y�����ϵ�ҶƬK�����������   unit:(%)
        /// </summary>
        public double[] BldKADyi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���z�����ϵ�ҶƬK�����������   unit:(%)
        /// </summary>
        public double[] BldKADzi;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���x�����ϵ�ҶƬK�ڵ�J�����������   unit:(%)
        /// </summary>
        public double[,] BkNjADxt;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���y�����ϵ�ҶƬK�ڵ�J�����������   unit:(%)
        /// </summary>
        public double[,] BkNjADyt;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���z�����ϵ�ҶƬK�ڵ�J�����������   unit:(%)
        /// </summary>
        public double[,] BkNjADzt;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���x�����ϵ�ҶƬK�����������   unit:(%)
        /// </summary>
        public double[] BldKADxt;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���y�����ϵ�ҶƬK�����������   unit:(%)
        /// </summary>
        public double[] BldKADyt;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���z�����ϵ�ҶƬK�����������   unit:(%)
        /// </summary>
        public double[] BldKADzt;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���x�����ϵ�ҶƬK�ڵ�J��������   unit:(Kw)
        /// </summary>
        public double[,] BkNjAWxi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���y�����ϵ�ҶƬK�ڵ�J��������   unit:(Kw)
        /// </summary>
        public double[,] BkNjAWyi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���z�����ϵ�ҶƬK�ڵ�J��������   unit:(Kw)
        /// </summary>
        public double[,] BkNjAWzi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���x�����ϵ�ҶƬK��������   unit:(Kw)
        /// </summary>
        public double[] BldKAWxi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���y�����ϵ�ҶƬK��������   unit:(Kw)
        /// </summary>
        public double[] BldKAWyi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���z�����ϵ�ҶƬK��������   unit:(Kw)
        /// </summary>
        public double[] BldKAWzi;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���x�����ϵ�ҶƬK�ڵ�J��������   unit:(Kw)
        /// </summary>
        public double[,] BkNjAWxt;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���y�����ϵ�ҶƬK�ڵ�J��������   unit:(Kw)
        /// </summary>
        public double[,] BkNjAWyt;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���z�����ϵ�ҶƬK�ڵ�J��������   unit:(Kw)
        /// </summary>
        public double[,] BkNjAWzt;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���x�����ϵ�ҶƬK��������   unit:(Kw)
        /// </summary>
        public double[] BldKAWxt;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���y�����ϵ�ҶƬK��������   unit:(Kw)
        /// </summary>
        public double[] BldKAWyt;
        /// <summary>
        /// ҶƬ�ֲ�����ϵ�£���z�����ϵ�ҶƬK��������   unit:(Kw)
        /// </summary>
        public double[] BldKAWzt;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���x�����ϵ����ܽڵ�J�����������   unit:(%)
        /// </summary>
        public double[] TwHtADxi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���y�����ϵ����ܽڵ�J�����������   unit:(%)
        /// </summary>
        public double[] TwHtADyi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���z�����ϵ����ܽڵ�J�����������   unit:(%)
        /// </summary>
        public double[] TwHtADzi;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���x�����ϵ����ܽڵ�J�����������   unit:(%)
        /// </summary>
        public double[] TwHtADxt;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���y�����ϵ����ܽڵ�J�����������   unit:(%)
        /// </summary>
        public double[] TwHtADyt;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���z�����ϵ����ܽڵ�J�����������   unit:(%)
        /// </summary>
        public double[] TwHtADzt;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���x�����ϵ��������������   unit:(%)
        /// </summary>
        public double TwrADxi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���y�����ϵ��������������   unit:(%)
        /// </summary>
        public double TwrADyi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���z�����ϵ��������������   unit:(%)
        /// </summary>
        public double TwrADzi;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���x�����ϵ��������������   unit:(%)
        /// </summary>
        public double TwrADxt;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���y�����ϵ��������������   unit:(%)
        /// </summary>
        public double TwrADyt;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���z�����ϵ��������������   unit:(%)
        /// </summary>
        public double TwrADzt;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���x�����ϵ����ܽڵ�J��������   unit:(Kw)
        /// </summary>
        public double[] TwHtAWxi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���y�����ϵ����ܽڵ�J��������   unit:(Kw)
        /// </summary>
        public double[] TwHtAWyi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���z�����ϵ����ܽڵ�J��������   unit:(Kw)
        /// </summary>
        public double[] TwHtAWzi;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���x�����ϵ����ܽڵ�J��������   unit:(Kw)
        /// </summary>
        public double[] TwHtAWxt;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���y�����ϵ����ܽڵ�J��������   unit:(Kw)
        /// </summary>
        public double[] TwHtAWyt;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���z�����ϵ����ܽڵ�J��������   unit:(Kw)
        /// </summary>
        public double[] TwHtAWzt;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���x�����ϵ�����������   unit:(Kw)
        /// </summary>
        public double TwrAWxi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���y�����ϵ�����������   unit:(Kw)
        /// </summary>
        public double TwrAWyi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���z�����ϵ�����������   unit:(Kw)
        /// </summary>
        public double TwrAWzi;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���x�����ϵ�����������   unit:(Kw)
        /// </summary>
        public double TwrAWxt;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���y�����ϵ�����������   unit:(Kw)
        /// </summary>
        public double TwrAWyt;
        /// <summary>
        /// ���ֲܾ�����ϵ�£���z�����ϵ�����������   unit:(Kw)
        /// </summary>
        public double TwrAWzt;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���x�����ϵķ������������   unit:(%)
        /// </summary>
        public double RtADxi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���y�����ϵķ������������   unit:(%)
        /// </summary>
        public double RtADyi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���z�����ϵķ������������   unit:(%)
        /// </summary>
        public double RtADzi;
        /// <summary>
        /// �������߾ֲ�����ϵ�£���x�����ϵķ������������   unit:(%)
        /// </summary>
        public double RtADxt;
        /// <summary>
        /// �������߾ֲ�����ϵ�£���y�����ϵķ������������   unit:(%)
        /// </summary>
        public double RtADyt;
        /// <summary>
        /// �������߾ֲ�����ϵ�£���z�����ϵķ������������   unit:(%)
        /// </summary>
        public double RtADzt;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���x�����ϵķ���������   unit:(Kw)
        /// </summary>
        public double RtAWxi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���y�����ϵķ���������   unit:(Kw)
        /// </summary>
        public double RtAWyi;
        /// <summary>
        /// ȫ�ֹ�������ϵ�£���z�����ϵķ���������   unit:(Kw)
        /// </summary>
        public double RtAWzi;
        /// <summary>
        /// �������߾ֲ�����ϵ�£���x�����ϵķ���������   unit:(Kw)
        /// </summary>
        public double RtAWxt;
        /// <summary>
        /// �������߾ֲ�����ϵ�£���y�����ϵķ���������   unit:(Kw)
        /// </summary>
        public double RtAWyt;
        /// <summary>
        /// �������߾ֲ�����ϵ�£���z�����ϵķ���������   unit:(Kw)
        /// </summary>
        public double RtAWzt;
    }
    /// <summary>
    /// �غ������ͨ��ö����,Ҫ��c#�Զ����ļ���Ӧ
    /// </summary>
    [SourceReflection]
    public enum MSA_Loadchannels
    {
        /// <summary>
        /// ҶƬ��ҶƬ�ڵ��ϵ������������������
        /// </summary>
        Blade_Aerodynamic_damping_And_Power = 0,
        /// <summary>
        /// ���ܺ����ܽڵ��ϵ������������������
        /// </summary>
        Tower_Aerodynamic_damping_And_Power = 1,
        /// <summary>
        /// ���ֵ������������������
        /// </summary>
        Rotor_Aerodynamic_damping_And_Power = 2,
    }
}
