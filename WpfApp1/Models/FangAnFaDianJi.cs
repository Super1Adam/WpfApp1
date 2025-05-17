using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Conmmon;

namespace WpfApp1.Models
{
   public class FangAnFaDianJi : PropertyChangedBase
    {
        private string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; SetPropertyChanged(); }

        }
        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; SetPropertyChanged(); }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set { time = value; SetPropertyChanged(); }

        }
        public string ImagePath { get; set; } // 图片路径字段

    }
}
