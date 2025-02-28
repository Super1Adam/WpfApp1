﻿using Microsoft.Win32;
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
        }
 
        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CartesianChart_Loaded_1(object sender, RoutedEventArgs e)
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
            jingjixingfenxi.ShowDialog();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                RunPythonScript();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("脚本错误");
            }


          

            var viewModel = this.DataContext as MainViewModels;
            if (viewModel != null)
            {
                
                // 检查并清除之前的数据
                if (viewModel.WindSpeedValues.Any()) viewModel.WindSpeedValues.Clear();
                if (viewModel.TemperaturValues.Any()) viewModel.TemperaturValues.Clear();
                if (viewModel.HumidityValues.Any()) viewModel.HumidityValues.Clear();
                if (viewModel.WindPowerDensity.Any()) viewModel.WindPowerDensity.Clear();

                // 读取新数据
                string datacsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "weather_data_with_wpd_new.csv");
                var data = ReadWindSpeedDataFromCsv(datacsv);

                // 添加新的数据到集合
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

                // 清除第二组数据
                if (viewModel.WindSpeedValuesZF.Any()) viewModel.WindSpeedValuesZF.Clear();
                if (viewModel.TemperaturValuesZF.Any()) viewModel.TemperaturValuesZF.Clear();
                if (viewModel.HumidityValuesZF.Any()) viewModel.HumidityValuesZF.Clear();
                if (viewModel.WindPowerDensityZF.Any()) viewModel.WindPowerDensityZF.Clear();

                // 读取第二组数据
                string datazf = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "frequency_data_new.csv");
                var dataZF = ReadWindSpeedDataFromCsv1(datazf);

                // 添加新的数据到第二组集合
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

        private ObservableCollection<YunXingShuJu> ReadYunXingShuJucsv(string filename)
        {
            var data = new ObservableCollection<YunXingShuJu>();
            using (var reader = new StreamReader(filename))
            {
                var line = reader.ReadLine();  // 跳过表头

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    var windSpeed = double.Parse(values[1]);
                    var temperature = values[0];


                    data.Add(new YunXingShuJu
                    {
                        YunXingShuJuValues = windSpeed
                           ,
                        XLabels = temperature
                                        ,

                    });
                }
            }
            return data;
        }
        private List<WindSpeedData> ReadWindSpeedDataFromCsv(string filePath)
        {
            var data = new List<WindSpeedData>();

            double totalTemperature = 0; // 累加温度
            double totalHumidity = 0;    // 累加湿度
            double totalWindSpeed = 0;   //累加风速
            int validEntries = 0;        // 有效数据行计数

            using (var reader = new StreamReader(filePath))
            {
                var line = reader.ReadLine(); // 跳过表头

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    if (values.Length < 5) continue; // 跳过不完整的数据行
                    totalWindSpeed += double.TryParse(values[1], out var windSpeed1) ? windSpeed1 : 0;
                    totalTemperature += double.TryParse(values[2], out var temp1) ? temp1 : 0;
                    totalHumidity += double.TryParse(values[3], out var hum1) ? hum1 : 0;
                    validEntries++;


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
            GlobalVariables.AvgTemperature = validEntries > 0 ? totalTemperature / validEntries : 0;
            GlobalVariables.AvgHumidity = validEntries > 0 ? totalHumidity / validEntries : 0;
            GlobalVariables.AvgWindSpeed = validEntries > 0 ? totalWindSpeed / validEntries : 0;

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



            var viewModel = this.DataContext as MainViewModels;

         

            string latitude = viewModel.SelectedLatitude.ToString();

            string longitude = viewModel.SelectedLongitude.ToString();

            // 校验输入
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate) ||
                string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude))
            {
                MessageBox.Show("Please enter all required fields.");
                return;
            }

            string pythonExePath = @"C:\Users\Adam_\.conda\envs\myenv\python.exe";

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
            string ecCodesPath = @"C:\Users\Adam_\.conda\envs\myenv\Library\bin";
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
                        MessageBox.Show("Output: " + "weather_data && frequence_data");
                    }

                    //if (!string.IsNullOrEmpty(error))
                    //{
                    //    MessageBox.Show("Error: " + error);
                    //}
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


        private void RunPythonScript1()//运行python脚本，计算月均数据变化折线图，和数据统计直方图 故障数据
        {


            string filePathToCsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "top_10_codes.csv");


       
            string? filePath = FilePathTextBox1.Text;

            if ( filePath == null)
            {
                MessageBox.Show("请选择有效的输入文件路径！");
                return;
            }



            string pythonExePath = @"C:\Users\Adam_\.conda\envs\myenv\python.exe";
            string pythonScriptPath = @"E:\gird\pythonProject\guzhang.py";


            // 拼接参数字符串，注意空格分隔
            string arguments = $"\"{pythonScriptPath}\"  \"{filePath}\" \"{filePathToCsv}\" ";

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
                        MessageBox.Show("读取成功");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
    
        }

        private void RunPythonScript2()//运行python脚本，计算月均数据变化折线图，和数据统计直方图scada数据 运行数据
        {


            string filePathToCsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "processed_output_12_bins.csv");



            string? filePath = FilePathTextBox2.Text;

            if (filePath == null)
            {
                MessageBox.Show("请选择有效的输入文件路径！");
                return;
            }



            string pythonExePath = @"C:\Users\Adam_\.conda\envs\myenv\python.exe";
            string pythonScriptPath = @"E:\gird\pythonProject\scad.py";


            // 拼接参数字符串，注意空格分隔
            string arguments = $"\"{pythonScriptPath}\"  \"{filePath}\" \"{filePathToCsv}\" ";

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
                        MessageBox.Show("读取成功");
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        //MessageBox.Show("Error: " + error);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void RunPythonScriptWithCoordinates(string inputFilePath)//经纬度提取
        {
            string pythonExePath = @"C:\Users\Adam_\.conda\envs\myenv\python.exe";
            string pythonScriptPath = @"E:\gird\pythonProject\jingweidutiqu.py"; // Python脚本路径

            // 拼接参数字符串
            string arguments = $"\"{pythonScriptPath}\" \"{inputFilePath}\"";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = pythonExePath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            string ecCodesPath = @"C:\Users\Adam_\.conda\envs\myenv\py1\Library\bin";
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
            string[] outputLines = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // 提取经纬度数据
            if (outputLines.Length >= 2)
            {
                var latitudesString = outputLines[0].Substring("Latitudes:".Length).Trim();
                var longitudesString = outputLines[1].Substring("Longitudes:".Length).Trim();

                // 将经纬度转换为列表
                var latitudesList = latitudesString.Split(',').Select(l => l.Trim()).ToList();
                var longitudesList = longitudesString.Split(',').Select(l => l.Trim()).ToList();
                List<double> latitudes = latitudesList.Select(l => double.Parse(l)).ToList();
                List<double> longitudes = longitudesList.Select(l => double.Parse(l)).ToList();

                        // 绑定数据
                        // 获取 ViewModel 并更新数据源
                        var viewModel = this.DataContext as MainViewModels;
                        if (viewModel != null)
                        {
                            viewModel.UpdateLatLonLists(latitudes, longitudes);
                        }

                    }

                    process.WaitForExit();
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error: {ex.Message}");
    }
        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择文件",
                Filter = "所有文件 (*.*)|*.*|文本文件 (*.txt)|*.txt|GRIB文件 (*.grib)|*.grib",
                InitialDirectory = @"E:\gird\wind\WpfApp1\WpfApp1\bin\Debug\net8.0-windows\Data"
            };

            // 如果用户选择了文件并点击确定
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件路径并显示到 TextBox
                string selectedFilePath = openFileDialog.FileName;
                FilePathTextBox.Text = selectedFilePath;

                // 进一步处理选定文件路径
                MessageBox.Show($"您选择的文件路径为：{selectedFilePath}");
                MessageBox.Show("正在读取经纬度信息");
                RunPythonScriptWithCoordinates(FilePathTextBox.Text);



            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
            var window1 =new Window1();
            window1.ShowDialog();
        }
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择文件",
                Filter = "CSV文件 (*.csv)|*.csv|所有文件 (*.*)|*.*",
                InitialDirectory = @"E:\gird\wind\WpfApp1\WpfApp1\bin\Debug\net8.0-windows\Data"
            };

            // 如果用户选择了文件并点击确定
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件路径并显示到 TextBox
                string selectedFilePath = openFileDialog.FileName;
                FilePathTextBox1.Text = selectedFilePath;

                // 进一步处理选定文件路径
                MessageBox.Show($"您选择的文件路径为：{selectedFilePath}");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            RunPythonScript1();


            var viewModel = this.DataContext as MainViewModels;
            if (viewModel != null)
            {
                //viewModel.YunXingShuJuValues.Clear();
                //viewModel.XLabels.Clear();

                string datacsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "top_10_codes.csv");

                var data = ReadYunXingShuJucsv(datacsv);
                foreach (var value in data.Select(d => d.YunXingShuJuValues))
                {
                    viewModel.YunXingShuJuValues.Add(value);

                }
                foreach (var value in data.Select(d => d.XLabels))
                {
                    viewModel.XLabels.Add(value);
                }



            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择文件",
                Filter = "CSV文件 (*.csv)|*.csv|所有文件 (*.*)|*.*",
                InitialDirectory = @"E:\gird\wind\WpfApp1\WpfApp1\bin\Debug\net8.0-windows\Data"
            };

            // 如果用户选择了文件并点击确定
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件路径并显示到 TextBox
                string selectedFilePath = openFileDialog.FileName;
                FilePathTextBox2.Text = selectedFilePath;

                // 进一步处理选定文件路径
                MessageBox.Show($"您选择的文件路径为：{selectedFilePath}");
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
       
            RunPythonScript2();
            var viewModel = this.DataContext as MainViewModels;
            if (viewModel != null)
            {
                viewModel.WindSpeed.Clear();
                viewModel.Power.Clear();
                viewModel.Vane.Clear();
                viewModel.GearboxSpeed.Clear();
                viewModel.Generator.Clear();
                viewModel.Voltage.Clear();
                string datacsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "processed_output_12_bins.csv");

                var data = ReadWindSpeedDataFromCsvScada(datacsv);
                foreach (var value in data.Select(d => d.WindSpeed))
                {
                    viewModel.WindSpeed.Add(value);

                }
                foreach (var value in data.Select(d => d.Power))
                {
                    viewModel.Power.Add(value);
                }
                foreach (var value in data.Select(d => d.Vane))
                {
                    viewModel.Vane.Add(value);
                }
                foreach (var value in data.Select(d => d.GearboxSpeed))
                {
                    viewModel.GearboxSpeed.Add(value);
                }
                foreach (var value in data.Select(d => d.Generator))
                {
                    viewModel.Generator.Add(value);
                }
                foreach (var value in data.Select(d => d.Voltage))
                {
                    viewModel.Voltage.Add(value);
                }




            }

        }
        private ObservableCollection<ScadaData> ReadWindSpeedDataFromCsvScada(string filePath)
        {
            var data = new ObservableCollection<ScadaData>();

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
                    var Five = double.Parse(values[4]);
                    var Six = double.Parse(values[5]);


                    data.Add(new ScadaData
                    {
                        WindSpeed = windSpeed
                           ,
                        Power = temperature
                                        ,
                        Vane = humidity,
                        GearboxSpeed = windPowerDensity,
                        Generator = Five,
                        Voltage = Six
                    });
                }
            }

            return data;
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

        private void test_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MainViewModels;
            // 修改经纬度的列表，例如，添加一些新的经纬度值
            if (viewModel != null)
            {
                // 清空当前列表并添加新的经纬度
                viewModel.LatitudeList.Clear();
                viewModel.LatitudeList.Add("38.0522");  // 示例值1
                viewModel.LatitudeList.Add("36.7783");  // 示例值2
                viewModel.LatitudeList.Add("37.7749");  // 示例值3
                viewModel.LatitudeList.Add("40.7128");  // 示例值4
                viewModel.SelectedLatitude = viewModel.LatitudeList[0];
            }
            //private void btnOpenFile_Click(object sender, RoutedEventArgs e)
            //{
            //    OpenFileDialog openFileDialog = new OpenFileDialog();
            //    if (openFileDialog.ShowDialog() == true)
            //        txtEditor.Text = File.ReadAllText(openFileDialog.FileName);

            //}
        }

        }
}