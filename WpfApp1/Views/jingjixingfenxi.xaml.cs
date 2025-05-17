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
            if (GlobalVariables.IsEra5DataImported)
            {
                Window1 mainWindow = new Window1();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // 如果数据未导入，弹出提示信息
                MessageBox.Show("请先导入ERA5数据！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Button_XunHuan(object sender, RoutedEventArgs e)

        {
            if (GlobalVariables.IsShouMing5DataImported)
            {
                CycleWindow window1 = new CycleWindow();
                window1.Show();
                this.Close();
            }
            else
            {
                // 如果数据未导入，弹出提示信息
                MessageBox.Show("请先进行寿命预测", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

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
            shuJuGengXingWindow.ShowDialog();

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
                int usedLifetime = TotalLife - expectedRetireYear;
                RemainingLifeTextBlock.Text = usedLifetime.ToString();
                // 使用全局变量中的加速因子计算剩余寿命和百分比
                var overallResult = LifeCalculator.CalculateOverallLife(GlobalVariables.OverallAccelerationFactor, usedLifetime);
                var bladeResult = LifeCalculator.CalculateBladeLife(GlobalVariables.BladeAccelerationFactor, usedLifetime);
                var gearboxResult = LifeCalculator.CalculateGearboxLife(GlobalVariables.GearboxAccelerationFactor, usedLifetime);
                var generatorResult = LifeCalculator.CalculateGeneratorLife(GlobalVariables.GeneratorAccelerationFactor, usedLifetime);
                var converterResult = LifeCalculator.CalculateConverterLife(GlobalVariables.ConverterAccelerationFactor, usedLifetime);
                // 计算比例
                double percentage = (double)usedLifetime / TotalLife;

                // 显示结果
                BladeResultTextBlock.Value = Math.Round(bladeResult.RemainingLifePercentage / 100, 2);
                GearboxResultTextBlock.Value = Math.Round(gearboxResult.RemainingLifePercentage / 100, 2);
                GeneratorResultTextBlock.Value = Math.Round(generatorResult.RemainingLifePercentage / 100, 2);
                ConverterResultTextBlock.Value = Math.Round(converterResult.RemainingLifePercentage / 100, 2);


                // 根据 percentage 推荐 4 种方案
                viewModel.FangAns1.Clear(); // 清空原有推荐

                if (percentage >= 0.7)
                {
                    // 剩余寿命长 → 推荐维修/延寿
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "叶片不停机检查",
                        Content = "检查叶片是否存在哨声，是否存在3个叶片声音不一致现象，存在时应停机进一步检查",
                        Time = "2周"
                    });
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "齿轮箱主轴轴承",
                        Content = "在日常检查中发现主轴轴承支座密封处渗漏油严重或集油盒内积满油脂时，应对主轴轴承进行开箱检查，检查结合油脂更换的周期，观察油脂的状况，以及主轴轴承滚动体和轨道磨损情况，轴承内外圈和保持架是否存在裂纹等状况。",
                        Time = "1个月"
                    });
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "发电机表面检查",
                        Content = "无锈蚀，清扫外壳",
                        Time = "6个月"
                    });
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "变流器断路器",
                        Content = "检查主断路器的动作次数是否超过规定次数，超过的话需要更换。",
                        Time = "1年"
                    });
                }
                else if (percentage >= 0.4)
                {
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "叶片前缘",
                        Content = "腐蚀、开裂",
                        Time = "涂料填充"
                    });
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "齿轮箱",
                        Content = "箱体和行星架的损坏、齿轮失效、齿轮轴和轴失效、滚动轴承失效",
                        Time = "用备用件替换，换下来的原件进行再制造"
                    });
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "发电机轴承",
                        Content = "轴承高温卡死、定转子扫膛",
                        Time = "故障排除，针对性解决"
                    });
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "变流器控制板",
                        Content = "通信故障",
                        Time = "检查线缆是否接触不良、元器件是否存在损坏"
                    });
                }
                else if (percentage >= 0)
                {
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "叶片再利用",//点击查看示例图片1.3.1 1.3.2 1.3.4
                        Content = "作为板材利用，可制作为挡板、托盘等就近梯次利用到农庄、物流等场景",
                        Time = "切割成合适的板材后，可制作为挡板、托盘等就近梯次利用到农庄、物流等场景"
                    });
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "齿轮箱再制造",
                        Content = "在原有制造的基础上进行一次新的制造",
                        Time = "接近甚至超过新品品质，但是成本更高"
                    });
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "发电机机组改造",
                        Content = "铜线拆解后用于金属回收",
                        Time = "材料回收利用，降低浪费"
                    });
                    viewModel.FangAns1.Add(new FangAn
                    {
                        ProjectName = "变流器再循环",
                        Content = "用化学溶剂提取贵重金属",
                        Time = "资源利用率高，但是会破坏金属品质，有大量污水的产生"
                    });
                }
                
            }
            else
            {
                MessageBox.Show("请输入有效的退役年份（1-25）！");
            }

            
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowScada windowScada = new WindowScada();
            windowScada.Show();
            this.Close();
        }
    }
}
