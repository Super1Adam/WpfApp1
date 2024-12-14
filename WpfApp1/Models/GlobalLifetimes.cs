using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public static class GlobalLifetimes
    {
        // 默认寿命百分比
        public static double BladeLife { get; set; } = 20.0;       // 叶片
        public static double GearboxLife { get; set; } = 70.0;     // 齿轮箱
        public static double GeneratorLife { get; set; } = 20.0;   // 发电机
        public static double ConverterLife { get; set; } = 20.0;   // 变流器
    }
}
