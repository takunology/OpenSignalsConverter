using OpenSignalsConverter.Model;
using System.Text;

namespace OpenSignalsConverter
{
    public static class FileServices
    {
        public static string ReadFileName { get; private set; }
        public static string CurrentFilePath { get; private set; }

        public static void ReadFile()
        {
            string fileName = "";

            while(fileName == "")
            {
                Console.WriteLine("読み込むファイルのパスを、拡張子含めて入力してください。");
                Console.Write("ファイル名：");
                fileName = Console.ReadLine();
                try
                {
                    if(fileName == "")
                    {
                        Console.WriteLine("ファイル名を空にすることはできません。");
                        continue;
                    }
                    ReadFileName = fileName;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ファイルを読み込めませんでした。");
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            try
            {
                using (StreamReader sr = new(fileName))
                {
                    string line;
                    sr.ReadLine(); //1行目は読み捨てる
                    string header = sr.ReadLine().Remove(0,1); //2行目はヘッダ情報
                    sr.ReadLine(); //3行目も読み捨てる

                    // ここより下の行は実験データ
                    List<string> measurementData = new List<string>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        measurementData.Add(line);
                    }

                    ConverterServices.Convert(ref header, ref measurementData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ファイルの読み込みに失敗しました。");
                throw new Exception(ex.Message);
            }

        }

        public static void SaveFile()
        {
            Console.WriteLine("\n保存するファイルの名前をつけてください（空にすると読み込んだファイルと同じ名前で保存します）");
            Console.Write("ファイル名：");
            string fileName = Console.ReadLine();

            try
            {
                var sb = new StringBuilder(ReadFileName);

                if (fileName == "")
                {              
                    fileName = sb.Replace(".txt", ".csv").ToString();
                }
                else
                {
                    if(!fileName.Contains(".csv"))
                    {
                        fileName = fileName + ".csv";
                    }
                }
                
                Console.WriteLine($"\n{fileName} に書き出します。");
                Console.Write("書き出し中...");

                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    sw.WriteLine($"測定機器名, {WriteModel.DeviceName}");
                    sw.WriteLine($"機器ID, {WriteModel.DeviceId}");
                    sw.WriteLine($"測定日, {WriteModel.Date}");
                    sw.WriteLine($"測定時刻, {WriteModel.Time}");
                    sw.WriteLine($"サンプリング周波数[Hz], {WriteModel.SampleRate}");
                    sw.WriteLine($"");
                    sw.WriteLine($"時間[ms],I1,I2,O1,O2,A1,A2,A3,A4,A5,A6");
                    foreach(var data in WriteModel.MeasurmentData)
                    {
                        sw.WriteLine(data);
                    }
                }
                Console.WriteLine("完了");
                Console.WriteLine("すべての作業が完了しました。");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ファイルを書き込めませんでした。");
                Console.WriteLine(ex.Message);
                return;
            }
        }

    }
}
