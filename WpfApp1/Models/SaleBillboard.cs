using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Conmmon;
namespace WpfApp1.Models
{
 
        public class SaleBillboard : PropertyChangedBase
        {
            /// <summary>
            /// 排名
            /// </summary>
            private int ranking;

            public int Ranking
            {
                get { return ranking; }
                set { ranking = value; SetPropertyChanged(); }
            }



            /// <summary>
            /// 项目
            /// </summary>
            private string projectName;

            public string ProjectName
            {
                get { return projectName; }
                set { projectName = value; SetPropertyChanged(); }
            }



            /// <summary>
            /// 销售量
            /// </summary>
            private int saleCount;

            public int SaleCount
            {
                get { return saleCount; }
                set { saleCount = value; SetPropertyChanged(); }
            }



            /// <summary>
            /// 销售额
            /// </summary>
            private double saleAmount;

            public double SaleAmount
            {
                get { return saleAmount; }
                set { saleAmount = value; SetPropertyChanged(); }
            }
        }
    }
