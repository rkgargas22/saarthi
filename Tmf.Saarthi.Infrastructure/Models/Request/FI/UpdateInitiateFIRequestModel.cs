using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FI;

public class UpdateInitiateFIRequestModel
{
    [JsonPropertyName("fleetId")]
    public long  FleetId { get; set; }

    [JsonPropertyName("queueId")]
    public long QueueId { get; set; }

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

    [JsonPropertyName("createdBy")]
    public long CreatedBy { get; set; }
}
