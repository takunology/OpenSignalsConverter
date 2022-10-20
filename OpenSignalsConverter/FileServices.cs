using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSignalsConverter.Model;

namespace OpenSignalsConverter
{
    public static class FileServices
    {
        public static string ReadFileName { get; private set; }

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
                //Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public static void SaveFile()
        {
            Console.WriteLine("\n保存するファイルの名前をつけてください（空にすると読み込んだファイル名と同じ名前で保存します）");
            Console.Write("ファイル名：");
            string fileName = Console.ReadLine();
            
            try
            {
                if (fileName == "") fileName = ReadFileName.Remove(ReadFileName.Length - 4, 3);

                Console.Write("\n書き出し中...");
                using (StreamWriter sw = new StreamWriter(fileName + ".csv", true, Encoding.UTF8))
                {
                    sw.WriteLine($"計測日時, {WriteModel.Date}, {WriteModel.Time}");
                    sw.WriteLine($"時間,I1,I2,O1,O2,A1,A2,A3,A4,A5,A6");
                    foreach(var data in WriteModel.MeasurmentData)
                    {
                        sw.WriteLine(data);
                    }
                }
                Console.WriteLine("完了");
                Console.WriteLine("すべての作業が完了しました。");
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
