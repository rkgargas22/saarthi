using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Email;

public class SendAgentEmailRequest
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("mobileNo")]
    public string MobileNo { get; set; } = string.Empty;

    [JsonPropertyName("module")]
    public string Module { get; set; } = string.Empty;

    [JsonPropertyName("subModule")]
    public string SubModule { get; set; } = string.Empty;

    [JsonPropertyName("template")]
    public string Template { get; set; } = string.Empty;
}
