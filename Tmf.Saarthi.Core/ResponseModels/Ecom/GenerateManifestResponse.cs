using System.Text.Json.Serialization;


namespace Tmf.Saarthi.Core.ResponseModels.Ecom
{
    public class GenerateManifestResponse
    {
        [JsonPropertyName("awb_number")]
        public int AwbNumber { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;

        [JsonPropertyName("order_number")]
        public string OrderNumber { get; set; } = string.Empty;

        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }

}
