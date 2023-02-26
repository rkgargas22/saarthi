using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Email;

public class TemplateDataRequest
{
    [JsonPropertyName("module")]
    public string Module { get; set; } = string.Empty;
    [JsonPropertyName("subModule")]
    public string SubModule { get; set; } = string.Empty;
    [JsonPropertyName("templateType")]
    public string TemplateType { get; set; } = string.Empty;
}
