using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Conmmon;
using WpfApp1.Models;
using System.Globalization;
using System.IO;
using System.Windows;

namespace WpfApp1.ViewModels
{
    public class   MainViewModels : PropertyChangedBase
    {
        public SeriesCollection SeriesCollection { get; set; }

        public string SelectedLatitude { get; set; }
        public string SelectedLongitude { get; set; }


        public ObservableCollection<string> LatitudeList { get; set; }
        public ObservableCollection<string> LongitudeList { get; set; }
     

        public ObservableCollection<string> TimeLabels { get; set; }

        public ChartValues<double> WindSpeedValues { get; set; }
        public ChartValues<double> YunXingShuJuValues { get; set; }

        public List<string> XLabels { get; set; }
        public ChartValues<double> TemperaturValues { get; set; }
        public ChartValues<double> HumidityValues { get; set; }
        public ChartValues<double> WindPowerDensity { get; set; }




        public ChartValues<double> WindSpeedValuesZF { get; set; }
        public ChartValues<double> TemperaturValuesZF { get; set; }
        public ChartValues<double> HumidityValuesZF { get; set; }
        public ChartValues<double> WindPowerDensityZF { get; set; }


        public ChartValues<double> WindSpeed { get; set; }
        public ChartValues<double> Power { get; set; }
        public ChartValues<double> Vane { get; set; }
        public ChartValues<double> GearboxSpeed { get; set; }
        public ChartValues<double> Generator { get; set; }
        public ChartValues<double> Voltage { get; set; }



        public ObservableCollection<string> Alarms { get; set; }


        public SelectDate StartDate { get; set; }     
        public DateTime?  DateTime {  get; set; }
        public SelectDate EndDate { get; set; }
        public List<string> Combobox1 { get; set; }
        public List<string> Combobox2 { get; set; }


        private ObservableCollection<SaleBillboard> saleBillboards;
        public ObservableCollection<SaleBillboard> SaleBillboards
        {
            get { return saleBillboards; }
            set { saleBillboards = value; }
        }
     
        private ObservableCollection<WindSpeedData> ReadWindSpeedDataFromCsv2(string filePath)
        {
            var data = new ObservableCollection<WindSpeedData>();

            using (var reader = new StreamReader(filePath))
            {
                var line = reader.ReadLine();  // 跳过表头

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    var timeRange = values[0];  // 假设 values[0] = "2024-01-03 - 2024-01-05"
                    var startDateString = timeRange.Split(' ')[0];  // 提取日期 "2024-01-03"

                    var windSpeed = double.Parse(values[1]);

                    data.Add(new WindSpeedData
                    {
                        WindSpeed = windSpeed
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
                    validEntries ++;
                    

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
                        XLabels  = temperature
                                        ,
                    
                    });
                }
            }
            return data ;
        }

        private ObservableCollection<zhifangtu> ReadWindSpeedDataFromCsv1(string filePath)
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

        public MainViewModels()
        {

  
            EndDate = new SelectDate();
            StartDate = new SelectDate();
               string datacsv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "weather_data_with_wpd.csv");
            string datazf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "frequency_data.csv");
            string datascada = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "processed_output_12_bins.csv");
            string dataYunXingShuJu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "top_10_codes.csv");

            //// 读取 CSV 文件并转换成 WindSpeedData 对象列表
            //var data = ReadWindSpeedDataFromCsv(datacsv);

            //// 转化为 LiveCharts 图表数据
            //WindSpeedValues = new ChartValues<double>(data.Select(d => d.WindSpeed));
            //TemperaturValues = new ChartValues<double>(data.Select(d => d.Temperature));
            //HumidityValues = new ChartValues<double>(data.Select(d => d.Humidity));
            //WindPowerDensity = new ChartValues<double>(data.Select(d => d.WindPowerDensity));
            //var dataZF = ReadWindSpeedDataFromCsv1(datazf);
            //WindSpeedValuesZF = new ChartValues<double>(dataZF.Select(d => d.WindSpeed));
            //TemperaturValuesZF = new ChartValues<double>(dataZF.Select(d => d.Temperature));
            //HumidityValuesZF = new ChartValues<double>(dataZF.Select(d => d.Humidity));
            //WindPowerDensityZF = new ChartValues<double>(dataZF.Select(d => d.WindPowerDensity));
            //var dataScada = ReadWindSpeedDataFromCsvScada(datascada);
            //WindSpeed =new ChartValues<double> (dataScada.Select(d => d.WindSpeed));
            //Power = new ChartValues<double>(dataScada.Select(d => d.Power));
            //Vane =new ChartValues<double>(dataScada.Select ((d) => d.Vane));
            //GearboxSpeed = new ChartValues<double>(dataScada.Select (d => d.GearboxSpeed));
            //Generator = new ChartValues<double>(dataScada.Select ((d) => d.Generator));
            //Voltage = new ChartValues<double>(dataScada.Select (((d) => d.Voltage)));

            //var datayxsj = ReadYunXingShuJucsv(dataYunXingShuJu);
            //YunXingShuJuValues = new ChartValues<double>(datayxsj.Select(d => d.YunXingShuJuValues));
            //XLabels = new List<string>(datayxsj.Select(d => d.XLabels));
            // 初始化空的 SeriesCollection
            SeriesCollection = new SeriesCollection();

            // 初始化空的 ChartValues
            WindSpeedValues = new ChartValues<double>();
            TemperaturValues = new ChartValues<double>();
            HumidityValues = new ChartValues<double>();
            WindPowerDensity = new ChartValues<double>();

            WindSpeedValuesZF = new ChartValues<double>();
            TemperaturValuesZF = new ChartValues<double>();
            HumidityValuesZF = new ChartValues<double>();
            WindPowerDensityZF = new ChartValues<double>();

            WindSpeed = new ChartValues<double>();
            Power = new ChartValues<double>();
            Vane = new ChartValues<double>();
            GearboxSpeed = new ChartValues<double>();
            Generator = new ChartValues<double>();
            Voltage = new ChartValues<double>();
            YunXingShuJuValues = new ChartValues<double>();
            // 初始化空的 ObservableCollection 和 List
            TimeLabels = new ObservableCollection<string>();
            XLabels = new List<string>();
            Alarms = new ObservableCollection<string>();

            // 初始化日期选择器
            StartDate = new SelectDate();
            EndDate = new SelectDate();
            LatitudeList = new ObservableCollection<string> { "0" }; // 实验数据1: 经纬度
            LongitudeList = new ObservableCollection<string> { "0" }; // 实验数据2: 经度
            SelectedLatitude = LatitudeList[0];  // 默认选中第一个经纬度
            SelectedLongitude = LongitudeList[0];  // 默认选中第一个经度

        }
        public void UpdateLatLonLists(List<double> latitudes, List<double> longitudes)
        {
            // 清空现有的数据
            LatitudeList.Clear();
            LongitudeList.Clear();

            // 将新的经纬度数据添加到 ObservableCollection 中
            foreach (var latitude in latitudes)
            {
                LatitudeList.Add(latitude.ToString("F2")); // 保留2位小数格式
            }

            foreach (var longitude in longitudes)
            {
                LongitudeList.Add(longitude.ToString("F2"));
            }
        }
    }

}
