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
    /// WelcomePage.xaml 的交互逻辑
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
            mediaElement.Source = new Uri(@"E:\gird\WpfApp1\WpfApp1\Contorls\fengji.mp4", UriKind.Absolute);

            mediaElement.MediaEnded += MediaElement_MediaEnded;
            mediaElement.Play();

        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            // 视频播放完毕后重新播放
            mediaElement.Position = TimeSpan.Zero;  // 将视频进度设置为开头
            mediaElement.Play();                    // 重新播放视频
        }

    }
}
