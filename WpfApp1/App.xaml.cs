using System.Configuration;
using System.Data;
using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.Views;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // 初始化 GlobalVariables 中的 ViewModel
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 初始化全局 ViewModel
            GlobalVariablesViewModels.CurrentMainViewModel = new MainViewModels();

            // 不直接决定启动哪个窗口，交给其他地方处理
        }
    }

}
