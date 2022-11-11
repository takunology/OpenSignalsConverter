using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenSignalConverterDesktop
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged を実装するためのイベントハンドラ
        public event PropertyChangedEventHandler PropertyChanged;

        // プロパティ名によって自動的にセットされる
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private int _StatusValue = 0;
        // 入力テキスト用のプロパティ
        public int StatusValue
        {
            get { return _StatusValue; }
            // 値をセットした後、画面側でも値が反映されるように通知する
            set
            {
                OnPropertyChanged();
            }
        }

    }
}
