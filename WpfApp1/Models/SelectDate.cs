using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Conmmon;

namespace WpfApp1.Models
{
    public  class SelectDate : PropertyChangedBase
    {

        private DateTime? _selectedDate;

        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                  
                }
            }
        }
    }
}
