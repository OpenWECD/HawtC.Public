### [简体中文](./README.md) | [繁體中文](./README_FCN.md) | [English](./README_EN.md) | [日本語](./README_JP.md)
</br>
</br>

![HawtC](./docs/image/TheoryManualandBarchMarkreport/图标.png)
## 如果你想获取完整代码，并参加HawtC2的开发，请加入我们的组织
## HawtC 优势
### 理论创新
- 1、基于四元数的运动学变换方法，打破FAST[^1] /Bladed的小角度假设，实现了高精度运动描述和计算</br>
- 2、具有自主知识产权的截面特性计算方法，打破IVABS 和BECAS 的长期垄断</br>
- 3、全耦合的高效多目标优化算法，支持气动-结构-控制-水动力全流程优化设计，打破传统人工优化设计的低效性</br>
- 4、基于共旋方法建立了各向异性几何非线性共旋梁方法，打破了传统拉格朗日方法和几何精确梁方法的低效性，实现了少分段、大步长、高精度的叶片非线性计算。</br>
- 5、首次基于Kane方法推导了叶片、塔架TMDI的运动学和动力学公式，并将其耦合到了多体动力学当中，实现了气动-结构-控制-水动力-TMDI控制的全链路耦合计算。借助接口模型APIL与多目标优化模块MoptL实现了复杂风-波-浪耦合的叶片TMDI多目标优化设计</br>
- 6、提出了实时数据驱动与多目标耦合的优化算法,通过建立真实数据参考向量,解决了传统数据驱动方法预测结果差,模型泛化能力弱的问题,大幅提高了优化效率和预测精度.
- 7、攻克了叶片铺层(结构设计)-叶片翼型应力(安全性设计)-叶片气动(高效气动外形设计)的超长柔性叶片耦合设计难题，实现了大型机组的翼型-叶片-整机耦合的设计方法，并提供了建模和仿真工具。</br>
</br>

### 技术创新</br>

- 1、完全的100%基于c#的原生代码，采用面向对象的编程形式，打破仿真软件的国外垄断</br>
- 2、具有完全的CLI系统、支持界面/命令双向操作的面向开发者的可执行命令</br>
- 3、基于商业软件Bladed理论的全新软件框架，MBD 2.0支持双机头与单点系泊动力学的高精度计算</br>
- 4、提供了面向Python/c++等用户的动态链接库和手册支持，方便与其他软件耦合</br>

## 3、重大更新预告
- 我计划在2.0.016 仍然以PCS模块的Bug修复和精确度开发为主，现在的版本为不考虑翘曲的简化版本，我们将在下一个版本提供非简化的选择
- 我计划在2.0.017版本开始逐步开发稳定性分析模块 MSA，该模块主要包含了动力学线性化分析以及时域下的气动阻尼分析：
##### 1）在这个版本当中我们首先实现了气动功与气动阻尼的计算和输出，以评判叶片的失稳分险，探讨叶片在TMDI作用下的不稳定现象，揭示增稳机理。主要是针对大论文的创新点3提供计算工具
##### 2）模态线性化模块，该模块可以对整机模型进行线性化以分析结构动力学特征，并输出相关的参数，该模块将会逐步开发完善。但不是当前重点，开发时间可能与年底或者毕业后开始。
##### 3）叶片的稳定性分析会跟随BeamL的开发进度进行规划，目前可以计算不同转速下的魔套和频率特征，我们将在未来更新当中支持气动-结构的稳定性耦合分析，主要包含颤振分析以及多风速下的载荷分析，该功能与Bladed 当中的Steady Operational Loads模块一致。

## 0、当前开发进度与功能
我们的目标是实现Bladed的全工况覆盖，并逐步开发UI界面（技术验证已经完成！，使用C# AOT来实现界面，可下载HawtC.UI来体验预览）
与Bladed的模块功能对比实现进度：

#### 0.1 功能与模块对比
| Bladed模块      |HawtC对应模块          |完成进度与支持情况             |HawtC模型|
| :----:     | :----:         | :----:          | :----:          |
|Modal Analysis          |            BeamL            |           :white_check_mark:基本实现，还在开发|  :white_check_mark:CR/:white_check_mark:TK/:warning:GEBT|
|Wind Turbulence        |       WindL.SimWind     |    :white_check_mark:完成  |     谐波叠加、风谱模型   |
|Earthquake Generation      |       SubFEML     |     :x: 已规划，未开发  |  线性有限元 | 
|Sea State      |       HydroL.WaveL     |    :white_check_mark:完成  | JS/PM 谱模型|
|水动力模块| HyderoL |  :warning:只支持Spar平台| :x:势流理论（计划开发）、:white_check_mark:Morison方程|
|Aerodynamic Information     |       AeroL/MBD    |    :white_check_mark:完成   | BEMT/FVM 以及动态失速Oye|
|Performance Coefficients    |       AeroL    |    :white_check_mark:完成,x: 柔性Cp未开发，可以使用MBD平替   |-|
|Steady Power Curve     |       AeroL/MBD    |    :white_check_mark:完成   |-|
|Steady Operational Loads     |       AeroL/MBD    |    :white_check_mark:完成   |-|
|Steady Parked Loads     |       AeroL/MBD    |    :white_check_mark:完成   |Kane多体动力学/FEM有限元耦合|
|Model Linearisation    |       MSAL    |    :warning: 正在开发当中。。。  | -|
|Electrical performance    |      -    |    :x: 不支持   |-|
|Power Production Loading     |       AeroL/MBD /ControL /HydroL/SubFEML/BeamL   |    :white_check_mark:完成   |耦合模型|
|Normal Stop     |       AeroL/MBD /ControL /HydroL/SubFEML/BeamL   |    :warning: 可以模拟，但是没有直接提供功能选择，开发中   |耦合模型|
|Emergency Stop   |       AeroL/MBD//ControL /HydroL/SubFEML/BeamL   |     :x: 错误控制模块位于ControL当中，尚未开发  |耦合模型|
|Idling    |       AeroL/MBD /BeamL   |    :white_check_mark:完成   |耦合模型|
|Parked    |       AeroL/MBD /HydroL/SubFEML/BeamL   |    :white_check_mark:完成   |耦合模型|
|Hardware Test   |       -   |    :x: 不支持  |-|
|Post Processing   |     PostL   |    :white_check_mark: 部分支持（年发电量、疲劳载荷，极限载荷，雨流计数以完全支持！）  |
|Bladed API |     APIL  |    外部应用接口，独有且便捷  |


#### 0.2 独有功能
| Bladed模块      |HawtC对应模块          |功能            |原理与模型|
| :----:     | :----:         | :----:          | :----:          |
| :x:不支持   |     :white_check_mark: PCSL  |    梁截面参数计算工具，独有  |FEM|
| :x:不支持  |     :white_check_mark: MoptL |    多目标并行优化算法程序，独有 |NSGA2/GDE3/MCell|
| :x:不支持  |     :white_check_mark: APIL/MoptL |  整机全参数一体化优化，独有 |耦合模型|
| :x:不支持  |    :white_check_mark: WTAI/MoptL |  数据驱动与实时数据驱动代理模块，独有 |Python、C++接口以及内置BP神经网络|
| :x:不支持  |    :white_check_mark: VTKL |  数据显示与动画输出模块 |-|
##  HawtC 与 OpenFAST/Bladed 4.11 计算验证对比

### 1. 与OpenFAST对比的陆上IEA 15MW 稳态无风剪切验证

#### 1）验证结果

http://www.openwecd.fun/data/稳态无风剪切Compare.html

#### 2）验证程序

http://www.openwecd.fun/data/稳态Compare.ipynb

### 2.与OpenFAST对比的陆上IEA 15MW 湍流风的验证

#### 1）验证结果

http://www.openwecd.fun/data/湍流Compare.html

#### 2）验证程序

http://www.openwecd.fun/data/湍流Compare.ipynb

### 3.HawtC.AeroL 气动力模块 与 Bladed 4.11 计算验证对比
![Compare_Bladed4_11.jpg](./docs/Compare_Bladed4_11.jpg)

### 4.HawtC.MBD.VTK 多体动力学可视化的NREL 5MW Spar海上漂浮式风力机测试
![windturbine](./docs/image/TheoryManualandBarchMarkreport/wind-farm.webp)
<br/>
<br/>
#### HAWTC.FARM:

![111](./docs/image/TheoryManualandBarchMarkreport/133.webp)


### 5.HawtC.BeamL 非线性梁(3D共旋梁理论)模块的验证

![windturbine](./docs/image/TheoryManualandBarchMarkreport/BeamL.png)

### 6.HawtC.HydroL.Wave 水动力波浪生成模块验证
该模块已经通过了与Bladed 4.11的验证,如下图所示:左面为Bladed4.11的波浪谱,有图为HawtC2计算的波浪谱.

| Blade	| HawtC|
|----|-----|
|![Blade](./docs/image/TheoryManualandBarchMarkreport/截图_20250412034847.png)|![HawtC](./docs/image/TheoryManualandBarchMarkreport/截图_20250412034808.png)|




### 7.HawtC.HydroL.MoorL 水动力波浪生成模块验证
该模块完全耦合了OpenMoor[^2]与MoorDyn[^3]模块,以计算系泊力.同时，我们自己的系泊动力学MoorL模块还在开发当中，以支持风场状态下的共享系泊。
![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case1-25.gif)
![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case3-5.gif)

图片来源于[http://openmoor.org/](http://openmoor.org/)


### 8.HawtC.PCSL 截面特性计算验证
Beta V2.0.014及其之后版本支持FEM方法计算截面特性以及翼型网格自动化算法,输入文件参考了PreComp开源软件的输入格式,但是算法完全不一致.该软件包支持API函数自定义计算实现.
#### 案例1 典型截面的自定义网格分析(以矩形横截面分析)

##### 1、输入文件定义:
主输入文件

![主输入文件](./demo/PCSL/二维有限元方法计算/BECAS_Case1验证/Becas_Test1_MainFile.pcs)


##### 2、材料输入文件定义:
材料输入文件

![材料输入文件](./demo/PCSL/二维有限元方法计算/BECAS_Case1验证/materials.inp)

 ##### 3、PCSL 计算:
案例网格:

![1750150781639](image/README/1750150781639.png)

计算结果:

![计算结果](./demo/PCSL/二维有限元方法计算/BECAS_Case1验证/Result1/SectionInf_复合材料叶片截面属性示例.out)

该算例以和BECAS交叉验证,计算结果完全一致!

#### 案例2 异形截面的自动化网格生成与计算(以翼型为例)
 ##### 1、输入主文件定义:
主输入文件

![主输入文件](./demo/PCSL/二维有限元方法计算/BECAS_Case3翼型验证/Becas_Test3_MainFile.pcs)

初始的翼型上下弦线几何外形为:

![1750151211121](image/README/1750151211121.png)

##### 3、基于PCSL网格自动化算法生成的网格为:

![1750151905931](image/README/1750151905931.png)

###### 3.1 截面特性分析计算:
计算结果

![!计算结果](./demo/PCSL/二维有限元方法计算/BECAS_Case3翼型验证/Result1/SectionInf_复合材料叶片截面属性示例弦长方向插值新的版本.out)

###### 3.2 截面应力与应变分析:
PCSL支持外部力输入下的截面应力/应变计算及其失效分析计算方法.
上述翼型的应力/应变以及失效分析主文件:

![应力/应变以及失效分析主文件](./demo/PCSL/二维有限元方法计算/BECAS_Case3翼型验证/Becas_Test3_MainFile_计算应力和应变.pcs)
应变分析:

![1750152148363](image/README/1750152148363.png)

应力分析:

![1750152198505](image/README/1750152198505.png)

网格自动加密:
![1750171439541](image/README/1750171439541.png)

![1750171535423](image/README/1750171535423.png)

![1750171585343](image/README/1750171585343.png)

失效分析:

![失效分析计算](./demo/PCSL/二维有限元方法计算/BECAS_Case3翼型验证/Result1/SectionFailure_Force_0_0_0_20000_10000_0_SectionMESH_1_复合材料叶片截面属性示例弦长方向插值新的版本.out)

##### 4、当前的问题

当前V2.0.014版本采用Q4单元对截面进行离散，当前模型不能考虑高阶插值函数，使得与弯曲和剪切相关的系数计算精度相对较差(但远比PreComp精确)。该问题我们将在2.0.015版本当中引入Q8单元来解决。但是,为了加速计算,我们考虑到叶片单元的闭合壳结构和叶片的小应变特点,我们在代码当中忽略了截面翘曲的能量.如果需要计算非闭合截面,请等待年度更新的V2.1.000 大版本!

### 9.HawtC.WindL.SimWind 湍流风生成模块验证
该模块与OpenFAST.TurbSim 模块功能类似,下面是ETM风模型:

![ETMWind](./docs/image/TheoryManualandBarchMarkreport/wind.webp)


### 10.HawtC.MoptL 整机一体化优化模块数据驱动脚本
请查阅文件,了解范例接口:
- 脚本类语言接口(Python/R/Julia/Matlab):
    BP神经网络模型:[DemoBPNetWork.py](./data/Mopt/Python脚本/DemoBPNetWork.py)

    自然神经网络模型:[DemoBPNetWork.py](./data/Mopt/Python脚本/DemoBPNetWork.py)

- 编译形语言接口(C/C++/Fortran/c#):

    c++接口模版:[MoptL数据驱动案例.sln](./data/Mopt/C++脚本/MoptL数据驱动案例/MoptL数据驱动案例.sln)

## 源代码下载
请访问[www.HawtC.cn](http://www.openwecd.fun/)

## 交流论坛
交流论坛 http://www.openwecd.fun:22304/

#### 参考文献
[^1]:https://github.com/OpenFAST/openfast
[^2]:Chen, L., Basu, B. & Nielsen, S.R.K. (2018). A coupled finite difference mooring dynamics model for floating offshore wind turbine analysis. Ocean Engineering,162, 304-315
[^3]:https://github.com/FloatingArrayDesign/MoorDyn?tab=readme-ov-file
