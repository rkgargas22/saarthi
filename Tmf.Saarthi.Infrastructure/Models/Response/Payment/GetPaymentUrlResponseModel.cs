using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Payment;

public class GetPaymentUrlResponseModel
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
