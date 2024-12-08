using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
   public  class WindSpeedData
    {
        public string TimeRange { get; set; }
        public double WindSpeed { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindPowerDensity { get; set; }
    }
}
