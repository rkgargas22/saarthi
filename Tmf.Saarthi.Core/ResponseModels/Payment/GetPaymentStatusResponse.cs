using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Payment;

public class GetPaymentStatusResponse
{
    [JsonPropertyName("paymentID")]
    public long PaymentID { get; set; }
    [JsonPropertyName("reqId")]
    public string ReqId { get; set; } = string.Empty;
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("payEMI")]
    public string PayEMI { get; set; } = string.Empty;
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;
    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }
    [JsonPropertyName("mobileNo")]
    public string MobileNo { get; set; } = string.Empty;
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;
    [JsonPropertyName("transactionId")]
    public string TransactionId { get; set; } = string.Empty;
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("transactionDateTime")]
    public DateTime? TransactionDateTime { get; set; }
    [JsonPropertyName("utrNo")]
    public string UtrNo { get; set; } = string.Empty;
    [JsonPropertyName("sapdocNo")]
    public string SapdocNo { get; set; } = string.Empty;
    [JsonPropertyName("postingDate")]
    public DateTime? PostingDate { get; set; }
}
