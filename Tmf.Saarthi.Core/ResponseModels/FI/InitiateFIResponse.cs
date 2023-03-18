using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.FI;

public class InitiateFIResponse
{
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;

    [JsonPropertyName("bpNo")]
    public string BpNo { get; set; } = string.Empty;

    [JsonPropertyName("transactionId")]
    public string TransactionId { get; set; } = string.Empty;

    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}
