using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Ocr; 

public class VoterIdExtractRequestModel 
{
    [JsonPropertyName("task_id")] 
    public string TaskId { get; set; } = string.Empty;

    [JsonPropertyName("group_id")] 
    public string GroupId { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public DataVoterId Data { get; set; }
}

public class DataVoterId
{
    [JsonPropertyName("document1")] 
    public string Document1 { get; set; } = string.Empty; 
}