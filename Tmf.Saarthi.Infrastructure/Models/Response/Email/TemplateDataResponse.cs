using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Email;

public class TemplateDataResponse
{
    [JsonPropertyName("templateId")]
    public long TemplateId { get; set; }
    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;
    [JsonPropertyName("templateType")]
    public string TemplateType { get; set; } = string.Empty;
    [JsonPropertyName("module")]
    public string Module { get; set; } = string.Empty;
    [JsonPropertyName("subModule")]
    public string SubModule { get; set; } = string.Empty;
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
    [JsonPropertyName("createdBy")]
    public long CreatedBy { get; set; }
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
    [JsonPropertyName("updatedBy")]
    public long? UpdatedBy { get; set; }
    [JsonPropertyName("updatedDate")]
    public DateTime? UpdatedDate { get; set; }
}
