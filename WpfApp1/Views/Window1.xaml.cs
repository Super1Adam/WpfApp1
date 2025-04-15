using HelixToolkit.Wpf;
using Microsoft.Win32;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Window.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private Window _previousWindow;
        private Model3DGroup initGroup;
        private Model3DGroup initGroup1;
        private Model3DGroup initGroup2;
        private Model3DGroup initGroup3;
        private Model3DGroup initGroup4;
        public Window1()
        {
            InitializeComponent();

            string objPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "obj.obj");


            this.DataContext = new MainViewModels();
            //主视角
            string modelPath1 = objPath;
            ModelImporter import = new ModelImporter();
            initGroup = import.Load(modelPath1);
            ModelVisual3D modelVisual3d = new ModelVisual3D();
          
            modelVisual3d.Content = initGroup;
            viewPort3D.Children.Add(modelVisual3d);


            //叶片
            initGroup1 = import.Load(modelPath1);
            ModelVisual3D modelVisual3d1 = new ModelVisual3D();

            modelVisual3d1.Content = initGroup1;
            viewPort3D1.Children .Add(modelVisual3d1);

            //齿轮箱
            initGroup2 = import.Load(modelPath1);
            ModelVisual3D modelVisual3d2 = new ModelVisual3D();

            modelVisual3d2.Content = initGroup2;
            viewPort3D2.Children.Add(modelVisual3d2);

            //发电机
            initGroup3 = import.Load(modelPath1);
            ModelVisual3D modelVisual3d3 = new ModelVisual3D();
            modelVisual3d3.Content = initGroup3;
            viewPort3D3.Children.Add(modelVisual3d3);

            //变流器
            initGroup4 = import.Load(modelPath1);
    
            ModelVisual3D modelVisual3d4 = new ModelVisual3D();
            modelVisual3d4.Content = initGroup4;
            viewPort3D4.Children.Add(modelVisual3d4);
            
   








        }

        private void Button_ClickBack(object sender, RoutedEventArgs e)

        {
            this.Close();
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
            jingjixingfenxi.Show();
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

        private void Button_3D(object sender, RoutedEventArgs e)

        {
            // 打开文件选择对话框
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择3D模型文件",
                Filter = "3D模型文件 (*.obj;*.fbx;*.stl)|*.obj;*.fbx;*.stl|所有文件 (*.*)|*.*",
                InitialDirectory = @"C:\Users\Public\Documents" // 默认路径
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // 获取用户选择的文件路径
                string selectedFilePath = openFileDialog.FileName;

                // 获取当前应用程序目录的路径
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // 设置保存路径，文件名和原始文件相同
                string savePath = System.IO.Path.Combine(appDirectory, System.IO.Path.GetFileName(selectedFilePath));

                try
                {
                    // 复制文件到应用程序目录（如果文件已存在，会被覆盖）
                    System.IO.File.Copy(selectedFilePath, savePath, true);

                    MessageBox.Show($"模型文件已成功保存到: {savePath}", "保存成功", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"保存文件时发生错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void UpdateModelColors(double usedLifetime)
        {
            // 计算每个部件的剩余寿命百分比
            var bladeResult = LifeCalculator.CalculateBladeLife(GlobalVariables.BladeAccelerationFactor, usedLifetime);
            var gearboxResult = LifeCalculator.CalculateGearboxLife(GlobalVariables.GearboxAccelerationFactor, usedLifetime);
            var generatorResult = LifeCalculator.CalculateGeneratorLife(GlobalVariables.GeneratorAccelerationFactor, usedLifetime);
            var converterResult = LifeCalculator.CalculateConverterLife(GlobalVariables.ConverterAccelerationFactor, usedLifetime);

            // 根据百分比设置颜色
            UpdateModelColor1(66, bladeResult.RemainingLifePercentage);    // 叶片
            UpdateModelColor2(84, gearboxResult.RemainingLifePercentage); // 齿轮箱
            UpdateModelColor4(49, converterResult.RemainingLifePercentage); // 发电机
            UpdateModelColor3(37, generatorResult.RemainingLifePercentage); // 变流器
          
        }

        private void UpdateModelColor1(int targetIndex, double remainingLifePercentage)
        {
            if (targetIndex >= 0 && targetIndex < initGroup1.Children.Count)
            {
                var model = initGroup1.Children[targetIndex];
                if (model is GeometryModel3D geometryModel)
                {
                    // 计算渐变颜色
                    Color color = GetColorForPercentage(remainingLifePercentage);

                    // 修改材质颜色
                    geometryModel.Material = new DiffuseMaterial(new SolidColorBrush(color));
                }
            }
        }
        private void UpdateModelColor2(int targetIndex, double remainingLifePercentage)
        {
            if (targetIndex >= 0 && targetIndex < initGroup2.Children.Count)
            {
                var model = initGroup2.Children[targetIndex];
                if (model is GeometryModel3D geometryModel)
                {
                    // 计算渐变颜色
                    Color color = GetColorForPercentage(remainingLifePercentage);

                    // 修改材质颜色
                    geometryModel.Material = new DiffuseMaterial(new SolidColorBrush(color));
                }
            }
        }
        private void UpdateModelColor3(int targetIndex, double remainingLifePercentage)
        {
            if (targetIndex >= 0 && targetIndex < initGroup3.Children.Count)
            {
                var model = initGroup3.Children[targetIndex];
                if (model is GeometryModel3D geometryModel)
                {
                    // 计算渐变颜色
                    Color color = GetColorForPercentage(remainingLifePercentage);

                    // 修改材质颜色
                    geometryModel.Material = new DiffuseMaterial(new SolidColorBrush(color));
                }
            }
        }
        private void UpdateModelColor4(int targetIndex, double remainingLifePercentage)
        {
            if (targetIndex >= 0 && targetIndex < initGroup4.Children.Count)
            {
                var model = initGroup4.Children[targetIndex];
                if (model is GeometryModel3D geometryModel)
                {
                    // 计算渐变颜色
                    Color color = GetColorForPercentage(remainingLifePercentage);

                    // 修改材质颜色
                    geometryModel.Material = new DiffuseMaterial(new SolidColorBrush(color));
                }
            }
        }

        private Color GetColorForPercentage(double percentage)
        {
            // 将百分比限制在 0 到 100 的范围
            double normalizedPercentage = Math.Clamp(percentage, 0, 100) / 100.0;

            byte red, green, blue;

            if (normalizedPercentage <= 0.5)
            {
                // 红色到蓝色过渡 (0% - 50%)
                double transition = normalizedPercentage / 0.5; // 0 到 1
                red = (byte)(255 * (1 - transition));          // 红色从 255 减少到 0
                green = 0;                                    // 绿色保持为 0
                blue = (byte)(255 * transition);              // 蓝色从 0 增加到 255
            }
            else
            {
                // 蓝色到绿色过渡 (50% - 100%)
                double transition = (normalizedPercentage - 0.5) / 0.5; // 0 到 1
                red = 0;                                    // 红色保持为 0
                green = (byte)(255 * transition);           // 绿色从 0 增加到 255
                blue = (byte)(255 * (1 - transition));      // 蓝色从 255 减少到 0
            }

            return Color.FromRgb(red, green, blue);
        }

        //private Color GetColorForPercentage(double percentage)
        //{
        //    // 将百分比限制在 0 到 100 的范围
        //    double normalizedPercentage = Math.Clamp(percentage, 0, 100) / 100.0;

        //    // 红色到绿色过渡
        //    byte red = (byte)(255 * (1 - normalizedPercentage));  // 红色从 255 过渡到 0
        //    byte green = (byte)(255 * normalizedPercentage);      // 绿色从 0 过渡到 255

        //    return Color.FromRgb(red, green, 0);
        //    //double normalizedPercentage = Math.Clamp(percentage, 0, 100) / 100.0;

        //    //// 根据百分比调整红色的透明度（深浅）
        //    //byte red = 255; // 红色分量固定
        //    //byte alpha = (byte)(255*(1- normalizedPercentage)); // 透明度从 0 到 255

        //    //return Color.FromArgb(alpha, red, 0, 0);
        //}

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

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(UsedLifetimeTextBox.Text, out double usedLifetime))
            {
                try
                {
                    // 调用计算方法，针对不同的加速因子计算寿命
                    UpdateTextBlocks(usedLifetime);
                    GlobalVariables.IsShouMing5DataImported = true;

                }
                catch (Exception ex)
                {
                    OverallResultTextBlock.Text = $"错误: {ex.Message}";
                }
            }
            else
            {
                OverallResultTextBlock.Text = "请输入有效的数字作为已使用寿命。";
            }

            if (double.TryParse(UsedLifetimeTextBox.Text, out double parsedLifetime))
            {
                UpdateModelColors(usedLifetime);
            }
            else
            {
                OverallResultTextBlock.Text = "请输入有效的数字作为已使用寿命。";
            }


        }
        private void UpdateTextBlocks(double usedLifetime)
        {
            // 使用全局变量中的加速因子计算剩余寿命和百分比
            var overallResult = LifeCalculator.CalculateOverallLife(GlobalVariables.OverallAccelerationFactor, usedLifetime);
            var bladeResult = LifeCalculator.CalculateBladeLife(GlobalVariables.BladeAccelerationFactor, usedLifetime);
            var gearboxResult = LifeCalculator.CalculateGearboxLife(GlobalVariables.GearboxAccelerationFactor, usedLifetime);
            var generatorResult = LifeCalculator.CalculateGeneratorLife(GlobalVariables.GeneratorAccelerationFactor, usedLifetime);
            var converterResult = LifeCalculator.CalculateConverterLife(GlobalVariables.ConverterAccelerationFactor, usedLifetime);
            // 更新全局变量
            GlobalLifetimes.BladeLife = bladeResult.RemainingLifePercentage;
            GlobalLifetimes.GearboxLife = gearboxResult.RemainingLifePercentage;
            GlobalLifetimes.GeneratorLife = generatorResult.RemainingLifePercentage;
            GlobalLifetimes.ConverterLife = converterResult.RemainingLifePercentage;

            // 显示结果
            OverallResultTextBlock.Text = $"{LimitRange(overallResult.RemainingLife, 20, 25):F2} 年";
      BladeResultTextBlock.Value = Math.Round(bladeResult.RemainingLifePercentage / 100, 2);
GearboxResultTextBlock.Value = Math.Round(gearboxResult.RemainingLifePercentage / 100, 2);
GeneratorResultTextBlock.Value = Math.Round(generatorResult.RemainingLifePercentage / 100, 2);
ConverterResultTextBlock.Value = Math.Round(converterResult.RemainingLifePercentage / 100, 2);

        }

        // 限制寿命范围的函数
        private double LimitRange(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowScada windowScada = new WindowScada();
            windowScada.Show();
            this.Close();
        }
    }





    }




public static class LifeCalculator
{
    // 各个部件的参考寿命
    private const double OverallReferenceLifetime = 23.0;  // 风机整体参考寿命
    private const double BladeReferenceLifetime = 28.0;    // 叶片参考寿命
    private const double GearboxReferenceLifetime = 12.0;  // 齿轮箱参考寿命
    private const double GeneratorReferenceLifetime = 12.0; // 发电机参考寿命
    private const double ConverterReferenceLifetime = 5.0; // 变流器参考寿命

    /// <summary>
    /// 通用寿命计算方法
    /// </summary>
    private static (double RemainingLife, double RemainingLifePercentage) CalculateRemainingLife(
        double referenceLifetime, double accelerationFactor, double usedLifetime)
    {
        if (accelerationFactor <= 0) throw new ArgumentException("加速因子必须大于0", nameof(accelerationFactor));
        if (usedLifetime < 0) throw new ArgumentException("已使用寿命不能为负", nameof(usedLifetime));

        double remainingLife = referenceLifetime / accelerationFactor - usedLifetime;
        double remainingLifePercentage = 100 * (1 - (accelerationFactor * usedLifetime) / referenceLifetime);

        remainingLife = Math.Max(0, remainingLife);
        remainingLifePercentage = Math.Max(0, remainingLifePercentage);

        return (remainingLife, remainingLifePercentage);
    }

    // 各个部件的专属计算方法
    public static (double RemainingLife, double RemainingLifePercentage) CalculateOverallLife(double accelerationFactor, double usedLifetime)
        => CalculateRemainingLife(OverallReferenceLifetime, accelerationFactor, usedLifetime);

    public static (double RemainingLife, double RemainingLifePercentage) CalculateBladeLife(double accelerationFactor, double usedLifetime)
        => CalculateRemainingLife(BladeReferenceLifetime, accelerationFactor, usedLifetime);

    public static (double RemainingLife, double RemainingLifePercentage) CalculateGearboxLife(double accelerationFactor, double usedLifetime)
        => CalculateRemainingLife(GearboxReferenceLifetime, accelerationFactor, usedLifetime);

    public static (double RemainingLife, double RemainingLifePercentage) CalculateGeneratorLife(double accelerationFactor, double usedLifetime)
        => CalculateRemainingLife(GeneratorReferenceLifetime, accelerationFactor, usedLifetime);

    public static (double RemainingLife, double RemainingLifePercentage) CalculateConverterLife(double accelerationFactor, double usedLifetime)
        => CalculateRemainingLife(ConverterReferenceLifetime, accelerationFactor, usedLifetime);
}

//public static class LifeCalculator
//{
//    private const double ReferenceLifetime = 25.0; // 参考风机寿命（年）

//    public static (double RemainingLife, double RemainingLifePercentage) CalculateRemainingLife(double accelerationFactor, double usedLifetime)
//    {
//        if (accelerationFactor <= 0) throw new ArgumentException("加速因子必须大于0", nameof(accelerationFactor));
//        if (usedLifetime < 0) throw new ArgumentException("已使用寿命不能为负", nameof(usedLifetime));

//        double remainingLife = ReferenceLifetime / accelerationFactor - usedLifetime;
//        double remainingLifePercentage = 100 * (1 - (accelerationFactor * usedLifetime) / ReferenceLifetime);

//        remainingLife = Math.Max(0, remainingLife);
//        remainingLifePercentage = Math.Max(0, remainingLifePercentage);

//        return (remainingLife, remainingLifePercentage);
//    }
//}





