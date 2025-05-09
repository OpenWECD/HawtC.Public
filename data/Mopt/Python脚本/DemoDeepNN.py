#Demo.py
#**********************************************************************************************************************************
#LICENSING
# Copyright(C) 2021, 2025  TG Team,Key Laboratory of Jiangsu province High-Tech design of wind turbine,WTG,WL,赵子祯
#
#    This file is part of OpenWECD.WTAI
#这个脚本用来实现MoptL模块下的数据驱动的多目标优化功能。采用了深层神经网络
#
#**********************************************************************************************************************************
import numpy as np
import torch
import torch.nn as nn
import torch.optim as optim
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import train_test_split
import matplotlib.pyplot as plt


# 设定随机种子以确保结果可复现
torch.manual_seed(42)
np.random.seed(42)

class DeepNN(nn.Module):
    def __init__(self, input_size, output_size):
        super(DeepNN, self).__init__()
        self.model = nn.Sequential(
            nn.Linear(input_size, 256),
            nn.BatchNorm1d(256),
            nn.ReLU(),
            nn.Dropout(0.3),
            nn.Linear(256, 128),
            nn.BatchNorm1d(128),
            nn.ReLU(),
            nn.Dropout(0.3),
            nn.Linear(128, 64),
            nn.BatchNorm1d(64),
            nn.ReLU(),
            nn.Linear(64, output_size)
        )
        
    def forward(self, x):
        return self.model(x)

def preprocess_data(data, n_features):
    """
    预处理数据：分割特征和目标，归一化特征
    
    参数:
    data: numpy数组，每行一个样本，前n_features列为特征，剩余列为目标
    n_features: 特征的数量
    
    返回:
    X_train, X_test, y_train, y_test: 训练和测试集的特征和目标
    scaler_X, scaler_y: 特征和目标的归一化器
    """
    X = data[:, :n_features]
    y = data[:, n_features:]
    
    # 归一化特征
    scaler_X = StandardScaler()
    X_scaled = scaler_X.fit_transform(X)
    
    # 归一化目标
    scaler_y = StandardScaler()
    y_scaled = scaler_y.fit_transform(y)
    
    # 分割训练集和测试集
    X_train, X_test, y_train, y_test = train_test_split(X_scaled, y_scaled, test_size=0.2, random_state=42)
    
    return X_train, X_test, y_train, y_test, scaler_X, scaler_y
    # BP神经网络训练函数
def train_NN_network(X_train, y_train, input_size, output_size, learning_rate=0.01, epochs=1000):
    """
    训练BP神经网络
    
    参数:
    X_train: 训练特征
    y_train: 训练目标
    input_size: 输入特征维度
    hidden_size: 隐藏层神经元数量
    output_size: 输出维度
    learning_rate: 学习率
    epochs: 训练轮数
    
    返回:
    model: 训练好的模型
    losses: 训练过程中的损失值
    """
    # 转换为PyTorch张量
    X_train_tensor = torch.FloatTensor(X_train)
    y_train_tensor = torch.FloatTensor(y_train)
    
    # 初始化模型
    model = DeepNN(input_size, output_size)
    
    # 定义损失函数和优化器
    criterion = nn.MSELoss()
    optimizer = optim.Adam(model.parameters(), lr=learning_rate)
    
    # 训练模型
    losses = []
    for epoch in range(epochs):
        # 前向传播
        outputs = model(X_train_tensor)
        loss = criterion(outputs, y_train_tensor)
        
        # 反向传播和优化
        optimizer.zero_grad()
        loss.backward()
        optimizer.step()
        
        # 记录损失
        losses.append(loss.item())
        
        # 每100轮打印一次损失
        if (epoch+1) % 100 == 0:
            print(f'Epoch [{epoch+1}/{epochs}], Loss: {loss.item():.4f}')
    
    return model, losses
# 预测函数
def predict(model, X_test, scaler_y):
    """
    使用训练好的模型进行预测
    
    参数:
    model: 训练好的BP神经网络模型
    X_test: 测试特征
    scaler_y: 目标的归一化器
    
    返回:
    predictions: 逆归一化后的预测结果
    """
    # 转换为PyTorch张量
    X_test_tensor = torch.FloatTensor(X_test)
    
    # 预测
    model.eval()  # 设置为评估模式
    with torch.no_grad():
        predictions_scaled = model(X_test_tensor).numpy()
    
    # 逆归一化
    predictions = scaler_y.inverse_transform(predictions_scaled)
    
    return predictions

# 评估函数
def evaluate(y_true, y_pred):
    """
    评估模型性能
    
    参数:
    y_true: 真实目标值
    y_pred: 预测目标值
    
    返回:
    mse: 均方误差
    """
    mse = np.mean((y_true - y_pred) ** 2)
    return mse

def read_data_file(filename):
    """
    读取数据文件，返回数据矩阵和标签列表
    """
    with open(filename, 'r') as f:
        # 读取前两行获取参数
        var_count = int(f.readline().split('!')[0].strip())  # 提取"4"并转为整数
        target_count = int(f.readline().split('!')[0].strip())  # 提取"7"并转为整数
        
        # 读取剩余行构建矩阵
        matrix = []
        for line in f:
            # 去除注释部分并分割数值
            # data_part = line.split()[0].strip()
            # if data_part:  # 忽略空行
            row = list(map(float, line.strip().split('\t')))
            matrix.append(row)
        return var_count, target_count, np.array(matrix)

def write_data_file(filename, data_matrix):
    with open(filename, 'w', encoding='utf-8') as f:
        # 写入数据矩阵
        for row in data_matrix:
            line_str = '\t'.join(map(str, row))  # 转换为空格分隔的字符串
            f.write(line_str + '\n')
# 主函数：示例如何使用上述函数
def main(data, n_features, hidden_size=64, learning_rate=0.01, epochs=1000):
    """
    主函数，演示完整的BP神经网络预测流程
    
    参数:
    data: numpy数组，每行一个样本，前n_features列为特征，剩余列为目标
    n_features: 特征的数量
    hidden_size: 隐藏层神经元数量
    learning_rate: 学习率
    epochs: 训练轮数
    """
    # 预处理数据
    X_train, X_test, y_train, y_test, scaler_X, scaler_y = preprocess_data(data, n_features)
    
    # 获取输入和输出维度
    input_size = X_train.shape[1]
    output_size = y_train.shape[1]
    
    # 训练模型
    model, losses = train_NN_network(X_train, y_train, input_size, hidden_size, output_size, learning_rate, epochs)
    
    # 绘制损失曲线
    
    # plt.plot(losses)
    # plt.title('Training Loss Curve')
    # plt.xlabel('Epoch')
    # plt.ylabel('Loss')
    # plt.grid(True)
    # plt.show()
    
    # 在测试集上预测
    y_pred_scaled = model(torch.FloatTensor(X_test)).detach().numpy()
    y_pred = scaler_y.inverse_transform(y_pred_scaled)
    y_true = scaler_y.inverse_transform(y_test)
    
    # 评估模型性能
    mse = evaluate(y_true, y_pred)
    print(f'测试集均方误差 (MSE): {mse:.4f}')
    
    # 可视化预测结果 (假设输出是一维的)
    if output_size == 1:
        plt.figure(figsize=(10, 6))
        plt.scatter(range(len(y_true)), y_true, color='blue', label='真实值')
        plt.scatter(range(len(y_pred)), y_pred, color='red', label='预测值')
        plt.title('预测结果与真实值对比')
        plt.legend()
        plt.grid(True)
        plt.show()
    
    return model, scaler_X, scaler_y
   
var_count, target_count, matrix=read_data_file('./Train.data.txt')

trained_model, scaler_X, scaler_y = main( matrix, var_count, hidden_size=64, epochs=1000)

var_count1, target_count1, matrix1=read_data_file('./Predict.data.txt')

new_data_scaled = scaler_X.transform(matrix1)
# 预测
new_predictions_scaled = trained_model(torch.FloatTensor(new_data_scaled)).detach().numpy()
# 逆归一化得到实际预测值
new_predictions = scaler_y.inverse_transform(new_predictions_scaled)

#预测完毕！将结果输出到Predict.Result.txt文件当中
write_data_file('./Predict.Result.txt',new_predictions )   
        
# if __name__ == "__main__":
   






