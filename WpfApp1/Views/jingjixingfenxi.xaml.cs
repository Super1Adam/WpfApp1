using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// jingjixingfenxi.xaml 的交互逻辑
    /// </summary>
    public partial class jingjixingfenxi : Window
    {
        private const int TotalLife = 25; // 总体寿命
        private readonly Random _random = new Random();
        public jingjixingfenxi()
        {

            InitializeComponent();
            CycleViewsModels viewModel = new CycleViewsModels();
            this.DataContext = viewModel;

        }
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void Button_ClickBack(object sender, RoutedEventArgs e)
        {

            var window2 = new CycleWindow();
            window2.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void Button_ShuJuDaoRu(object sender, RoutedEventArgs e)

        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
      
        }
        private void Button_ShouMingYuCe(object sender, RoutedEventArgs e)

        {
            Window1 mainWindow = new Window1(); 
            mainWindow.Show();
            this.Close();
        }
        private void Button_XunHuan(object sender, RoutedEventArgs e)

        {
            CycleWindow window1 = new CycleWindow();
            window1.Show();
            this.Close();
        }
        private void Button_JingJi(object sender, RoutedEventArgs e)

        {
            jingjixingfenxi jingjixingfenxi = new jingjixingfenxi();
            jingjixingfenxi .Show();
            this.Close();
        }
        private void Button_Genxing(object sender, RoutedEventArgs e)

        {
           ShuJuGengXingWindow shuJuGengXingWindow = new ShuJuGengXingWindow();
            shuJuGengXingWindow.Show();
            this .Close();

        }
        private void Button_ShouMing(object sender, RoutedEventArgs e)

        {
            MessageBox.Show("本软件集数据可视化、剩余寿命预测及循环利用三大模块于一体，显著提升运维效率与资源利用率。未来，该软件将深化智能分析，优化预测精度，促进风电装备更高效、可持续地循环利用，引领风电行业绿色转型。 ");

        }

        private void CalculateBestSwitchYear_Click(object sender, RoutedEventArgs e)
        {
            // 从 TextBox 获取输入
            double oldInvestmentCost = double.Parse(OldInvestmentCostTextBox.Text);
            double oldMaintenanceCost = double.Parse(OldMaintenanceCostTextBox.Text);
            double oldFinancingCost = double.Parse(OldFinancingCostTextBox.Text);
            double oldGeneration = double.Parse(OldGenerationTextBox.Text);

            double newInvestmentCost = double.Parse(NewInvestmentCostTextBox.Text);
            double newMaintenanceCost = double.Parse(NewMaintenanceCostTextBox.Text);
            double newFinancingCost = double.Parse(NewFinancingCostTextBox.Text);
            double newGeneration = double.Parse(NewGenerationTextBox.Text);

            int years = 25; // 评估周期
            double P = double.Parse(PTextBox.Text)/10;
            double taxRate = double.Parse(TaxRateTextBox.Text)/100;
            double oldResaleValue = double.Parse(oldResaleValueTextBox.Text);
            double newResaleValue = double.Parse(newResaleValueTextBox.Text);
            double r = double.Parse(RTextBox.Text)/100;

            // 计算最佳换新年份
            (int bestYear, double bestR2) = FindBestReplacementTime(years, oldGeneration, newGeneration, P, taxRate, oldInvestmentCost, newInvestmentCost, oldMaintenanceCost, newMaintenanceCost, oldResaleValue, newResaleValue, r, oldFinancingCost, newFinancingCost);

            // 显示结果
            ResultTextBlock.Text = $"最佳换新年份: {bestYear}年";

            // 显示结果
        }
        // 计算函数
        public (int best_K, double best_R2) FindBestReplacementTime(int N, double G_old, double G_new, double P, double tax_rate, double c_old, double c_new, double f_old, double f_new, double resale_old, double resale_new, double r,double oldFinancingCost,double newFinancingCost)
        {
            int best_K = -1;
            double best_R2 = double.MaxValue;

            // 遍历 K = 1 到 N-1，寻找最佳换新时间 K
            for (int K = 1; K < N; K++)
            {
                // 计算旧机组在 1 到 K 年的收益（R2_old）
                double R2_old = 0;
                for (int n = 1; n <= K; n++)
                {
                    R2_old += (1 - tax_rate) * G_old * P * Math.Pow(1 + r, -n) 
                        - (f_old * (2 - Math.Exp(1 - n)) + oldFinancingCost) * Math.Pow(1 + r, -n);
                }
                R2_old += resale_old * Math.Pow(1 + r, -K)-c_old; // 旧机组变卖收益

                // 计算新机组在 K+1 到 N 年的收益（R2_new）
                double R2_new = 0;
                for (int n = K + 1; n <= N; n++)
                {
                    R2_new += (1 - tax_rate) * G_new * P * Math.Pow(1 + r, -n) 
                        - (f_new * (2 - Math.Exp(1 - n)) + newFinancingCost) * Math.Pow(1 + r, -n);
                }
                R2_new += resale_new * Math.Pow(1 + r, -N)-c_new * Math.Pow(1 + r, -K); // 新机组变卖收益

                // 计算总的净收益 R2
                double R2 = R2_old + R2_new;

                // 如果当前 R2 更大，更新最佳换新时间 K 和 R2
                if (R2 < best_R2)
                {
                    best_R2 = R2;
                    best_K = K;
                }
            }
            return (best_K, best_R2);
        }



        // 最小化功能
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // 最大化/还原功能
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            // 如果当前窗口是最大化状态，则还原窗口
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            // 否则将窗口最大化
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        // 关闭功能
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // 关闭窗口
        }
        private void OnSubmitClick(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as CycleViewsModels;
            //viewModel.FangAns1.Clear ();
            // 获取用户输入的期望退役年份
            if (int.TryParse(ExpectedRetireYearTextBox.Text, out int expectedRetireYear) && expectedRetireYear > 0 && expectedRetireYear <= TotalLife)
            {
                // 计算剩余寿命
                int remainingLife = TotalLife - expectedRetireYear;
                RemainingLifeTextBlock.Text = remainingLife.ToString();

                // 计算比例
                double percentage = (double)remainingLife / TotalLife;

                // 根据比例随机生成四个部件寿命
                BladeResultTextBlock1.Text = GetRandomValue(25, 30, percentage).ToString();
                GearboxResultTextBlock1.Text = GetRandomValue(10, 15, percentage).ToString();
                GeneratorResultTextBlock1.Text = GetRandomValue(10, 15, percentage).ToString();
                ConverterResultTextBlock1.Text = GetRandomValue(2, 7, percentage).ToString();
            }
            else
            {
                MessageBox.Show("请输入有效的退役年份（1-25）！");
            }

            viewModel.FangAns1.Add(new FangAn
            {
                ProjectName = "再利用",
                Time = "作为建材利用",
                Content = "根据不同的场景将叶片切割成10cm~20cm或其他尺寸长条状小块，作为新型复合材料取代木质复合材料，可用于地板、塑料路面障碍等。本方法并未将叶片复合材料分离，而是将叶片切割后直接制作成建筑材料，因此成本较低。"
            });
            viewModel.FangAns1.Add(new FangAn
            {
                ProjectName = "再利用",
                Content = "景观利用",
                Time = "废弃的风机叶片可以被改造成艺术品，用于城市公园、展览等场合。"
            });
        }
        private int GetRandomValue(int min, int max, double percentage)
        {
            // 根据百分比缩放范围
            int scaledMin = (int)(min * percentage);
            int scaledMax = (int)(max * percentage);

            // 保证范围有效
            if (scaledMin < 1) scaledMin = 1;
            if (scaledMax < scaledMin) scaledMax = scaledMin;

            // 在缩放后的范围内取值
            return _random.Next(scaledMin, scaledMax + 1);
        }
    }
}
