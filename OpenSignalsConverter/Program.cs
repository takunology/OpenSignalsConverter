namespace OpenSignalsConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== OpenSignals converter ver1.0.0 / Nihon Univ, Biophys Lab. ===\n");
            FileServices.ReadFile();
            FileServices.SaveFile();
        }
    }
}