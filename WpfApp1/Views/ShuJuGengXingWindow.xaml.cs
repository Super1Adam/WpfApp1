using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.Views
{
    /// <summary>
    /// ShuJuGengXingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShuJuGengXingWindow : Window
    {
        public ShuJuGengXingWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 获取每个TextBox的输入内容
            string bladeDiameter = BladeDiameterTextBox.Text;  // 叶片直径
            string hubHeight = HubHeightTextBox.Text;           // 轮毂高度
            string cutOutSpeed = CutOutSpeedTextBox.Text;       // 切出风速
            string sweptArea = SweptAreaTextBox.Text;           // 扫风面积
            string ratedPower = RatedPowerTextBox.Text;         // 额定功率
            string cutInArea = CutInAreaTextBox.Text;           // 切入面积
            string gearboxRatio = GearboxRatioTextBox.Text;     // 齿轮箱传动比

            // 设置文件路径
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wind_turbine_data.txt");

            try
            {
                // 创建StreamWriter对象，打开文件进行写入
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    // 写入文本内容到文件
                    writer.WriteLine($"叶片直径: {bladeDiameter}");
                    writer.WriteLine($"轮毂高度: {hubHeight}");
                    writer.WriteLine($"切出风速: {cutOutSpeed}");
                    writer.WriteLine($"扫风面积: {sweptArea}");
                    writer.WriteLine($"额定功率: {ratedPower}");
                    writer.WriteLine($"切入面积: {cutInArea}");
                    writer.WriteLine($"齿轮箱传动比: {gearboxRatio}");
                    writer.WriteLine("---------------------------------------------------"); // 分隔线
                }

                MessageBox.Show("风机数据已保存！", "保存成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // 处理文件写入时的异常
                MessageBox.Show($"保存失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
