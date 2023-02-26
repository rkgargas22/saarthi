using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Ocr; 

public class DocumentMaskingRequestModel 
{
    [JsonPropertyName("task_id")]
    public string TaskId { get; set; } = string.Empty;

    [JsonPropertyName("group_id")]
    public string GroupId { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public DataMasking Data { get; set; }
}

public class DataMasking
{
    [JsonPropertyName("document1")]
    public string Document1 { get; set; } = string.Empty;

    [JsonPropertyName("consent")]
    public string Consent { get; set; } = string.Empty;
}