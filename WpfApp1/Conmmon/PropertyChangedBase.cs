using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.Conmmon
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        // 事件声明
        public event PropertyChangedEventHandler PropertyChanged;

        // 通知属性变更的方法
        protected void SetPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // 如果有监听事件，则触发 PropertyChanged 事件
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
