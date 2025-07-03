### [简体中文](./README.md) | [繁體中文](./README_FCN.md) | [English](./README_EN.md) | [日本語](./README_JP.md) | [한국어](./README_KO.md)

![HawtC](./docs/image/TheoryManualandBarchMarkreport/%E5%9B%BE%E6%A0%87.png)

## If you want to obtain the complete code and participate in the development of HawtC2, please join our organization

## Advantages of HawtC

### Theoretical Innovation

*   1\. The quaternion-based kinematic transformation method breaks the small-angle assumption of FAST[1](#user-content-fn-1)/Bladed, achieving high-precision motion description and computation.
*   2\. The proprietary sectional property calculation method breaks the long-term monopoly of IVABS and BECAS.
*   3\. The fully coupled efficient multi-objective optimization algorithm supports the full-process optimization design of aerodynamic-structural-control-hydrodynamic, breaking the inefficiency of traditional manual optimization design.
*   4\. The anisotropic geometric nonlinear co-rotational beam method based on the co-rotational approach breaks the inefficiency of traditional Lagrangian methods and geometrically exact beam methods, achieving fewer segments, larger step sizes, and high-precision nonlinear blade computation.
*   5\. For the first time, the kinematic and dynamic formulas of the blade and tower TMDI were derived based on the Kane method and coupled into multibody dynamics, achieving full-link coupling calculations of aerodynamic-structural-control-hydrodynamic-TMDI control. Through the interface model APIL and the multi-objective optimization module MoptL, a complex wind-wave coupling blade TMDI multi-objective optimization design was realized.
*   6\. A real-time data-driven and multi-objective coupling optimization algorithm was proposed, which, by establishing a real data reference vector, solved the problems of poor prediction results and weak model generalization ability in traditional data-driven methods, significantly improving optimization efficiency and prediction accuracy.
*   7\. The challenge of coupling design for ultra-long flexible blades, including blade layup (structural design), blade airfoil stress (safety design), and blade aerodynamics (efficient aerodynamic shape design), was overcome, realizing the design method for airfoil-blade-whole machine coupling of large units, and providing modeling and simulation tools.

### Technological Innovation

*   1\. Completely 100% native code based on C#, utilizing object-oriented programming to break the foreign monopoly on simulation software
*   2\. Features a complete CLI system, supporting bidirectional interface/command operations for developer-oriented executable commands
*   3\. Provides dynamic link libraries and manual support for Python/C++ users, facilitating coupling with other software

## 0\. How To Use?

HawtC is partially open-source and free-to-use computational software, requiring you to apply for a free license to use it. We provide an automated license management system; you only need to log in to our

website: [http://www.hawtc.cn](http://www.hawtc.cn) or [http://www.openwecd.fun/](http://www.openwecd.fun/) to get support!

![1751571229053](image/README/1751571229053.png)

## 01\. Current Development Progress and Features

Our goal is to achieve full condition coverage of Bladed and gradually develop the UI interface (technical verification has been completed! Using C# AOT to implement the interface, you can download HawtC.UI to experience the preview) and compare the implementation progress of Bladed's module functions:

#### 01.1 Function and Module Comparison

| Bladed Module | OpenFAST Corresponding Module | HawtC Corresponding Module | HawtC Progress and Support Status | HawtC Model |
| --- | --- | --- | --- | --- |
| Modal Analysis | Bmodes (Non-OpenFAST Module) | BeamL | ✅Basic implementation, still under development | ✅CR/✅TK/⚠️GEBT |
| Wind Turbulence | TurbSim | WindL.SimWind | ✅Completed | Harmonic superposition, wind spectrum model |
| Earthquake Generation | Earthquake | SubFEML | ❌ Planned, not developed | Linear Finite Element |
| Sea State | Sea State (V4.0.0) | HydroL.WaveL | ✅Completed | JS/PM Spectrum Model |
| Hydrodynamic Module | HydroDyn | HyderoL | ⚠️Only supports Spar platform | ❌Potential flow theory (planned development), ✅Morison equation |
| Aerodynamic Information | AeroDyn | AeroL/BeamL | ✅Completed | BEMT/FVM and dynamic stall Oye |
| Performance Coefficients | AeroDyn (does not support flexible Cp) | AeroL/BeamL | ✅ Completed, ❌ Flexible Cp not developed, MBD can be used as an alternative | \- |
| Steady Power Curve | AeroDyn | TurbineL | ✅Completed | \- |
| Steady Operational Loads | AeroDyn | TurbineL | ✅Completed | \- |
| Steady Parked Loads | AeroDyn | TurbineL | ✅Completed | Floating Coordinate Method |
| Model Linearization | FAST Main Module | MSAL/TurbineL | ⚠️ Under development... | \- |
| Electrical performance | \- | \- | ❌ Not supported | \- |
| Power Production Loading | BeamDyn/ElastoDyn | AeroL/MBD/ControL/HydroL/SubFEML/BeamL | ✅Completed | Coupled model |
| Normal Stop | \- | AeroL/MBD/ControL/HydroL/SubFEML/BeamL | ⚠️ Simulation is possible, but direct feature selection is not provided, under development | Coupled Model |
| Emergency Stop | \- | AeroL/MBD//ControL /HydroL/SubFEML/BeamL | ❌ Error control module is located within ControL, not yet developed | Coupled model |
| Idling | BeamDyn/ElastoDyn | AeroL/MBD /BeamL | ✅Completed | Coupled Model |
| Parked | BeamDyn/ElastoDyn | AeroL/MBD/HydroL/SubFEML/BeamL | ✅ Completed | Coupled Model |
| Hardware Test | \- | \- | ❌ Not Supported | \- |
| Post Processing | \- | PostL | ✅ Partial support (annual power generation, fatigue load, ultimate load, rainflow counting fully supported!) | S-N fatigue damage theory, rainflow counting method |
| Bladed API | pyOpenFAST | APIL | External application interface, unique and convenient | \- |
| Batch | ❌ Not supported | Batch | ⚠️ Supports batch processing and operation for most working conditions, but the code is not yet complete | \- |

#### 01.2 Unique Features

| Bladed Module | OpenFAST Module | Corresponding HawtC Module | Function | Principles and Models |
| --- | --- | --- | --- | --- |
| ❌ Not Supported | IVABS (Non-OpenFAST Module) | ✅ PCSL | Beam section parameter calculation tool, unique | FEM |
| ❌Not supported | ❌Not supported | ✅ MoptL | Multi-objective parallel optimization algorithm program, unique | NSGA2/GDE3/MCell (improved multithreaded C# implementation) |
| ❌Not supported | ❌Not supported | ✅ APIL/MoptL | Integrated optimization of all parameters of the whole machine, unique | Coupling model |
| ❌Not supported | ❌Not supported | ✅ WTAI/MoptL | Data-driven and real-time data-driven agent module, unique | Python, C++ interfaces and built-in BP neural network |
| ❌ Not supported | ✅ VTK supported | ✅ VTKL | Data display and animation output module | \- |
| ❌Only supports TMD | ⚠️Only supports TMD (supports basic/tower/blade structures, etc.) | ✅ TMD/TMDI (exclusive blade)/gyroscope | Vibration reduction calculation under TMD/TMDI/gyroscope | MBD/FEM Multibody Dynamics-Finite Element Coupling Model Coupling |

## Comparison of HawtC with OpenFAST/Bladed 4.11 Computational Validation

### 1\. Onshore IEA 15MW Steady-State No Wind Shear Validation Compared with OpenFAST

#### 1) Validation Results

[http://www.openwecd.fun/data/稳态无风剪切Compare.html](http://www.openwecd.fun/data/%E7%A8%B3%E6%80%81%E6%97%A0%E9%A3%8E%E5%89%AA%E5%88%87Compare.html)

#### 2) Verification Procedure

[http://www.openwecd.fun/data/稳态Compare.ipynb](http://www.openwecd.fun/data/%E7%A8%B3%E6%80%81Compare.ipynb)

### 2\. Verification of Onshore IEA 15MW Turbulent Wind Compared with OpenFAST

#### 1) Verification Results

[http://www.openwecd.fun/data/湍流Compare.html](http://www.openwecd.fun/data/%E6%B9%8D%E6%B5%81Compare.html)

#### 2) Verification Procedure

[http://www.openwecd.fun/data/湍流Compare.ipynb](http://www.openwecd.fun/data/%E6%B9%8D%E6%B5%81Compare.ipynb)

### 3\. HawtC.AeroL Aerodynamic Module and Bladed 4.11 Calculation Verification Comparison

![Compare_Bladed4_11.jpg](./docs/Compare_Bladed4_11.jpg)

### 4\. HawtC.MBD.VTK Multibody Dynamics Visualization of NREL 5MW Spar Offshore Floating Wind Turbine Test

![windturbine](./docs/image/TheoryManualandBarchMarkreport/wind-farm.webp)

#### HAWTC.FARM:

![111](./docs/image/TheoryManualandBarchMarkreport/133.webp)

### 5\. HawtC.BeamL Verification of Nonlinear Beam (3D Co-rotational Beam Theory) Module

![windturbine](./docs/image/TheoryManualandBarchMarkreport/BeamL.png)

### 6\. HawtC.HydroL.Wave Hydrodynamic Wave Generation Module Verification

This module has been verified with Bladed 4.11, as shown in the figure below: the left side shows the wave spectrum of Bladed 4.11, and the right side shows the wave spectrum calculated by HawtC2.

| Blade | HawtC |
| --- | --- |
|  |  |

### 7\. HawtC.HydroL.MoorL Hydrodynamic Wave Generation Module Validation

This module is fully coupled with the OpenMoor[2](#user-content-fn-2) and MoorDyn[3](#user-content-fn-3) modules to calculate mooring forces. Meanwhile, our own mooring dynamics MoorL module is still under development to support shared mooring under wind field conditions.![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case1-25.gif) ![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case3-5.gif)

Image source from [http://openmoor.org/](http://openmoor.org/)

### 8\. HawtC.PCSL Cross-Sectional Characteristics Calculation Validation

Beta V2.0.014 and subsequent versions support FEM method for calculating section properties and airfoil grid automation algorithms. The input file references the input format of the open-source software PreComp, but the algorithms are completely different. This package supports API function customization for calculation implementation.

#### Case 1: Custom grid analysis of a typical section (analyzing a rectangular cross-section)

##### 1\. Input file definition:

Main input file

![主输入文件](./demo/PCSL/%E4%BA%8C%E7%BB%B4%E6%9C%89%E9%99%90%E5%85%83%E6%96%B9%E6%B3%95%E8%AE%A1%E7%AE%97/BECAS_Case1%E9%AA%8C%E8%AF%81/Becas_Test1_MainFile.pcs)

##### 2\. Material Input File Definition:

Material Input File

![材料输入文件](./demo/PCSL/%E4%BA%8C%E7%BB%B4%E6%9C%89%E9%99%90%E5%85%83%E6%96%B9%E6%B3%95%E8%AE%A1%E7%AE%97/BECAS_Case1%E9%AA%8C%E8%AF%81/materials.inp)

##### 3\. PCSL Calculation:

Case Grid:

![1750150781639](image/README/1750150781639.png)

Calculation results:

![计算结果](./demo/PCSL/%E4%BA%8C%E7%BB%B4%E6%9C%89%E9%99%90%E5%85%83%E6%96%B9%E6%B3%95%E8%AE%A1%E7%AE%97/BECAS_Case1%E9%AA%8C%E8%AF%81/Result1/SectionInf_%E5%A4%8D%E5%90%88%E6%9D%90%E6%96%99%E5%8F%B6%E7%89%87%E6%88%AA%E9%9D%A2%E5%B1%9E%E6%80%A7%E7%A4%BA%E4%BE%8B.out)

This example is cross-validated with BECAS, and the calculation results are completely consistent!

#### Case 2: Automated mesh generation and calculation for irregular cross-sections (using airfoil as an example)

##### 1\. Input main file definition:

Main input file

![主输入文件](./demo/PCSL/%E4%BA%8C%E7%BB%B4%E6%9C%89%E9%99%90%E5%85%83%E6%96%B9%E6%B3%95%E8%AE%A1%E7%AE%97/BECAS_Case3%E7%BF%BC%E5%9E%8B%E9%AA%8C%E8%AF%81/Becas_Test3_MainFile.pcs)

The initial upper and lower camber line geometry of the airfoil is:

![1750151211121](image/README/1750151211121.png)

##### 3\. The mesh generated based on the PCSL grid automation algorithm is:

![1750151905931](image/README/1750151905931.png)

###### 3.1 Sectional characteristic analysis calculation:

Calculation Results

![!计算结果](./demo/PCSL/%E4%BA%8C%E7%BB%B4%E6%9C%89%E9%99%90%E5%85%83%E6%96%B9%E6%B3%95%E8%AE%A1%E7%AE%97/BECAS_Case3%E7%BF%BC%E5%9E%8B%E9%AA%8C%E8%AF%81/Result1/SectionInf_%E5%A4%8D%E5%90%88%E6%9D%90%E6%96%99%E5%8F%B6%E7%89%87%E6%88%AA%E9%9D%A2%E5%B1%9E%E6%80%A7%E7%A4%BA%E4%BE%8B%E5%BC%A6%E9%95%BF%E6%96%B9%E5%90%91%E6%8F%92%E5%80%BC%E6%96%B0%E7%9A%84%E7%89%88%E6%9C%AC.out)

###### 3.2 Cross-sectional Stress and Strain Analysis:

PCSL supports the calculation of cross-sectional stress/strain and failure analysis methods under external force input. The main file for stress/strain and failure analysis of the aforementioned airfoil:

![应力/应变以及失效分析主文件](./demo/PCSL/%E4%BA%8C%E7%BB%B4%E6%9C%89%E9%99%90%E5%85%83%E6%96%B9%E6%B3%95%E8%AE%A1%E7%AE%97/BECAS_Case3%E7%BF%BC%E5%9E%8B%E9%AA%8C%E8%AF%81/Becas_Test3_MainFile_%E8%AE%A1%E7%AE%97%E5%BA%94%E5%8A%9B%E5%92%8C%E5%BA%94%E5%8F%98.pcs)Strain Analysis:

![1750152148363](image/README/1750152148363.png)

Stress Analysis:

![1750152198505](image/README/1750152198505.png)

Automatic Mesh Encryption:![1750171439541](image/README/1750171439541.png)

![1750171535423](image/README/1750171535423.png)

![1750171585343](image/README/1750171585343.png)

Failure Analysis:

![失效分析计算](./demo/PCSL/%E4%BA%8C%E7%BB%B4%E6%9C%89%E9%99%90%E5%85%83%E6%96%B9%E6%B3%95%E8%AE%A1%E7%AE%97/BECAS_Case3%E7%BF%BC%E5%9E%8B%E9%AA%8C%E8%AF%81/Result1/SectionFailure_Force_0_0_0_20000_10000_0_SectionMESH_1_%E5%A4%8D%E5%90%88%E6%9D%90%E6%96%99%E5%8F%B6%E7%89%87%E6%88%AA%E9%9D%A2%E5%B1%9E%E6%80%A7%E7%A4%BA%E4%BE%8B%E5%BC%A6%E9%95%BF%E6%96%B9%E5%90%91%E6%8F%92%E5%80%BC%E6%96%B0%E7%9A%84%E7%89%88%E6%9C%AC.out)

##### 4\. Current Issues

The current version V2.0.014 uses Q4 elements for discretizing the cross-section. The current model cannot consider higher-order interpolation functions, resulting in relatively poor accuracy in the calculation of coefficients related to bending and shear (though far more accurate than PreComp). We will address this issue by introducing Q8 elements in version 2.0.015. However, to accelerate computation, considering the closed shell structure of the blade elements and the small strain characteristics of the blade, we have ignored the energy of cross-section warping in the code. If you need to calculate non-closed sections, please wait for the annual update of the major version V2.1.000!

### 9\. HawtC.WindL.SimWind Turbulent Wind Generation Module Verification

This module is similar in functionality to the OpenFAST.TurbSim module. Below is the ETM wind model:

![ETMWind](./docs/image/TheoryManualandBarchMarkreport/wind.webp)

### 10\. HawtC.MoptL Integrated Optimization Module Data-Driven Script

Please refer to the document for example interfaces:

*   Script language interfaces (Python/R/Julia/Matlab): BP neural network model: [DemoBPNetWork.py](./data/Mopt/Python%E8%84%9A%E6%9C%AC/DemoBPNetWork.py)
    
    Natural neural network model: [DemoBPNetWork.py](./data/Mopt/Python%E8%84%9A%E6%9C%AC/DemoBPNetWork.py)
    
*   Compiled language interfaces (C/C++/Fortran/c#):
    
    C++ Interface Template: [MoptL Data-Driven Case.sln](./data/Mopt/C++%E8%84%9A%E6%9C%AC/MoptL%E6%95%B0%E6%8D%AE%E9%A9%B1%E5%8A%A8%E6%A1%88%E4%BE%8B/MoptL%E6%95%B0%E6%8D%AE%E9%A9%B1%E5%8A%A8%E6%A1%88%E4%BE%8B.sln)
    

## Source Code Download

Please visit [www.HawtC.cn](http://www.openwecd.fun/)

## Discussion Forum

Communication Forum [http://www.openwecd.fun:22304/](http://www.openwecd.fun:22304/)

#### References

## Footnotes

1.  [https://github.com/OpenFAST/openfast](https://github.com/OpenFAST/openfast) [↩](#user-content-fnref-1)
    
2.  Chen, L., Basu, B. & Nielsen, S.R.K. (2018). A coupled finite difference mooring dynamics model for floating offshore wind turbine analysis. Ocean Engineering, 162, 304-315 [↩](#user-content-fnref-2)
    
3.  [https://github.com/FloatingArrayDesign/MoorDyn?tab=readme-ov-file](https://github.com/FloatingArrayDesign/MoorDyn?tab=readme-ov-file) [↩](#user-content-fnref-3)