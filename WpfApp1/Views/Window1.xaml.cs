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
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Window.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            string objPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "obj.obj");
            this.DataContext = new MainViewModels();
            string modelPath1 = objPath ;
            ModelImporter import = new ModelImporter();
            var initGroup1 = import.Load(modelPath1);

            // // 加载第二个模型
            // string modelPath2 = "E:\\OBJ格式\\Z更新2.obj";
            // var initGroup2 = import.Load(modelPath2);

            // 创建第一个 ModelVisual3D 并为每个 GeometryModel3D 设置红色
            ModelVisual3D modelVisual3d1 = new ModelVisual3D();
            //SetModelColor(initGroup1, Colors.Red); // 设置颜色为红色
            modelVisual3d1.Content = initGroup1;
            viewPort3D.Children.Add(modelVisual3d1);
        }
    }



}
