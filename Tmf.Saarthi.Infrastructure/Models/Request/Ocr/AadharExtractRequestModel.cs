using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Ocr;

public class AadharExtractRequestModel
{
    [JsonPropertyName("task_id")]
    public string TaskId { get; set; } = string.Empty;

    [JsonPropertyName("group_id")]
    public string GroupId { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public DataAadhar Data { get; set; }
}

public class DataAadhar
{
    [JsonPropertyName("document1")]
    public string Document1 { get; set; } = string.Empty;

    [JsonPropertyName("document2")]
    public string Document2 { get; set; } = string.Empty;

    [JsonPropertyName("consent")]
    public string Consent { get; set; } = string.Empty;
}