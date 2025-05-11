[简体中文](./README.md) | [繁體中文](./README_CN.md) | [English](./README_EN.md) | [日本語](./README_JP.md)
![HawtC](./docs/image/TheoryManualandBarchMarkreport/图标.png)
##  如果你想獲取完整代碼，並參加 HawtC2 的開發，請加入我們的組織

##  HawtC 優勢

###  理論創新

*     
    1、基於四元數的運動學變換方法，打破 FAST1/Bladed 的小角度假設，實現了高精度運動描述和計算  
    
*     
    2、有自主知識產權的截面特性計算方法，打破 IVABS 和 BECAS 的長期壟斷  
    
*     
    3、全耦合的高效多目標優化算法，支持氣動-結構-控制-水動力全流程優化設計，打破傳統人工優化設計的低效性  
    
*     
    4、基於共旋方法建立了各向異性幾何非線性共旋梁方法，打破了傳統拉格朗日方法和幾何精確梁方法的低效性，實現了少分段、大步長、高精度的葉片非線性計算。  
    
*     
    5、首次基於 Kane 方法推導了葉片、塔架 TMDI 的運動學和動力學公式，並將其耦合到多體動力學當中，實現了氣動-結構-控制-水動力-TMDI 控制的全鏈路耦合計算。借助接口模型 APIL 與多目標優化模塊 MoptL 實現了複雜風-波-浪耦合的葉片 TMDI 多目標優化設計  
    
*     
    6、攻克了葉片鋪層（結構設計）-葉片翼型應力（安全性設計）-葉片氣動（高效氣動外形設計）的超長柔性葉片耦合設計難題，實現了大型機組的翼型-葉片-整機耦合的設計方法，並提供了建模和仿真工具。  
      
    

###   技術創新  

*     
    1、完全的 100%基於 c#的原生代碼，採用面向對象的編程形式，打破仿真軟件的國外壟斷  
    
*     
    2、具有完全的 CLI 系統、支持界面/命令雙向操作的面向開發者的可執行命令  
    
*     
    3、基於商業軟件 Bladed 理論的全新軟件框架，MBD 2.0 支持雙機頭與單點系泊動力學的高精度計算  
    
*     
    4、提供了面向 Python/c++等用戶的動態連結庫和手冊支持，方便與其他軟件耦合  
    

##   HawtC 與 OpenFAST/Bladed 4.11 計算驗證對比

###   1\. 與 OpenFAST 對比的陸上 IEA 15MW 穩態無風剪切驗證

####   1）驗證結果

[http://www.openwecd.fun/data/稳态无风剪切Compare.html](http://www.openwecd.fun/data/%E7%A8%B3%E6%80%81%E6%97%A0%E9%A3%8E%E5%89%AA%E5%88%87Compare.html)

####   2）驗證程序

[http://www.openwecd.fun/data/稳态Compare.ipynb](http://www.openwecd.fun/data/%E7%A8%B3%E6%80%81Compare.ipynb)

###   2.與 OpenFAST 對比的陸上 IEA 15MW 湍流風的驗證

####   1）驗證結果

[http://www.openwecd.fun/data/湍流Compare.html](http://www.openwecd.fun/data/%E6%B9%8D%E6%B5%81Compare.html)

####   2）驗證程序

[http://www.openwecd.fun/data/湍流Compare.ipynb](http://www.openwecd.fun/data/%E6%B9%8D%E6%B5%81Compare.ipynb)

###   3.HawtC.AeroL 氣動力模組與 Bladed 4.11 計算驗證對比

![Compare_Bladed4_11.jpg](./docs/Compare_Bladed4_11.jpg)

###   4.HawtC.MBD.VTK 多體動力學可視化的 NREL 5MW Spar 海上漂浮式風力機測試

![windturbine](./docs/image/TheoryManualandBarchMarkreport/12.webp)

###   5.HawtC.BeamL 非線性樑(3D 共旋樑理論)模組的驗證

![windturbine](./docs/image/TheoryManualandBarchMarkreport/BeamL.png)

###   6.HawtC.HydroL.Wave 水動力波浪生成模組驗證

  
該模組已經通過了與 Bladed 4.11 的驗證

###   7.HawtC.HydroL.MoorL 水動力波浪生成模組驗證

  
該模組完全耦合了 OpenMoor 2 與 MoorDyn 3 模組，以計算系泊力。同時，我們自己的系泊動力學 MoorL 模組還在開發當中，以支持風場狀態下的共享系泊。![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case1-25.gif) ![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case3-5.gif)

  
圖片來源於 http://openmoor.org/

###   8.HawtC.WindL.IECWind IEC 風場生成模組驗證

###   9.HawtC.WindL.SimWind 湍流風生成模組驗證

  
該模組與 OpenFAST.TurbSim 模組功能類似，下麵是 ETM 風模型：![ETMWind](./docs/image/TheoryManualandBarchMarkreport/wind.webp)

###   10.HawtC.MoptL 整機一體化最佳化模組數據驅動腳本

  
請查閱文件，了解範例接口：

*     
    腳本類語言接口（Python/R/Julia/Matlab）：BP 神經網路模型：DemoBPNetWork.py
    
      
    自然神經網路模型：DemoBPNetWork.py
    
*     
    編譯型語言接口(C/C++/Fortran/c#):
    
      
    c++接口模板: MoptL 數據驅動案例.sln
    

##   原始碼下載

  
請訪問 www.HawtC.cn

##   交流論壇

  
交流論壇 http://www.openwecd.fun:22304/

####   參考文獻

1.  [https://github.com/OpenFAST/openfast](https://github.com/OpenFAST/openfast) [↩](#user-content-fnref-1)
    
2.    
    Chen, L., Basu, B. & Nielsen, S.R.K.（2018）。用於浮動式海上風力渦輪機分析的耦合有限差分系泊動力學模型。海洋工程, 162, 304-315 ↩
    
3.  [https://github.com/FloatingArrayDesign/MoorDyn?tab=readme-ov-file](https://github.com/FloatingArrayDesign/MoorDyn?tab=readme-ov-file) [↩](#user-content-fnref-3)