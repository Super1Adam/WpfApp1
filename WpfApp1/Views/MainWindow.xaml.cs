using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;
using WpfApp1.ViewModels;
using WpfApp1.Models;
using System.Globalization;
using System.Diagnostics;
using LiveCharts;
using System.Security.Cryptography.X509Certificates;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();


            this.DataContext = new MainViewModels();
            



            //mediaElement.Source = new Uri(@"E:\gird\WpfApp1\WpfApp1\Contorls\fengji.mp4", UriKind.Absolute);


            //// 播放视频
            //mediaElement.MediaEnded += MediaElement_MediaEnded;
            //mediaElement.Play();

        }
        //private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        //{
        //    // 视频播放完毕后重新播放
        //    mediaElement.Position = TimeSpan.Zero;  // 将视频进度设置为开头
        //    mediaElement.Play();                    // 重新播放视频
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CartesianChart_Loaded_1(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            RunPythonScript();
           

            var viewModel = this.DataContext as MainViewModels;
            if (viewModel != null)
            {
                viewModel.WindSpeedValues.Clear();
                viewModel.TemperaturValues.Clear();
                viewModel.HumidityValues.Clear();
                viewModel.WindPowerDensity.Clear();
                string datacsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "weather_data_with_wpd_new.csv");

                var data = ReadWindSpeedDataFromCsv(datacsv);
                foreach (var value in data.Select(d => d.WindSpeed))
                {
                    viewModel.WindSpeedValues.Add(value);
                    
                }
                foreach (var value in data.Select(d => d.Temperature))
                {
                    viewModel.TemperaturValues.Add(value);
                }
                foreach (var value in data.Select(d => d.Humidity))
                {
                    viewModel.HumidityValues.Add(value);
                }
                foreach (var value in data.Select(d => d.WindPowerDensity))
                {
                    viewModel.WindPowerDensity.Add(value);
                }




                viewModel.WindSpeedValuesZF.Clear();
                viewModel.TemperaturValuesZF.Clear();
                viewModel.HumidityValuesZF.Clear();
                viewModel.WindPowerDensityZF.Clear();
                string datazf = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "frequency_data_new.csv");



                var dataZF = ReadWindSpeedDataFromCsv1(datazf);
                foreach (var value in dataZF.Select(d => d.WindSpeed))
                {
                    viewModel.WindSpeedValuesZF.Add(value);
                }
                foreach (var value in dataZF.Select(d => d.Temperature))
                {
                    viewModel.TemperaturValuesZF.Add(value);
                }
                foreach (var value in dataZF.Select(d => d.Humidity))
                {
                    viewModel.HumidityValuesZF.Add(value);
                }
                foreach (var value in dataZF.Select(d => d.WindPowerDensity))
                {
                    viewModel.WindPowerDensityZF.Add(value);
                }
                //// 创建随机数生成器
                //Random random = new Random();

                //// 更新 WindSpeedValues 属性，生成一个新的范围在 60 到 90 之间的随机值
                //viewModel.WindSpeedValues.Clear();  // 清空当前的值

                //// 添加 12 个新的随机值（可以根据实际需要调整数量）
                //for (int i = 0; i < 12; i++)
                //{
                //    viewModel.WindSpeedValues.Add(random.NextDouble() * (90 - 60) + 60); // 随机数在 60 到 90 之间
                //}

            }
        }

        private List<WindSpeedData> ReadWindSpeedDataFromCsv(string filePath)//月均温度直线图
        {
            var data = new List<WindSpeedData>();

            using (var reader = new StreamReader(filePath))
            {
                var line = reader.ReadLine(); // 跳过表头

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    if (values.Length < 5) continue; // 跳过不完整的数据行

                    data.Add(new WindSpeedData
                    {
                        TimeRange = values[0],
                        WindSpeed = double.TryParse(values[1], out var windSpeed) ? windSpeed : 0,
                        Temperature = double.TryParse(values[2], out var temp) ? temp : 0,
                        Humidity = double.TryParse(values[3], out var hum) ? hum : 0,
                        WindPowerDensity = double.TryParse(values[4], out var wpd) ? wpd : 0
                    });
                }
            }

            return data;
        }

        private ObservableCollection<zhifangtu> ReadWindSpeedDataFromCsv1(string filePath)//统计直方图计算
        {
            var data = new ObservableCollection<zhifangtu>();

            using (var reader = new StreamReader(filePath))
            {
                var line = reader.ReadLine();  // 跳过表头

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    var windSpeed = double.Parse(values[0]);
                    var temperature = double.Parse(values[1]);
                    var humidity = double.Parse(values[2]);
                    var windPowerDensity = double.Parse(values[3]);


                    data.Add(new zhifangtu
                    {
                        WindSpeed = windSpeed
                           ,
                        Temperature = temperature
                                        ,
                        Humidity = humidity,
                        WindPowerDensity = windPowerDensity
                    });
                }
            }

            return data;
        }
        private void RunPythonScript()//运行python脚本，计算月均数据变化折线图，和数据统计直方图
        {


            string filePathToCsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "weather_data_with_wpd_new.csv");
            string frequentFilePathToCsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "frequency_data_new.csv");


            DateTime? startDateSelected = StartDatePicker.SelectedDate;
            DateTime? endDateSelected = EndDatePicker.SelectedDate;
            string? filePath = FilePathTextBox.Text;

            if (startDateSelected == null || endDateSelected == null || filePath== null)
            {
                MessageBox.Show("请选择有效的开始日期，结束日期和输入文件路径！");
                return;
            }

            string startDate = startDateSelected.Value.ToString("yyyy-MM-dd") + "T00:00:00";
            string endDate = endDateSelected.Value.ToString("yyyy-MM-dd") + "T00:00:00";





            
            string latitude = LatitudeTextBox.Text.Trim();
            string longitude = LongitudeTextBox.Text.Trim();

            // 校验输入
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate) ||
                string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude))
            {
                MessageBox.Show("Please enter all required fields.");
                return;
            }

            string pythonExePath = @"C:\Users\Adam_\.conda\envs\py1\python.exe";
            string pythonScriptPath = @"E:\gird\pythonProject\inputopen.py";

           
            // 拼接参数字符串，注意空格分隔
            string arguments = $"\"{pythonScriptPath}\" \"{startDate}\" \"{endDate}\" \"{latitude}\" \"{longitude}\" \"{filePath}\" \"{filePathToCsv}\" \"{frequentFilePathToCsv}\"";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = pythonExePath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            // 添加环境变量（如果需要设置路径）
            string ecCodesPath = @"C:\Users\Adam_\.conda\envs\py1\Library\bin";
            startInfo.EnvironmentVariables["PATH"] = ecCodesPath + ";" + Environment.GetEnvironmentVariable("PATH");

            try
            {
                using (Process process = Process.Start(startInfo))
                {
                    if (process == null)
                    {
                        MessageBox.Show("Failed to start process.");
                        return;
                    }

                    // 读取输出
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(output))
                    {
                        MessageBox.Show("Output: " + output);
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show("Error: " + error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            //string pythonExePath = @"C:\Users\Adam_\.conda\envs\py1\python.exe";
            //string pythonScriptPath = @"E:\gird\pythonProject\inputopen.py";

            //// 定义参数
            //string startDate = "2024-01-01T00:00:00";
            //string endDate = "2024-01-29T00:00:00";
            //string latitude = "-0.25";
            //string longitude = "-9.75";

            //// 拼接参数字符串
            //string arguments = $"\"{pythonScriptPath}\" \"{startDate}\" \"{endDate}\" \"{latitude}\" \"{longitude}\"";

            //ProcessStartInfo startInfo = new ProcessStartInfo
            //{
            //    FileName = pythonExePath,
            //    Arguments = arguments,
            //    UseShellExecute = false,
            //    RedirectStandardOutput = true,
            //    RedirectStandardError = true,
            //    CreateNoWindow = true
            //};
            //string ecCodesPath = @"C:\Users\Adam_\.conda\envs\py1\Library\bin";
            //startInfo.EnvironmentVariables["PATH"] = ecCodesPath + ";" + Environment.GetEnvironmentVariable("PATH");
            //try
            //{
            //    using (Process process = Process.Start(startInfo))
            //    {
            //        if (process == null)
            //        {
            //            MessageBox.Show("Failed to start process.");
            //            return;
            //        }

            //        // 读取输出
            //        string output = process.StandardOutput.ReadToEnd();
            //        string error = process.StandardError.ReadToEnd();

            //        process.WaitForExit();

            //        if (!string.IsNullOrEmpty(output))
            //        {
            //            MessageBox.Show("Output: " + output);
            //        }

            //        if (!string.IsNullOrEmpty(error))
            //        {
            //            MessageBox.Show("Error: " + error);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择文件",
                Filter = "所有文件 (*.*)|*.*|文本文件 (*.txt)|*.txt|GRIB文件 (*.grib)|*.grib",
                InitialDirectory = @"C:\"
            };

            // 如果用户选择了文件并点击确定
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件路径并显示到 TextBox
                string selectedFilePath = openFileDialog.FileName;
                FilePathTextBox.Text = selectedFilePath;

                // 进一步处理选定文件路径
                MessageBox.Show($"您选择的文件路径为：{selectedFilePath}");
            }
        }


        //private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    if (openFileDialog.ShowDialog() == true)
        //        txtEditor.Text = File.ReadAllText(openFileDialog.FileName);

        //}

    }
}