using System.Text;
using System.Text.Json;
using OpenSignalsConverter.Model;

namespace OpenSignalsConverter
{
    public static class ConverterServices
    {
        
        public static void Convert(ref string header, ref List<string> measurementData)
        {
            Console.Write("\nファイル情報を読み込み中...");
            // 測定デバイスと日時の記録
            Header(ref header);
            Console.WriteLine("完了");
            // 測定データの記録
            Console.Write("ファイルを変換中...");
            Measurement(ref measurementData);
            Console.WriteLine("完了");
        }

        private static void Measurement(ref List<string> measurementData)
        {
            char[] progressBar = { '／', '―', '＼', '｜' };
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
                }
                row.Remove(row.Length - 2, 2);
                WriteModel.MeasurmentData.Add(row.ToString());
                

                //Console.WriteLine((WriteModel.MeasurmentData.Count / measurementData.Count));
                // 進捗表示
                /*if (((time / measurementData.Count)) == 0)
                {
                    Console.CursorLeft = 0;
                    Console.Write("ファイルを変換しています...");
                    Console.Write(progressBar[time % 4]);
                    //Console.Write("{0, 4:d0}%", (time / measurementData.Count));
                    Console.Write((time / measurementData.Count).ToString("P1"));
                    Console.SetCursorPosition(0, Console.CursorTop);
                }*/
            }
        }

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



    }
}
