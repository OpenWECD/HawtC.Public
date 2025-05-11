[简体中文](./README.md) | [繁體中文](./README_CN.md) | [English](./README_EN.md) | [日本語](./README_JP.md)
![HawtC](./docs/image/TheoryManualandBarchMarkreport/图标.png)

##   If you wish to access the complete code and participate in the development of HawtC2, please join our organization.

## Advantages of HawtC

### Theoretical Innovation

*     
    1\. The kinematic transformation method based on quaternions breaks the small angle assumption of FAST[^1]/Bladed, achieving high-precision motion description and computation  
    
*     
    2\. A method for calculating cross-sectional characteristics with independent intellectual property rights, breaking the long-term monopoly of IVABS and BECAS.  
    
*     
    3\. A fully coupled efficient multi-objective optimization algorithm that supports aerodynamic-structural-control-hydrodynamic full-process optimization design, overcoming the inefficiency of traditional manual optimization design.  
    
*     
    4\. An anisotropic geometric nonlinear co-rotational beam method has been established based on the co-rotational method, breaking the inefficiency of the traditional Lagrange method and geometrically exact beam method, achieving large-step, high-precision nonlinear blade computation with fewer segments.  
    
*     
    5\. For the first time, kinematic and dynamic formulas for the blade and tower TMDI have been derived based on the Kane method and coupled into multibody dynamics, achieving full-link coupling computation of aerodynamic-structural-control-hydrodynamic-TMDI control. Utilizing the interface model APIL and multi-objective optimization module MoptL, a complex wind-wave-swell coupled blade TMDI multi-objective optimization design has been realized.  
    
*     
    6\. Tackled the coupling design challenge of ultra-long flexible blades, which involves blade laminates (structural design), blade airfoil stress (safety design), and blade aerodynamics (efficient aerodynamic shape design). Achieved the airfoil-blade-unit coupling design method for large units, and provided modeling and simulation tools.  
      
    

###   Technological Innovation  

*     
    1\. Completely 100% native code based on C#, employing object-oriented programming, breaking the foreign monopoly on simulation software.  
    
*     
    2\. Features a complete CLI system, supporting both interface and command bidirectional operations, offering executable commands for developers.  
    
*     
    3\. A new software framework based on the theory of commercial software Bladed, MBD 2.0 supports high-precision calculations for dual-head and single-point mooring dynamics.  
    
*     
    4\. Provides dynamic link libraries and manual support for users of Python/C++ and others, facilitating coupling with other software.  
    

##   Comparison of HawtC with OpenFAST/Bladed 4.11 in computational validation.

###   1\. Validation of the steady state with no wind shear for the 15MW IEA onshore, compared to OpenFAST.

####   1) Verification Results

[http://www.openwecd.fun/data/稳态无风剪切Compare.html](http://www.openwecd.fun/data/%E7%A8%B3%E6%80%81%E6%97%A0%E9%A3%8E%E5%89%AA%E5%88%87Compare.html)

####   2) Verification Procedure

[http://www.openwecd.fun/data/稳态Compare.ipynb](http://www.openwecd.fun/data/%E7%A8%B3%E6%80%81Compare.ipynb)

###   2\. Comparison of IEA 15MW Onshore Turbulent Wind Verification with OpenFAST

####   1) Verification Results

[http://www.openwecd.fun/data/湍流Compare.html](http://www.openwecd.fun/data/%E6%B9%8D%E6%B5%81Compare.html)

####   2) Verification Procedure

[http://www.openwecd.fun/data/湍流Compare.ipynb](http://www.openwecd.fun/data/%E6%B9%8D%E6%B5%81Compare.ipynb)

###   3\. Comparison of HawtC.AeroL Aerodynamic Module and Bladed 4.11 Computational Verification

![Compare_Bladed4_11.jpg](./docs/Compare_Bladed4_11.jpg)

###   4\. Testing of HawtC.MBD.VTK Multibody Dynamics Visualization on NREL 5MW Spar Offshore Floating Wind Turbine

![windturbine](./docs/image/TheoryManualandBarchMarkreport/12.webp)

###   5\. Verification of HawtC.BeamL Nonlinear Beam (3D Co-rotational Beam Theory) Module

![windturbine](./docs/image/TheoryManualandBarchMarkreport/BeamL.png)

###   6.HawtC.HydroL.Wave Hydrodynamic wave generation module verification

  
This module has been verified with Bladed 4.11

###   7.HawtC.HydroL.MoorL Hydrodynamic wave generation module verification

  
This module fully couples the OpenMoor[^2] and MoorDyn[^3] modules to calculate mooring forces. Meanwhile, our own mooring dynamics MoorL module is still in development to support shared mooring under wind conditions.![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case1-25.gif) ![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case3-5.gif)

  
The image is sourced from http://openmoor.org/

###   8.HawtC.WindL.IECWind IEC Wind Field Generation Module Validation

###   9.HawtC.WindL.SimWind Turbulent Wind Generation Module Validation

  
This module is functionally similar to the OpenFAST.TurbSim module, here is the ETM wind model:![ETMWind](./docs/image/TheoryManualandBarchMarkreport/wind.webp)

###  10.HawtC.MoptL Complete Integration Optimization Module Data-Driven Script

  
Please review the document to understand the example interface:

*     
    Interface for scripting languages (Python/R/Julia/Matlab): BP Neural Network Model: DemoBPNetWork.py
    
      
    Natural Neural Network Model: DemoBPNetWork.py
    
*     
    Compiled Language Interfaces (C/C++/Fortran/C#):
    
      
    C++ Interface Template: MoptL Data-Driven Case.sln
    

##  Source Code Download

  
Please visit www.HawtC.cn

##  Communication Forum

  
Discussion forum http://www.openwecd.fun:22304/

####   References
[^1]:https://github.com/OpenFAST/openfast
[^2]:Chen, L., Basu, B. & Nielsen, S.R.K. (2018). A coupled finite difference mooring dynamics model for floating offshore wind turbine analysis. Ocean Engineering,162, 304-315
[^3]:https://github.com/FloatingArrayDesign/MoorDyn?tab=readme-ov-file
