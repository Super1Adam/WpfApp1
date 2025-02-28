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

namespace WpfApp1.Views
{
    /// <summary>
    /// testimage.xaml 的交互逻辑
    /// </summary>
    public partial class testimage : Window
    {
        public testimage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var imagePaths = new Uri[]
 {
      
        new Uri("pack://application:,,,/WpfApp1;component/Image/2.1.png"),
        new Uri("pack://application:,,,/WpfApp1;component/Image/3.1.png")
 };
           
            var url = new Uri("pack://application:,,,/WpfApp1;component/Image/1.1.png");
            ShowImagePopup(imagePaths); // 传递 url 作为图片路径

        }
        private void ShowImagePopup(Uri[] imageUris)
        {
            // 创建一个新的弹出窗口来显示多张图片
            var popupWindow = new Window
            {
                Title = "图片查看",
                Width = 500,
                Height = 500
            };

            // 创建一个 ListBox 来容纳多个图片
            var listBox = new ListBox
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            // 创建并添加每一张图片到 ListBox 中
            foreach (var imageUri in imageUris)
            {
                var imageControl = new Image
                {
                    Source = new BitmapImage(imageUri),
                    Stretch = Stretch.Uniform,
                    Margin = new Thickness(5)
                };

                // 将图片控件添加到 ListBox
                listBox.Items.Add(imageControl);
            }

            // 设置窗口的内容
            popupWindow.Content = listBox;
            popupWindow.ShowDialog(); // 显示弹出窗口
        }
    }
}
