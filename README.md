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

#### 案例1 典型截面的自定义网格分析(以矩形横截面分析)

##### 1、输入文件定义:
主输入文件[!主输入文件](./demo/PCSL/二维有限元方法计算/BECAS_Case1验证/Becas_Test1_MainFile.pcs)

``` CSharp
********************  PCSL 主输入文件（HawtC2.中文版） **********************
复合材料叶片截面属性示例
!这个空行必须要
-------------------------------- 基本信息 -----------------------------------------------
20.15    Bl_length        -    叶片总长（米）
0         N_sections      -    叶片截面总数（-）
1           N_materials    -    材料表中列出的材料数量（见material.inp文件）
3           Out_format       -    输出文件格式（1: 通用格式，2: BModes格式，3: 双格式，4:矩阵形式，5: 三格式）
true       TabDelim        -  （真：制表符分隔表格；假：空格分隔表格）
true      FEMSove       -     是否启用二维有限元方法计算截面特性参数
true      SelfMesh           -     自定义网格参数，需要注意的是，如果你的计算目标不是翼型，该选项必须设置为true，设置true才支持自定义截面样式格式，且这个选项为true，一次只能计算一个截面
false       CalStress         -  是否计算内力(必须有输出的Catch以及Node文件，开启后，不会计算截面参数！！！)
"./materials.inp"     MaterialsPath  - 材料文件的路径

-------------------------------- FEM 方法参数 -----------------------------------------------
true      SelfMesh           -     自定义网格参数，需要注意的是，如果你的计算目标不是翼型，该选项必须设置为true，设置true才支持自定义截面样式格式，且这个选项为true，一次只能计算一个截面
true      Shear_Center    -     得到的刚度矩阵是否关于剪切中心进行转换
true       Shear_Center    -     得到的刚度矩阵是否关于剪切中心进行转换
true        VTKShow            -      是否输出网格VTK可视化文件
true        SaveCatch                      - 是否保存网格矩阵信息以便计算翼型的应力
4          MeshWeb         -     腹板的网格数量{only used when SelfMesh=false}
-1          SurMeshExpT      -     翼面网格在厚度方向上的加密离散化数量(当上下翼面的针对材料铺层不一致时需要对网格进行统一以拼接上下网格的最小离散数量，-1表示不加密){only used when SelfMesh=false}
-1           SurMeshExpC      -     翼面网格在弦长方向上的加密离散化数量(-1表示不加密){only used when SelfMesh=false}
0.3,0.95       SurMeshExpCNorm   -  翼面网格在弦长方向上的加密的弦向归一化加密区间位置{only used when SelfMesh=false}
true        OutMeshfFile      -       是否将自动生成的网格信息输出为PcsL可以识别的网格数组文件，以供研究人员自动调整网格{only used when SelfMesh=false}
true        ConsidertwistAndpitch  - 是否考虑结构、气动扭角以及变桨轴线的影响{only used when SelfMesh=false}

-------------------------------- 叶片截面数据 ------------------------------------------
截面跨度      气动中心         弦长      气动扭转角    翼型文件         内部结构
位置              位置                                                                                 铺层文件
(Span_loc)    (Le_loc)   (Chord)  (Tw_aero)   (Af_shape_file)  (Int_str_file)
  (-)          (-)        (m)     (deg)           (-)             (-)
!这个空行必须要
!这个空行必须要
-------------------------------- 腹板（梁）数据 ----------------------------------------
!
2          Nweb         - 腹板总数（-）! 若无腹板填0
7        Ib_sp_stn      -   腹板最内端对应的叶片截面编号（-）
16       Ob_sp_stn   -  腹板最外端对应的叶片截面编号（-）

腹板编号   内端弦向位置    外端弦向位置（弦长比例）(Web_num)  (Inb_end_ch_loc)  (Oub_end_ch_loc)
1              0.15            0.15
2              0.50            0.50

======  Outputs  =============================================
"./Result1/"                                SumPath   - 生成的文件夹名称，注意是文件夹！  

-------------------------------- 自定义截面内力计算------------------------------------（used only  CalStress=true）
"./Result1/SectionMesh_1_复合材料叶片截面属性示例弦长方向插值新的版本.out"         StrainMeshFile    -  用于计算的内应力和应变网格文件
"./Result1/SectionCatch_1_复合材料叶片截面属性示例弦长方向插值新的版本.yaml"       StrainCache        -  用于计算的内应力和应变的预先矩阵文件
false                        CalFailure               - 是否进行材料失效计算
0                            FailureCriterion     - 翼型失效准则，可以多选，需要补充材料的失效特性{0：Tsai-Wu 失效准则；1：最大应力准则；2：Tsai-Hill 准则；3：Hashin 准则；4：Hoffman准则}
2                            ExtForceNum         - 外部力的数量(Fx,Fy,Fz,Mx,My,Mz)的顺序
0  0   0   2e4   1e4 0     !BeginEXTFORCE  开始力的矩阵的标志
0  0   0   3e4   4e4 0    


-------------------------------- 自定义网格 ----------------------------------------!由于网格与节点单元数量太多，请使用程序生成，手动不太行，未来我计划支持Ansys一笔Abcus当中的网格格式
441                                   FEMNodeNum        -   自定义网格当中的有限元节点数量
400                                   FEMElementNum   -  自定义网格当中的单元数量
!BeginNode  (节点编号，从零开始 ； 节点x坐标  ；节点y坐标)
0	-0.050 	-0.050 
1	-0.050 	-0.045 
2	-0.050 	-0.040 
.....
438	0.050 	0.040 
439	0.050 	0.045 
440	0.050 	0.050 

!BeginElement(单元，从零开始 ； 节点编号1  ； 节点编号2 ； 节点编号3 ； 节点编号4；材料编号 ；铺层角度 )
0	0	21	22	1	1	0
1	1	22	23	2	1	0
2	2	23	24	3	1	0
.....
398	417	438	439	418	1	0
399	418	439	440	419	1	0

```
##### 2、材料输入文件定义:
材料输入文件[!材料输入文件](./demo/PCSL/二维有限元方法计算/BECAS_Case1验证/materials.inp)
``` Csharp
Id   E1       E2         E3      G12   G13  G23    Nu12      Nu13      Nu23    Density      Name
 (-)                         (Pa)          (Pa)          (Pa)                       (-)          (-)         (-)                 (-)                   (-)               (-)                (kg/m³)           (-)
!这一行必须有
1                             100           100             100             41.667      41.667    41.667           0.2                 0.2              0.2           1000         #1各向同性材料
 

```
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
失效分析:
![失效分析计算](./demo/PCSL/二维有限元方法计算/BECAS_Case3翼型验证/Result1/SectionFailure_Force_0_0_0_20000_10000_0_SectionMESH_1_复合材料叶片截面属性示例弦长方向插值新的版本.out)

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
