using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Payment;

public class GetRazorPayStatusResponseModel
{
    [JsonPropertyName("txnId")]
    public string TxnId { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;

    [JsonPropertyName("utrNo")]
    public string UtrNo { get; set; } = string.Empty;

    [JsonPropertyName("sapdocNo")]
    public string SapdocNo { get; set; } = string.Empty;

    [JsonPropertyName("postingDate")]
    public string PostingDate { get; set; } = string.Empty;
}
