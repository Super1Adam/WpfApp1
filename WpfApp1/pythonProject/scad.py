import pandas as pd
import numpy as np

# 读取数据
import sys
if len(sys.argv) != 3:
    print("Usage: python script.py <file_path> <to_cav_path>")
    sys.exit(1)

file_path =sys.argv[1]
to_csv_path = sys.argv[2]
df = pd.read_csv(file_path, comment='#')

# 只保留需要的六个列
df_needed = df[['Wind speed (m/s)', 'Power (kW)', 'Vane position 1+2', 'Gearbox speed (RPM)', 'Generator RPM (RPM)',
                'Voltage L1 / U (V)']]

# 计算每列分为12份的平均值
num_bins = 12  # 12份
result = []

for col in df_needed.columns:
    # 将列数据分为 12 份并计算每一份的均值
    col_data = df_needed[col].dropna()  # 删除该列中的 NaN 数据
    bin_size = len(col_data) // num_bins  # 每份的数据量

    # 获取每一份的平均值
    averages = [col_data[i * bin_size:(i + 1) * bin_size].mean() for i in range(num_bins)]

    # 如果数据量不是12的整数倍，处理剩余的数据
    if len(col_data) % num_bins != 0:
        remaining_data = col_data[num_bins * bin_size:]
        remaining_avg = remaining_data.mean() if not remaining_data.empty else np.nan
        averages[-1] = remaining_avg  # 更新最后一份的平均值

    result.append(averages)

# 将计算的结果转换为 DataFrame
# 转置列表，确保每列的数据对应于 DataFrame 中的每一列
result_df = pd.DataFrame(np.array(result).T, columns=df_needed.columns)

result_df = result_df.fillna(0)
# 输出处理后的数据到一个新的CSV文件

result_df.to_csv(to_csv_path, index=False)
