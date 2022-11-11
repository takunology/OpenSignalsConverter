using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;

namespace OpenSignalConverterDesktop
{
    public static class FileServices
    {
        public static string OpenFileName { get; private set; } = "";

        public static bool OpenFile()
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Title = "ファイルを開く";
                dialog.Filter = "txtファイル(*.txt*)|*.txt*";
                if (dialog.ShowDialog() == true)
                {
                    OpenFileName = dialog.FileName;
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool SaveFile()
        {
            try
            {
                var dialog = new SaveFileDialog();
                dialog.Title = "ファイルの保存";
                dialog.Filter = "csvファイル|*.csv";
                if (dialog.ShowDialog() == true)
                {
                    // CSV 書き込み
                    File.WriteAllText(dialog.FileName, DataModel.CSV, Encoding.UTF8);
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }            
        }

    }
}
