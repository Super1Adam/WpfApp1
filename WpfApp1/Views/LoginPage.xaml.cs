using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.IO;

namespace WpfApp1.Views
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        private string userFile = "users.json";

        public LoginPage()
        {
            InitializeComponent();
            mediaElement.MediaEnded += MediaElement_MediaEnded;
            mediaElement.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string use = tetuse.Text;
            //string psw = usepsw.Password;
            //if (use == "123" && psw == "123")
            //{
            //    MessageBox.Show("登录成功");
            //    var mainwindow = new MainWindow();
            //    mainwindow.Show();

            //    // 关闭当前窗口
            //    Window.GetWindow(this)?.Close();

            //}
        
        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            // 视频播放完毕后重新播放
            mediaElement.Position = TimeSpan.Zero;  // 将视频进度设置为开头
            mediaElement.Play();                    // 重新播放视频
        }

        // 🚀 1. 处理 "注册" 按钮
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("用户名或密码不能为空！");
                return;
            }

            // 读取已存用户
            List<User> users = LoadUsers();

            // 检查用户名是否已存在
            if (users.Any(u => u.Username == username))
            {
             
                MessageBox.Show("用户名已存在，请更换！");

                return;
            }

            // 🚀 **不加密密码，直接存储**
            users.Add(new User { Username = username, Password = password });
            SaveUsers(users);
            
            MessageBox.Show("注册成功！请登录。");

        }

        // 🚀 2. 处理 "登录" 按钮
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("用户名或密码不能为空！");
   
                return;
            }

            // 读取用户数据
            List<User> users = LoadUsers();
            var user = users.FirstOrDefault(u => u.Username == username);

            // **直接比较明文密码**
            if (user != null && user.Password == password)
            {
                MessageBox.Show("登录成功！");
           
                MessageBox.Show("欢迎 " + username + "！");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this)?.Close();

            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
               
            }
        }

        // 🚀 3. 读取用户数据
        private List<User> LoadUsers()
        {
            if (!File.Exists(userFile)) return new List<User>();

            string json = File.ReadAllText(userFile);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        // 🚀 4. 保存用户数据
        private void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userFile, json);
        }
    }

    // 🚀 **用户数据模型**
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; } // **不加密，直接存明文密码**
    }

}

