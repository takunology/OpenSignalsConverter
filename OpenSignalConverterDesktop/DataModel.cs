using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenSignalConverterDesktop
{
    public static class DataModel
    {
        public static string CSV { get; set; } = "";
    }

    public class DeviceModel
    {
        [JsonPropertyName("device")]
        public string? Name { get; set; }
        [JsonPropertyName("device name")]
        public string? Id { get; set; }
        [JsonPropertyName("sampling rate")]
        public int? SampleRate { get; set; }
        [JsonPropertyName("date")]
        public string? Date { get; set; }
        [JsonPropertyName("time")]
        public string? Time { get; set; }
    }

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
