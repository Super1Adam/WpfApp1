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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.Views
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            mediaElement.MediaEnded += MediaElement_MediaEnded;
            mediaElement.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string use = tetuse.Text;
            string psw = usepsw.Text;
            if (use == "123" && psw == "123")
            {
                MessageBox.Show("登录成功");
                var mainwindow = new MainWindow();
                mainwindow.Show();

                // 关闭当前窗口
                Window.GetWindow(this)?.Close();

            }
        
        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            // 视频播放完毕后重新播放
            mediaElement.Position = TimeSpan.Zero;  // 将视频进度设置为开头
            mediaElement.Play();                    // 重新播放视频
        }
    }
}
