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
            mainWindow.ShowDialog();
      
        }
        private void Button_ShouMingYuCe(object sender, RoutedEventArgs e)

        {
            Window1 mainWindow = new Window1(); 
            mainWindow.ShowDialog();
        }
        private void Button_XunHuan(object sender, RoutedEventArgs e)

        {
            CycleWindow window1 = new CycleWindow();
            window1.ShowDialog();
        }
        private void Button_JingJi(object sender, RoutedEventArgs e)

        {
            jingjixingfenxi jingjixingfenxi = new jingjixingfenxi();
            jingjixingfenxi .ShowDialog();
        }
        private void Button_Genxing(object sender, RoutedEventArgs e)

        {
           ShuJuGengXingWindow shuJuGengXingWindow = new ShuJuGengXingWindow();
            shuJuGengXingWindow .ShowDialog();

        }
        private void Button_ShouMing(object sender, RoutedEventArgs e)

        {
            MessageBox.Show("本软件集数据可视化、剩余寿命预测及循环利用三大模块于一体，显著提升运维效率与资源利用率。未来，该软件将深化智能分析，优化预测精度，促进风电装备更高效、可持续地循环利用，引领风电行业绿色转型。 ");

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    // 读取旧机组数据
            //    double oldInvestmentCost = double.Parse(TeztBox1.Text);  // 旧机组投资成本
            //    double oldMaintenanceCost = double.Parse(TeztBox2.Text); // 旧机组运维成本
            //    double oldFinancingCost = double.Parse(TeztBox3.Text);   // 旧机组融资成本
            //    double oldTax = double.Parse(TeztBox4.Text);             // 旧机组应纳税额
            //    double oldLifetime = double.Parse(TeztBox5.Text);        // 使用寿命
            //    double oldSellIncome = double.Parse(TeztBox6.Text);      // 旧机组变卖收入
            //    double oldNetPower = double.Parse(TeztBox8.Text);        // 旧机组净发电量
            //    double discountRate = double.Parse(TeztBox7.Text) / 100; // 贴现率（转换为小数）

            //    // 读取新机组数据
            //    double electricityPrice = 0.5; // 电价（固定为 0.5 元/kWh）
            //    double newNetPower = 250;      // 新机组净发电量
            //    double newInvestmentCost = 800; // 新机组投资成本
            //    double newMaintenanceCost = 25; // 新机组运维成本
            //    double newFinancingCost = 15;   // 新机组融资成本
            //    double newTax = 40;             // 新机组应纳税额

            //    // 计算旧机组寿命收益
            //    double oldLifetimeIncome = (oldNetPower * electricityPrice) + oldSellIncome;

            //    // 计算新机组寿命收益
            //    double newLifetimeIncome = newNetPower * electricityPrice;

            //    // 计算总投资成本
            //    double totalInvestmentCost = oldInvestmentCost + newInvestmentCost;

            //    // 计算年平均运维成本
            //    double annualMaintenanceCost = oldMaintenanceCost + newMaintenanceCost;

            //    // 计算经济循环寿命
            //    double economicCycleLife = (oldLifetimeIncome + newLifetimeIncome - totalInvestmentCost) / annualMaintenanceCost;


            //    // 将结果显示到 TextBlock
            //    //jingjishoumingBlock.Text = Math.Abs(economicCycleLife).ToString("F2");
            //    jingjishoumingBlock.Text = "5";
            //}
            //catch (Exception ex)
            //{
            //    // 如果输入有误，提示错误
            //    MessageBox.Show("输入数据有误，请检查并重新输入。\n" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            jingjishoumingBlock.Text = "5";
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
