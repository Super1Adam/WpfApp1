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

namespace WpfApp1.ViewModels
{
    public class MainViewModels : PropertyChangedBase
    {
        public LiveCharts.SeriesCollection MyProperty { get; set; }
        public ChartValues<ObservableValue> ScadaData { get; set; }

        public ChartValues<ObservableValue> ScadaDatb { get; set; }
        public ChartValues<ObservableValue> ScadaDatc { get; set; }
        public ChartValues<ObservableValue> ScadaDatd { get; set; }
        public ChartValues<ObservableValue> ScadaDate { get; set; }
        public List <string> Alarms { get; set; }
        public List<BadItemModel> BadScatter { get; set; }



        private ObservableCollection<SaleBillboard> saleBillboards;
        public ObservableCollection<SaleBillboard> SaleBillboards
        {
            get { return saleBillboards; }
            set { saleBillboards = value; }
        }

        public MainViewModels()
        {
            ScadaData = new ChartValues<ObservableValue>();   
            ScadaDatb = new ChartValues<ObservableValue>();
            ScadaDatc = new ChartValues<ObservableValue>();
            ScadaDatd = new ChartValues<ObservableValue>();
            ScadaDate = new ChartValues<ObservableValue>();


            Random random = new Random();
            for (int i = 0; i < 12; i++)
            {
                ScadaData.Add(new ObservableValue(random.Next(20, 380)));
                ScadaDatb.Add(new ObservableValue(random.Next(20, 300)));
                ScadaDatc.Add(new ObservableValue(random.Next(20, 300)));
                ScadaDatd.Add(new ObservableValue(random.Next(20, 300)));
                ScadaDate.Add(new ObservableValue(random.Next(20, 300)));

            }

            Alarms = new List<string>();
            Alarms.Add("扇片寿命大于50% 可以继续使用");
            Alarms.Add("齿轮箱寿命大于50% 可以继续使用");
            Alarms.Add("变速器寿命小于30% 建议维修");
            Alarms.Add("变流器寿命小于10% 建议报废");
            BadScatter = new List<BadItemModel>();
            string[] BadNames = new string[] { "叶片", "齿轮箱", "发电机", "变流器"};
            for (int i = 0; i < BadNames.Length; i++)
            {
                BadScatter.Add(new BadItemModel() { Title = BadNames[i], Size = 180 - 20 * i, Value = 0.9 - 0.1 * i });
            }
        }

        private void Init()
        {
            SaleBillboards = new() {
            new SaleBillboard{ Ranking=1,ProjectName="万科金域曦府",SaleCount=3621,SaleAmount=65421234},
            new SaleBillboard{ Ranking=2,ProjectName="星河智荟",SaleCount=3214,SaleAmount=564363535},
            new SaleBillboard{ Ranking=3,ProjectName="珠江天郦",SaleCount=3225,SaleAmount=45635},
            new SaleBillboard{ Ranking=4,ProjectName="龙湖天著",SaleCount=3142,SaleAmount=64646},
            new SaleBillboard{ Ranking=5,ProjectName="金地半山风华",SaleCount=2954,SaleAmount=34535},
            new SaleBillboard{ Ranking=6,ProjectName="富力南驰·富颐华庭",SaleCount=2865,SaleAmount=45646},
            new SaleBillboard{ Ranking=7,ProjectName="星河东悦湾",SaleCount=2841,SaleAmount=34534563},
            new SaleBillboard{ Ranking=8,ProjectName="合生湖山国际",SaleCount=2754,SaleAmount=34536345},
            new SaleBillboard{ Ranking=9,ProjectName="星瀚TOD",SaleCount=2541,SaleAmount=5464535},
            new SaleBillboard{ Ranking=10,ProjectName="珠江铂世湾",SaleCount=2425,SaleAmount=3453535},
            };
        }
        }

}
