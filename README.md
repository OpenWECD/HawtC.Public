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


##  HawtC 与 OpenFAST 计算验证对比

### 1. 15MW 稳态无风剪切

#### 1）验证结果

http://www.openwecd.fun/data/稳态无风剪切Compare.html

#### 2）验证程序

http://www.openwecd.fun/data/稳态Compare.ipynb

### 4.15MW 湍流风验证

#### 1）验证结果

http://www.openwecd.fun/data/湍流Compare.html

#### 2）验证程序

http://www.openwecd.fun/data/湍流Compare.ipynb

## HawtC 与 Bladed 4.11 计算验证对比
![Compare_Bladed4_11.jpg](./docs/Compare_Bladed4_11.jpg)


### 源代码下载
请访问[www.HawtC.cn](http://www.openwecd.fun/)

### 交流论坛
交流论坛 http://www.openwecd.fun:22304/

## 5MW海上漂浮式风力机oc3测试
![windturbine](./docs/image/TheoryManualandBarchMarkreport/12.gif)

