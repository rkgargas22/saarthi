using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Ocr; 

public class ValidateDocumentResponseModel 
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;

    [JsonPropertyName("completed_at")]
    public DateTime CompletedAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("group_id")]
    public string GroupId { get; set; } = string.Empty;

    [JsonPropertyName("request_id")]
    public string RequestId { get; set; } = string.Empty;

    [JsonPropertyName("result")]
    public ResultValidaDoc Result { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("task_id")]
    public string TaskId { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}

public class ResultValidaDoc
{
    [JsonPropertyName("detected_doc_side")]
    public string DetectedDocSide { get; set; } = string.Empty;

    [JsonPropertyName("detected_doc_type")]
    public string DetectedDocType { get; set; } = string.Empty;

    [JsonPropertyName("is_readable")]
    public bool IsReadable { get; set; }

    [JsonPropertyName("readability")]
    public Readability Readability { get; set; }
}

public class Readability
{
    [JsonPropertyName("confidence")]
    public int Confidence { get; set; }
}
