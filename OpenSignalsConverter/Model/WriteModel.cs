namespace OpenSignalsConverter.Model
{
    public static class WriteModel
    {
        public static string? DeviceName { get; set; }
        public static string? DeviceId { get; set; }
        public static int? SampleRate { get; set; }
        public static string? Date { get; set; }
        public static string? Time { get; set; }
        // time 1 + channel 10
        public static List<string> MeasurmentData { get; set; } = new List<string>();
    }
}
