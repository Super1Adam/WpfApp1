using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public static class GlobalVariables
    {
        private static double _avgTemperature;
        private static double _avgHumidity;
        private static double _avgWindSpeed;

        // 静态属性，用于存储和更新
        public static double AvgTemperature
        {
            get => _avgTemperature;
            set
            {
                _avgTemperature = value;
                UpdateAccelerationFactors();
            }
        }

        public static double AvgHumidity
        {
            get => _avgHumidity;
            set
            {
                _avgHumidity = value;
                UpdateAccelerationFactors();
            }
        }

        public static double AvgWindSpeed
        {
            get => _avgWindSpeed;
            set
            {
                _avgWindSpeed = value;
                UpdateAccelerationFactors();
            }
        }

        // 加速因子 A（针对整体和各部件）
        public static double OverallAccelerationFactor { get; private set; }
        public static double BladeAccelerationFactor { get; private set; }
        public static double GearboxAccelerationFactor { get; private set; }
        public static double GeneratorAccelerationFactor { get; private set; }
        public static double ConverterAccelerationFactor { get; private set; }

        // 运维策略影响因子 k，范围 0.8 ~ 1.2
        public static double MaintenanceFactor { get; set; } = 1.0;

        // 参考参数
        public static double ReferenceHumidity { get; set; } = 50.0; // RH_s
        public static double ReferenceTemperature { get; set; } = 25; // T_s (25°C in Kelvin)
        public static double ReferenceWindSpeed { get; set; } = 12.0; // v_s

        // k1, k2, k3 参数（针对不同部件）
        public static (double K1, double K2, double K3) OverallKs { get; set; } = (1.0, 1.0, 1.0);
        public static (double K1, double K2, double K3) BladeKs { get; set; } = (1.0, 1.0, 1.0);
        public static (double K1, double K2, double K3) GearboxKs { get; set; } = (1.0, 1.0, 1.0);
        public static (double K1, double K2, double K3) GeneratorKs { get; set; } = (1.0, 1.0, 1.0);
        public static (double K1, double K2, double K3) ConverterKs { get; set; } = (1.0, 1.0, 1.0);

        /// <summary>
        /// 更新加速因子 A 的计算
        /// </summary>
        private static void UpdateAccelerationFactors()
        {
            if (_avgHumidity == 0 || _avgTemperature == 0 || _avgWindSpeed == 0)
            {
                OverallAccelerationFactor = BladeAccelerationFactor = GearboxAccelerationFactor =
                    GeneratorAccelerationFactor = ConverterAccelerationFactor = 0;
                return;
            }

            try
            {
                OverallAccelerationFactor = CalculateAccelerationFactor(OverallKs);
                BladeAccelerationFactor = CalculateAccelerationFactor(BladeKs);
                GearboxAccelerationFactor = CalculateAccelerationFactor(GearboxKs);
                GeneratorAccelerationFactor = CalculateAccelerationFactor(GeneratorKs);
                ConverterAccelerationFactor = CalculateAccelerationFactor(ConverterKs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating acceleration factors: {ex.Message}");
                OverallAccelerationFactor = BladeAccelerationFactor = GearboxAccelerationFactor =
                    GeneratorAccelerationFactor = ConverterAccelerationFactor = 0;
            }
        }

        /// <summary>
        /// 计算单个部件的加速因子 A
        /// </summary>
        /// <param name="ks">k1, k2, k3 参数</param>
        /// <returns>加速因子 A</returns>
        private static double CalculateAccelerationFactor((double K1, double K2, double K3) ks)
        {
            var (k1, k2, k3) = ks;

            return MaintenanceFactor * Math.Exp(
                //k1 * Math.Log(ReferenceHumidity / _avgHumidity) +
                k2 * (1 / _avgTemperature - 1 / ReferenceTemperature) +
                k3 * Math.Log(_avgWindSpeed / ReferenceWindSpeed)
            );
        }
    }
}
