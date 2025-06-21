### [简体中文](./README.md) | [繁體中文](./README_FCN.md) | [English](./README_EN.md) | [日本語](./README_JP.md)

</br>
</br>

![HawtC](./docs/image/TheoryManualandBarchMarkreport/图标.png)

## 完全なコードを取得し、HawtC2の開発に参加したい場合は、私たちの組織に参加してください

## HawtCの利点

### 理論的革新

* 四元数に基づく運動学的変換方法は、FAST[^1] /Bladedの小角度仮定を打破し、高精度な運動記述と計算を実現しました。
* 自主的な知的財産権を持つ断面特性の計算方法により、IVABSおよびBECASの長期独占を打破しました。
* 完全に結合された効率的な多目的最適化アルゴリズムは、空力-構造-制御-水力の全プロセス最適化設計をサポートし、従来の人工最適化設計の非効率性を打破します。
* 共回転法に基づき各異性幾何非線形共回転梁法を構築し、従来のラグランジュ法と幾何精密梁法の非効率性を打破し、少分割、大ステップ、高精度なブレード非線形計算を実現しました。
* 5、初めてKane 法に基づきブレードおよびタワーTMDIの運動学と動力学の公式を導出し、それを多体動力学に組み込み、空力-構造-制御-水力-TMDI 制御の全リンクカップル計算を実現しました。インターフェースモデルAPILと多目的最適化モジュールMoptLを用いて複雑な風-波-浪のカップリングにおけるブレードTMDIの多目的最適化設計を実現しました。
* 6、リアルタイムデータ駆動と多目的結合の最適化アルゴリズムを提案し、真のデータ参照ベクトルを構築することで、従来のデータ駆動法の予測結果の悪さやモデルの一般化能力の弱さの問題を解決し、最適化効率と予測精度を大幅に向上させました。
* 7、ブレードの積層（構造設計）-ブレード翼型の応力（安全性設計）-ブレード空力（高効率な空力形状設計）の超長柔軟ブレードのカップル設計の難題を克服し、大型ユニットの翼型-ブレード-機全体のカップル設計方法を実現し、モデリングおよびシミュレーションツールを提供しました。

### 技術革新

* 1、完全に100％C#ベースのネイティブコードを使用し、オブジェクト指向プログラミング形式を採用し、シミュレーションソフトウェアの海外独占を打ち破りました。
* 完全なCLIシステムを備え、インターフェース/コマンドの双方向操作をサポートする開発者向けの実行可能コマンド
* 商業ソフトウェアBladedの理論に基づく新しいソフトウェアフレームワークであり、MBD 2.0は双機頭と単点係留力学の高精度計算をサポートします
* Python/c++などのユーザー向けに動的リンクライブラリとマニュアルサポートを提供し、他のソフトウェアとの結合を容易にします

## HawtCとOpenFAST/Bladed 4.11の計算検証比較

### 1\. OpenFASTとの比較における陸上 IEA 15MWの定常無風剪断検証

#### 1）検証結果

[http://www.openwecd.fun/data/稳态无风剪切Compare.html](http://www.openwecd.fun/data/%E7%A8%B3%E6%80%81%E6%97%A0%E9%A3%8E%E5%89%AA%E5%88%87Compare.html)

#### 2）検証手順

[http://www.openwecd.fun/data/稳态Compare.ipynb](http://www.openwecd.fun/data/%E7%A8%B3%E6%80%81Compare.ipynb)

### 2\. OpenFASTとの比較における陸上 IEA 15MWの乱流風の検証

#### 1）検証結果

[http://www.openwecd.fun/data/湍流Compare.html](http://www.openwecd.fun/data/%E6%B9%8D%E6%B5%81Compare.html)

#### 2）検証手順

[http://www.openwecd.fun/data/湍流Compare.ipynb](http://www.openwecd.fun/data/%E6%B9%8D%E6%B5%81Compare.ipynb)

### 3.HawtC.AeroL 空力モジュールとBladed 4.11 計算検証比較

![Compare_Bladed4_11.jpg](./docs/Compare_Bladed4_11.jpg)

### 4.HawtC.MBD.VTK 多体動力学可視化のNREL 5MW Spar 海上浮体式風車試験

![windturbine](./docs/image/TheoryManualandBarchMarkreport/12.webp)
`<br/>`
`<br/>`

#### HAWTC.FARM:

![111](./docs/image/TheoryManualandBarchMarkreport/133.webp)

### 5.HawtC.BeamL 非線形梁（3D 共回転梁理論）モジュールの検証

![windturbine](./docs/image/TheoryManualandBarchMarkreport/BeamL.png)

### 6.HawtC.HydroL.Wave 水動力波生成モジュールの検証

このモジュールはBladed 4.11との検証に合格しました

| Blade                                                                       | HawtC                                                                       |
| --------------------------------------------------------------------------- | --------------------------------------------------------------------------- |
| ![Blade](./docs/image/TheoryManualandBarchMarkreport/截图_20250412034847.png) | ![HawtC](./docs/image/TheoryManualandBarchMarkreport/截图_20250412034808.png) |

### 7.HawtC.HydroL.MoorL 水動力波生成モジュールの検証

このモジュールは、係止力を計算するためにOpenMoor[^2]とMoorDyn[^3]モジュールを完全に結合しています。同時に、風場状態における共有係留をサポートするために、我々自身の係留動力学 MoorLモジュールも開発中です。![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case1-25.gif) ![OpenMoor](./docs/image/TheoryManualandBarchMarkreport/Case3-5.gif)

画像出典：http://openmoor.org/

### 8.HawtC.PCSL 断面特性の計算と検証
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

### 9.HawtC.WindL.SimWind 乱流風生成モジュールの検証

このモジュールはOpenFAST.TurbSimモジュールと似た機能を持っています。以下はETM 風モデルです:![ETMWind](./docs/image/TheoryManualandBarchMarkreport/wind.webp)

### 10.HawtC.MoptL 統合型最適化モジュールデータ駆動スクリプト

ファイルを参照して、インターフェースの例を確認してください:

* スクリプト言語インターフェース(Python/R/Julia/Matlab): BPニューラルネットワークモデル: DemoBPNetWork.py

  自然ニューロネットワークモデル: DemoBPNetWork.py
* コンパイル言語インターフェース (C/C++/Fortran/c#):

  c++インターフェーステンプレート: MoptLデータ駆動ケース.sln

## ソースコードのダウンロード

[www.HawtC.cn](http://www.openwecd.fun/) をご覧ください。

## フォーラム

フォーラム http://www.openwecd.fun:22304/

#### 文献参照

[^1]: https://github.com/OpenFAST/openfast
    
[^2]: Chen, L., Basu, B. & Nielsen, S.R.K. (2018). A coupled finite difference mooring dynamics model for floating offshore wind turbine analysis. Ocean Engineering,162, 304-315
    
[^3]: https://github.com/FloatingArrayDesign/MoorDyn?tab=readme-ov-file
