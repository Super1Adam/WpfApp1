using System;
using System.Collections.Generic;
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
    /// DataImportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataImportWindow : Window
    {
        public DataImportWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModels();
            MyDataGrid.Items.Add(new SaleBillboard
            {
                Ranking = 1,
                ProjectName = "一号机",
                SaleCount = 3621,
                SaleAmount = 65421234
            });

            MyDataGrid.Items.Add(new SaleBillboard
            {
                Ranking = 2,
                ProjectName = "二号机",
                SaleCount = 3214,
                SaleAmount = 564363535
            });
            MyDataGrid1.Items.Add(new SaleBillboard
            {
                Ranking = 1,
                ProjectName = "一号机",
                SaleCount = 3621,
                SaleAmount = 65421234
            });

            MyDataGrid1.Items.Add(new SaleBillboard
            {
                Ranking = 2,
                ProjectName = "二号机",
                SaleCount = 3214,
                SaleAmount = 564363535
            });
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //    txtEditor.Text = File.ReadAllText(openFileDialog.FileName);

        }
    }
}
