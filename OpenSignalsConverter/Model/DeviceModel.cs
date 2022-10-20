using System.Text.Json.Serialization;

namespace OpenSignalsConverter.Model
{
    public class DeviceModel
    {
        [JsonPropertyName("device")]
        public string? Name { get; set; }
        [JsonPropertyName("device connection")]
        public string? Id { get; set; }
        [JsonPropertyName("sampling rate")]
        public int? SampleRate { get; set; }
        [JsonPropertyName("date")]
        public string? Date { get; set; }
        [JsonPropertyName("time")]
        public string? Time { get; set; }
    }
}
