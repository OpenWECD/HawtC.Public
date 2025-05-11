## 如果你想获取完整代码，并参加HawtC2的开发，请加入我们的组织
## HawtC 优势
### 理论创新
- 1、基于四元数的运动学变换方法，打破FAST/Bladed的小角度假设，实现了高精度运动描述和计算</br>
- 2、具有自主知识产权的截面特性计算方法，打破IVABS 和BECAS 的长期垄断</br>
- 3、全耦合的高效多目标优化算法，支持气动-结构-控制-水动力全流程优化设计，打破传统人工优化设计的低效性</br>
- 4、基于共旋方法建立了各向异性几何非线性共旋梁方法，打破了传统拉格朗日方法和几何精确梁方法的低效性，实现了少分段、大步长、高精度的叶片非线性计算。</br>
- 5、首次基于Kane方法推导了叶片、塔架TMDI的运动学和动力学公式，并将其耦合到了多体动力学当中，实现了气动-结构-控制-水动力-TMDI控制的全链路耦合计算。借助接口模型APIL与多目标优化模块MoptL实现了复杂风-波-浪耦合的叶片TMDI多目标优化设计</br>
- 6、攻克了叶片铺层(结构设计)-叶片翼型应力(安全性设计)-叶片气动(高效气动外形设计)的超长柔性叶片耦合设计难题，实现了大型机组的翼型-叶片-整机耦合的设计方法，并提供了建模和仿真工具。</br>
</br>

### 技术创新</br>

- 1、完全的100%基于c#的原生代码，采用面向对象的编程形式，打破仿真软件的国外垄断</br>
- 2、具有完全的CLI系统、支持界面/命令双向操作的面向开发者的可执行命令</br>
- 3、基于商业软件Bladed理论的全新软件框架，MBD 2.0支持双机头与单点系泊动力学的高精度计算</br>
- 4、提供了面向Python/c++等用户的动态链接库和手册支持，方便与其他软件耦合</br>


##  HawtC 与 OpenFAST/Bladed 4.11 计算验证对比

### 1. 陆上IEA 15MW 稳态无风剪切的OpenFAST验证

#### 1）验证结果

http://www.openwecd.fun/data/稳态无风剪切Compare.html

#### 2）验证程序

http://www.openwecd.fun/data/稳态Compare.ipynb

### 2.陆上IEA 15MW 湍流风的OpenFAST验证

#### 1）验证结果

http://www.openwecd.fun/data/湍流Compare.html

#### 2）验证程序

http://www.openwecd.fun/data/湍流Compare.ipynb

### 3.HawtC.AeroL 气动力模块 与 Bladed 4.11 计算验证对比
![Compare_Bladed4_11.jpg](./docs/Compare_Bladed4_11.jpg)

### 4.HawtC.MBD.VTK 多体动力学可视化的NREL 5MW Spar海上漂浮式风力机测试
![windturbine](./docs/image/TheoryManualandBarchMarkreport/12.gif)

### 5.HawtC.BeamL 非线性梁(3D共旋梁理论)模块的验证
![windturbine](./docs/image/TheoryManualandBarchMarkreport/BeamL.png)

### 6.HawtC.HydroL.Wave 水动力波浪生成模块验证
该模块已经通过了与Bladed 4.11的验证

### 7.HawtC.HydroL.MoorL 水动力波浪生成模块验证
该模块完全耦合了OpenMoor [^1] 与MoorDyn [^2] 模块,以计算系泊力.同时，我们自己的系泊动力学MoorL模块还在开发当中，以支持风场状态下的共享系泊。
![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case1-25.gif)
![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case3-5.gif)

图片来源于[http://openmoor.org/](http://openmoor.org/)


### 8.HawtC.WindL.IECWind IEC风场生成模块验证


### 9.HawtC.WindL.SimWind 湍流风生成模块验证
该模块与OpenFAST.TurbSim 模块功能类似,下面是ETM风模型:
![wind](./docs/image/TheoryManualandBarchMarkreport/wind%2000_00_00-00_00_43~2.gif)


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
[^1]:Chen, L., Basu, B. & Nielsen, S.R.K. (2018). A coupled finite difference mooring dynamics model for floating offshore wind turbine analysis. Ocean Engineering,162, 304-315
[^2]:https://github.com/FloatingArrayDesign/MoorDyn?tab=readme-ov-file
