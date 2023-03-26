
using System.Text.Json.Serialization;


namespace Tmf.Saarthi.Core.ResponseModels.Ecom
{
    public class PushShipmentTrackResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
