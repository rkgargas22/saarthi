using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Document;

public class DocumentRequest
{
    [JsonPropertyName("fleetId")]
    public long FleetId { get; set; }

    [JsonPropertyName("docTypeId")]
    public int DocTypeId { get; set; }

    [JsonPropertyName("stageId")]
    public int StageId { get; set; }

    [JsonPropertyName("extension")]
    public string Extension { get; set; } = string.Empty;

    [JsonPropertyName("documentName")]
    public string DocumentName { get; set; } = string.Empty;

    [JsonPropertyName("entityCode")]
    public string EntityCode { get; set; } = string.Empty;

    [JsonPropertyName("entityId")]
    public long EntityId { get; set; }
}
