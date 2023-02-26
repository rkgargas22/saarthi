using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Email;

public class SendEmailRequest
{
    [JsonPropertyName("bPNumber")]
    public long BPNumber { get; set; }

    [JsonPropertyName("module")]
    public string Module { get; set; } = string.Empty;

    [JsonPropertyName("subModule")]
    public string SubModule { get; set; } = string.Empty;

    [JsonPropertyName("templateType")]
    public string TemplateType { get; set; } = string.Empty;
}
