using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Ecom
{
    public class RescheduleOrCancelAppointmentResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("awb")]
        public string Awb { get; set; } = string.Empty;
        [JsonPropertyName("error")]
        public string[]? Error { get; set; }
    }
}
