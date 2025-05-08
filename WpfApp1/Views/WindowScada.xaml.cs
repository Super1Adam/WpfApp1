using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// WindowScada.xaml 的交互逻辑
    /// </summary>
    public partial class WindowScada : Window
    {
        public WindowScada()
        {
            InitializeComponent();
            this.DataContext = new MainViewModels();
        }
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
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
        private void Button_ShuJuDaoRu(object sender, RoutedEventArgs e)

        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Button_ShouMingYuCe(object sender, RoutedEventArgs e)

        {
            if (GlobalVariables.IsEra5DataImported)
            {
                Window1 mainWindow = new Window1();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // 如果数据未导入，弹出提示信息
                MessageBox.Show("请先导入ERA5数据！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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


        private void Button_ClickBack(object sender, RoutedEventArgs e)

        {
            this.Close();
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择文件",
                Filter = "CSV文件 (*.csv)|*.csv|所有文件 (*.*)|*.*",
                //InitialDirectory = @"E:\gird\wind\WpfApp1\WpfApp1\bin\Debug\net8.0-windows\Data"
            };

            // 如果用户选择了文件并点击确定
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件路径并显示到 TextBox
                string selectedFilePath = openFileDialog.FileName;
                FilePathTextBox2.Text = selectedFilePath;

                // 进一步处理选定文件路径
                MessageBox.Show($"您选择的文件路径为：{selectedFilePath},正在读取信息。");
                RunPythonScriptWithSCADA(FilePathTextBox2.Text);

            }
            else {
                MessageBox.Show("请选择正确的文件路径");
            
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择文件",
                Filter = "CSV文件 (*.csv)|*.csv|所有文件 (*.*)|*.*",
                //InitialDirectory = @"E:\gird\wind\WpfApp1\WpfApp1\bin\Debug\net8.0-windows\Data"
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
        private void RunPythonScriptWithSCADA(string inputFilePath)//提取运行数据,选出展示的列
        {
          

            string pythonExePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "myenv", "python.exe");
            string pythonScriptPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pythonProject", "scada6.py");


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

            string ecCodesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "myenv", "Library", "bin");
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
                    if (outputLines.Length >= 6)
                    {
                        var EnvironmentalInfoString = outputLines[0].Substring("环境量： ".Length).Trim();
                        var PowerGenerationInfoString = outputLines[1].Substring("发电信息： ".Length).Trim();
                        var BladeListString = outputLines[2].Substring("叶片： ".Length).Trim();
                        var GearboxListString = outputLines[3].Substring("齿轮箱： ".Length).Trim();
                        var GeneratorListString = outputLines[4].Substring("发电机： ".Length).Trim();
                        var ConverterListString = outputLines[5].Substring("变流器： ".Length).Trim();



                        // 将经纬度转换为列表
                        var EnvironmentalInfo = EnvironmentalInfoString.Split(',').Select(l => l.Trim()).ToList();
                        var PowerGenerationInfo = PowerGenerationInfoString.Split(',').Select(l => l.Trim()).ToList();
                        var BladeList = BladeListString.Split(',').Select(l => l.Trim()).ToList();
                        var GearboxList = GearboxListString.Split(',').Select(l => l.Trim()).ToList();
                        var GeneratorList = GeneratorListString.Split(',').Select(l => l.Trim()).ToList();
                        var ConverterList = ConverterListString.Split(',').Select(l => l.Trim()).ToList();



                        // 绑定数据
                        // 获取 ViewModel 并更新数据源
                        var viewModel = this.DataContext as MainViewModels;
                        if (viewModel != null)
                        {
                            viewModel.UpdateSCADALists(EnvironmentalInfo, PowerGenerationInfo, BladeList, GearboxList, GeneratorList, ConverterList);
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
                viewModel.TimeLabels1.Clear();
                var EnvironmentalInfoSlect = Combox1.SelectedItem;
                var PowerGenerationInofSelect =Combox2.SelectedItem;
                var BladeListSelect = Combox3.SelectedItem;
                var GearboxListSelect = Combox4.SelectedItem;
                var GeneratorListSelect = Combox5.SelectedItem;
                var ConverterListSelect = Combox6.SelectedItem;
                

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
                foreach (var value in data.Select(d => d.TimeRange))
                {
                    viewModel.TimeLabels1.Add(value);
                }



            }
        }

        private void RunPythonScript2()//运行python脚本，计算月均数据变化折线图，和数据统计直方图scada数据 运行数据 完成修改
        {

            var EnvironmentalInfoSlect = Combox1.SelectedItem;
            var PowerGenerationInofSelect = Combox2.SelectedItem;
            var BladeListSelect = Combox3.SelectedItem;
            var GearboxListSelect = Combox4.SelectedItem;
            var GeneratorListSelect = Combox5.SelectedItem;
            var ConverterListSelect = Combox6.SelectedItem;

            string filePathToCsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "processed_output_12_bins.csv");



            string? filePath = FilePathTextBox2.Text;

            if (filePath == null)
            {
                MessageBox.Show("请选择有效的输入文件路径！");
                return;
            }


            string pythonExePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "myenv", "python.exe");

            string pythonScriptPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pythonProject", "scad.py");



            // 拼接参数字符串，注意空格分隔
            string arguments = $"\"{pythonScriptPath}\"  \"{filePath}\" \"{filePathToCsv}\" \"{EnvironmentalInfoSlect}\" \"{PowerGenerationInofSelect}\" \"{BladeListSelect}\" \"{GearboxListSelect}\" \"{GeneratorListSelect}\" \"{ConverterListSelect}\"";

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
            string ecCodesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "myenv", "Library", "bin");
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
                        MessageBox.Show("Error: " + error);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
            }

        }
        private ObservableCollection<ScadaData> ReadWindSpeedDataFromCsvScada(string filePath)
        {
            var data = new ObservableCollection<ScadaData>();


            using (var reader = new StreamReader(filePath))
            {


                var line = reader.ReadLine();  // 跳过表头

                if (!string.IsNullOrEmpty(line))
                {
                    var headers = line.Split(',');
                    var viewModel = this.DataContext as MainViewModels;
                    // 假设 headers 的长度足够，否则要做额外检查
                    viewModel.YLabel1 = headers.Length > 1 ? headers[1] : "Column1";
                    viewModel.YLabel2 = headers.Length > 2 ? headers[2] : "Column2";
                    viewModel.YLabel3 = headers.Length > 3 ? headers[3] : "Column3";
                    viewModel.YLabel4 = headers.Length > 4 ? headers[4] : "Column4";
                    viewModel.YLabel5 = headers.Length > 5 ? headers[5] : "Column5";
                    viewModel.YLabel6 = headers.Length > 6 ? headers[6] : "Column6";
                }

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    var windSpeed = double.Parse(values[1]);
                    var temperature = double.Parse(values[2]);
                    var humidity = double.Parse(values[3]);
                    var windPowerDensity = double.Parse(values[4]);
                    var Five = double.Parse(values[5]);
                    var Six = double.Parse(values[6]);


                    data.Add(new ScadaData
                    {
                        TimeRange = values[0],

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
        private void RunPythonScript1()//运行python脚本，计算月均数据变化折线图，和数据统计直方图 故障数据 完成
        {


            string filePathToCsv = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "top_10_codes.csv");



            string? filePath = FilePathTextBox1.Text;

            if (filePath == null)
            {
                MessageBox.Show("请选择有效的输入文件路径！");
                return;
            }



            string pythonExePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "myenv", "python.exe");

            string pythonScriptPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pythonProject", "guzhang.py");




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
            string ecCodesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "myenv", "Library", "bin");
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
                        XLabels = temperature,

                    });
                }
            }
            return data;
        }
    }
}
