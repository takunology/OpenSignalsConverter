using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace OpenSignalConverterDesktop
{
    public static class ConverterServices
    {
        // ファイル読み込み
        public static bool FileRead(string fileName)
        {
            try
            {
                using (StreamReader sr = new(fileName))
                {
                    string line;
                    sr.ReadLine(); //1行目は読み捨てる
                    string header = sr.ReadLine().Remove(0, 1); //2行目はヘッダ情報
                    sr.ReadLine(); //3行目も読み捨てる

                    // ここより下の行は実験データ
                    List<string> measurementData = new List<string>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        measurementData.Add(line);
                    }
                    Convert(ref header, ref measurementData);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"読み込みに失敗しました。\n{ex.Message}");
                return false;
            }
        }

        // 測定データ変換
        private static bool Convert(ref string header, ref List<string> measurementData)
        {
            try
            {
                // 測定デバイスと日時の記録
                Header(ref header);
                // 測定データの記録
                Measurement(measurementData);
                // データ保存用に整形する
                SaveFormat();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"変換に失敗しました。\n{ex.Message}");
                return false;
            }
        }

        // 測定データ解析
        private static async void Measurement(List<string> measurementData)
        {
            int time = 1;
            foreach (var line in measurementData)
            {
                string[] split = line.Split("\t");
                split[0] = time.ToString();
                time++;

                StringBuilder row = new StringBuilder("");
                foreach (var element in split)
                {
                    row.Append($"{element},");
                    await Task.Delay(1);
                }
                row.Remove(row.Length - 2, 2);
                WriteModel.MeasurmentData.Add(row.ToString());
            }
        }

        // ヘッダー解析
        private static void Header(ref string header)
        {
            // ヘッダーから最初のキーを削除して閉じる括弧も消す
            StringBuilder sb = new StringBuilder(header);
            sb.Remove(1, 22);
            sb.Remove(sb.Length - 1, 1);

            var json = JsonSerializer.Deserialize<DeviceModel>(sb.ToString());
            WriteModel.DeviceName = json.Name;
            WriteModel.DeviceId = json.Id;
            WriteModel.SampleRate = json.SampleRate;
            WriteModel.Date = json.Date;
            WriteModel.Time = json.Time;
        }

        // 保存用データフォーマット整形
        private static void SaveFormat()
        {
            DataModel.CSV = "";
            try
            {
                DataModel.CSV += $"測定機器名, {WriteModel.DeviceName}\n";
                DataModel.CSV += $"機器ID, {WriteModel.DeviceId}\n";
                DataModel.CSV += $"測定日, {WriteModel.Date}\n";
                DataModel.CSV += $"測定時刻, {WriteModel.Time}\n";
                DataModel.CSV += $"サンプリング周波数[Hz], {WriteModel.SampleRate}" + "\n\n";
                DataModel.CSV += $"時間[ms],I1,I2,O1,O2,A1,A2,A3,A4,A5,A6" + "\n";
                foreach (var data in WriteModel.MeasurmentData)
                {
                    DataModel.CSV += $"{data}\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
