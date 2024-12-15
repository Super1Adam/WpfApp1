using HelixToolkit.Wpf;
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
        private Model3DGroup initGroup1;
        public Window1(Window previousWindow)
        {
            InitializeComponent();
            _previousWindow = previousWindow;

            string objPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "obj.obj");
            this.DataContext = new MainViewModels();
            string modelPath1 = objPath;
            ModelImporter import = new ModelImporter();
            initGroup1 = import.Load(modelPath1);


            // // 加载第二个模型
            // string modelPath2 = "E:\\OBJ格式\\Z更新2.obj";
            // var initGroup2 = import.Load(modelPath2);

            // 创建第一个 ModelVisual3D 并为每个 GeometryModel3D 设置红色
            ModelVisual3D modelVisual3d1 = new ModelVisual3D();
            //SetModelColor(initGroup1, Colors.Red); // 设置颜色为红色
            modelVisual3d1.Content = initGroup1;
            viewPort3D.Children.Add(modelVisual3d1);
            double A = GlobalVariables.OverallAccelerationFactor;
            string AS = A.ToString();
            //MessageBox.Show(AS);
        }

        private void Button_ClickBack(object sender, RoutedEventArgs e)

        {
            this.Close();
        }

        private void UpdateModelColors(double usedLifetime)
        {
            // 计算每个部件的剩余寿命百分比
            var bladeResult = LifeCalculator.CalculateRemainingLife(GlobalVariables.BladeAccelerationFactor, usedLifetime);
            var gearboxResult = LifeCalculator.CalculateRemainingLife(GlobalVariables.GearboxAccelerationFactor, usedLifetime);
            var generatorResult = LifeCalculator.CalculateRemainingLife(GlobalVariables.GeneratorAccelerationFactor, usedLifetime);
            var converterResult = LifeCalculator.CalculateRemainingLife(GlobalVariables.ConverterAccelerationFactor, usedLifetime);

            // 根据百分比设置颜色
            UpdateModelColor(66, bladeResult.RemainingLifePercentage);    // 叶片
            UpdateModelColor(84, gearboxResult.RemainingLifePercentage); // 齿轮箱
            UpdateModelColor(49, generatorResult.RemainingLifePercentage); // 发电机
            UpdateModelColor(37, converterResult.RemainingLifePercentage); // 变流器
        }

        private void UpdateModelColor(int targetIndex, double remainingLifePercentage)
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

        private Color GetColorForPercentage(double percentage)
        {
            // 将百分比限制在 0 到 100 的范围
            double normalizedPercentage = Math.Clamp(percentage, 0, 100) / 100.0;

            // 红色到绿色过渡
            byte red = (byte)(255 * (1 - normalizedPercentage));  // 红色从 255 过渡到 0
            byte green = (byte)(255 * normalizedPercentage);      // 绿色从 0 过渡到 255

            return Color.FromRgb(red, green, 0);
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
            var overallResult = LifeCalculator.CalculateRemainingLife(GlobalVariables.OverallAccelerationFactor, usedLifetime);
            var bladeResult = LifeCalculator.CalculateRemainingLife(GlobalVariables.BladeAccelerationFactor, usedLifetime);
            var gearboxResult = LifeCalculator.CalculateRemainingLife(GlobalVariables.GearboxAccelerationFactor, usedLifetime);
            var generatorResult = LifeCalculator.CalculateRemainingLife(GlobalVariables.GeneratorAccelerationFactor, usedLifetime);
            var converterResult = LifeCalculator.CalculateRemainingLife(GlobalVariables.ConverterAccelerationFactor, usedLifetime);
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
            
            var window2 =new CycleWindow();
            window2.ShowDialog();
        }
    }





    }



        public static class LifeCalculator
        {
            private const double ReferenceLifetime = 25.0; // 参考风机寿命（年）

            public static (double RemainingLife, double RemainingLifePercentage) CalculateRemainingLife(double accelerationFactor, double usedLifetime)
            {
                if (accelerationFactor <= 0) throw new ArgumentException("加速因子必须大于0", nameof(accelerationFactor));
                if (usedLifetime < 0) throw new ArgumentException("已使用寿命不能为负", nameof(usedLifetime));

                double remainingLife = ReferenceLifetime / accelerationFactor - usedLifetime;
                double remainingLifePercentage = 100 * (1 - (accelerationFactor * usedLifetime) / ReferenceLifetime);

                remainingLife = Math.Max(0, remainingLife);
                remainingLifePercentage = Math.Max(0, remainingLifePercentage);

                return (remainingLife, remainingLifePercentage);
            }
        }
     




