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
    public class MainViewModels : PropertyChangedBase
    {
        public SeriesCollection SeriesCollection { get; set; }

        

        public ObservableCollection<string> TimeLabels { get; set; }

        public ChartValues<double> WindSpeedValues { get; set; }
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


    


        public SelectDate StartDate { get; set; }     
        public DateTime?  DateTime {  get; set; }
        public SelectDate EndDate { get; set; } 


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
            string dataZf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "frequency_data.csv");
            string datascada = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "processed_output_12_bins.csv");

            // 读取 CSV 文件并转换成 WindSpeedData 对象列表
            var data = ReadWindSpeedDataFromCsv(datacsv);

            // 转化为 LiveCharts 图表数据
            WindSpeedValues = new ChartValues<double>(data.Select(d => d.WindSpeed));
            TemperaturValues = new ChartValues<double>(data.Select(d => d.Temperature));
            HumidityValues = new ChartValues<double>(data.Select(d => d.Humidity));
            WindPowerDensity = new ChartValues<double>(data.Select(d => d.WindPowerDensity));
            var dataZF = ReadWindSpeedDataFromCsv1(dataZf);
            WindSpeedValuesZF = new ChartValues<double>(dataZF.Select(d => d.WindSpeed));
            TemperaturValuesZF = new ChartValues<double>(dataZF.Select(d => d.Temperature));
            HumidityValuesZF = new ChartValues<double>(dataZF.Select(d => d.Humidity));
            WindPowerDensityZF = new ChartValues<double>(dataZF.Select(d => d.WindPowerDensity));
            var dataScada = ReadWindSpeedDataFromCsvScada(datascada);
            WindSpeed =new ChartValues<double> (dataScada.Select(d => d.WindSpeed));
            Power = new ChartValues<double>(dataScada.Select(d => d.Power));
            Vane =new ChartValues<double>(dataScada.Select ((d) => d.Vane));
            GearboxSpeed = new ChartValues<double>(dataScada.Select (d => d.GearboxSpeed));
            Generator = new ChartValues<double>(dataScada.Select ((d) => d.Generator));
            Voltage = new ChartValues<double>(dataScada.Select (((d) => d.Voltage)));






        }

    }

}
