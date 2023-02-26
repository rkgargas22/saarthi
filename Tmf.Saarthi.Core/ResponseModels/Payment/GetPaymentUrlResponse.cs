using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Payment;

public class GetPaymentUrlResponse
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
