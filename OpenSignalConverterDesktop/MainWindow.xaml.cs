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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenSignalConverterDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButton(object sender, RoutedEventArgs e)
        {
            var result = FileServices.OpenFile();
            if (result)
            {
                _filePath.Content = FileServices.OpenFileName;
                _status.Content = "変換中...";
                result = ConverterServices.FileRead(FileServices.OpenFileName);
                if (result)
                {
                    _status.Content = "完了";
                }
                else
                {
                    _status.Content = "失敗";
                    return;
                }
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            var result = FileServices.SaveFile();
            _ = result ? _status.Content = "保存しました" : "" ; 
        }
    }
}
