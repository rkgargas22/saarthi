using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Ocr;

public class ValidateDocumentRequestModel
{
    [JsonPropertyName("task_id")]
    public string TaskId { get; set; } = string.Empty;

    [JsonPropertyName("group_id")]
    public string GroupId { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public Data Data { get; set; }
}

public class AdvancedFeatures
{
    [JsonPropertyName("detect_doc_side")]
    public bool DetectDocSide { get; set; }
}

public class Data
{
    [JsonPropertyName("document1")]
    public string Document1 { get; set; } = string.Empty;

    [JsonPropertyName("doc_type")]
    public int DocType { get; set; } = 0;

    [JsonPropertyName("advanced_features")]
    public AdvancedFeatures AdvancedFeatures { get; set; }
}