using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.FI;

public class InitiateFIResponseModel
{
    [JsonPropertyName("responseData")]
    public ResponseData ResponseData { get; set; } = new ResponseData();

    [JsonPropertyName("errorCode")]
    public object ErrorCode { get; set; } = null!;

    [JsonPropertyName("errorMessage")]
    public object ErrorMessage { get; set; } = null!;
}

public class ResponseData
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