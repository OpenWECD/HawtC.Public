import math
import numpy as np

def generate_ring_mesh(R, t, alpha=1.0, n_min=8, m_min=2):
    """
    生成圆环的4节点四边形网格
    :param R: 外半径
    :param t: 厚度
    :param alpha: 目标长宽比 (默认1.0)
    :param n_min: 最小环向单元数 (默认8)
    :param m_min: 最小径向层数 (默认2)
    :return: 节点坐标, 单元连接
    """
    # 1. 计算环向单元数
    n = max(n_min, math.ceil(4 * math.pi * R / t))
    
    # 2. 计算径向层数 (带曲率补偿)
    k_curve = 1 + t/(2*R)  # 曲率补偿因子
    m = max(m_min, math.ceil((t * n * k_curve) / (2 * math.pi * R * alpha)))
    
    # 3. 生成节点 (从外向内)
    nodes = []
    for i in range(m + 1):
        rho = R - i * (t / m)  # 当前半径
        for j in range(n):
            theta = j * (2 * math.pi / n)
            x = rho * math.cos(theta)
            y = rho * math.sin(theta)
            nodes.append([x, y])
    
    # 4. 生成单元
    elements = []
    for i in range(m):
        for j in range(n):
            n1 = i * n + j
            n2 = i * n + (j + 1) % n
            n3 = (i + 1) * n + (j + 1) % n
            n4 = (i + 1) * n + j
            elements.append([n1, n2, n3, n4])
            
    return np.array(nodes), np.array(elements)

# 示例：生成 t/(2R)=0.2 的网格
R = 0.3
t_ratio = 0.33333333333333333 # t/(2R) = 1/3
t = 2 * R * t_ratio 
nodes, elements = generate_ring_mesh(R, t,1.0,100,20)

# 将节点和单元信息输出到文件当中
with open('ring_mesh.txt', 'w') as f:
    f.write(f"{len(nodes)}                                   FEMNodeNum        -   自定义网格当中的有限元节点数量\n")
    f.write(f"{len(elements)}                               FEMElementNum     -   自定义网格当中的有限元单元数量\n")
    f.write(f"!BeginNode  (节点编号，从零开始 ； 节点x坐标  ；节点y坐标)\n")
    i= 0
    for node in nodes:
        f.write(f"{i}\t{node[0]:.6f}\t{node[1]:.6f}\n")
        i += 1
    i= 0
    f.write("\n")
    f.write(f"!BeginElement(单元，从零开始 ； 节点编号1  ； 节点编号2 ； 节点编号3 ； 节点编号4；材料编号 ；铺层角度 )\n")
    for element in elements:
        f.write(f"{i}\t{element[0]}\t{element[1]}\t{element[2]}\t{element[3]}\t{1}\t{0}\n")
        i += 1

