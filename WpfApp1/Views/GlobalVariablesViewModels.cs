using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public static class GlobalVariablesViewModels
    {
        // 存储 ViewModel 实例的静态字段
        public static MainViewModels CurrentMainViewModel { get; set; }
        
    }
}
