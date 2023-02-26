using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Payment; 

public class SavePaymentStatusRequest 
{
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;
    [JsonPropertyName("bPNumber")]
    public long BPNumber { get; set; }
    [JsonPropertyName("txnID")]
    public string TxnID { get; set; } = string.Empty;
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;
    [JsonPropertyName("txnNo")]
    public string TxnNo { get; set; } = string.Empty;
    [JsonPropertyName("txnDateTime")]
    public DateTime? TxnDateTime { get; set; }
}
