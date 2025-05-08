using System;
using System.Collections.Generic;
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
using System.Text.Json;
using System.Collections.ObjectModel;
namespace WpfApp1.Views
{
    /// <summary>
    /// ShuJuGengXingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShuJuGengXingWindow : Window
    {
        private ObservableCollection<WindTurbine> turbines = new ObservableCollection<WindTurbine>();
        private string dataFile = "data.json";

        public ShuJuGengXingWindow()
        {
            InitializeComponent();
            LoadData();
            TurbineListView.ItemsSource = turbines;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var turbine = new WindTurbine
            {
                BladeDiameter = BladeDiameterTextBox.Text,
                HubHeight = HubHeightTextBox.Text,
                CutOutSpeed = CutOutSpeedTextBox.Text,
                SweptArea = SweptAreaTextBox.Text,
                RatedPower = RatedPowerTextBox.Text,
                CutInArea = CutInAreaTextBox.Text,
                GearboxRatio = GearboxRatioTextBox.Text
            };

            turbines.Add(turbine);
            SaveData();
            ClearInputs();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (TurbineListView.SelectedItem is WindTurbine selected)
            {
                selected.BladeDiameter = BladeDiameterTextBox.Text;
                selected.HubHeight = HubHeightTextBox.Text;
                selected.CutOutSpeed = CutOutSpeedTextBox.Text;
                selected.SweptArea = SweptAreaTextBox.Text;
                selected.RatedPower = RatedPowerTextBox.Text;
                selected.CutInArea = CutInAreaTextBox.Text;
                selected.GearboxRatio = GearboxRatioTextBox.Text;

                TurbineListView.Items.Refresh();
                SaveData();
                ClearInputs();
             }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (TurbineListView.SelectedItem is WindTurbine selected)
            {
                turbines.Remove(selected);
                SaveData();
            }
        }
        private void LoadData()
        {
            if (File.Exists(dataFile))
            {
                var json = File.ReadAllText(dataFile);
                var loaded = JsonSerializer.Deserialize<ObservableCollection<WindTurbine>>(json);
                if (loaded != null)
                    turbines = loaded;
            }
        }
        private void SaveData()
        {
            var json = JsonSerializer.Serialize(turbines, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataFile, json);
        }
        private void ClearInputs()
        {
            BladeDiameterTextBox.Clear();   
            HubHeightTextBox.Clear();
            CutOutSpeedTextBox.Clear();
            SweptAreaTextBox.Clear();
            RatedPowerTextBox.Clear();
            CutInAreaTextBox.Clear();
            GearboxRatioTextBox.Clear();
        }
        private void TurbineListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 减去滚动条宽度和边框等可能偏移
            double totalWidth = TurbineListView.ActualWidth - 35;
            int columnCount = MainGridView.Columns.Count;
            double columnWidth = totalWidth / columnCount;

            foreach (var col in MainGridView.Columns)
            {
                col.Width = columnWidth;
            }
        }

    }
}
