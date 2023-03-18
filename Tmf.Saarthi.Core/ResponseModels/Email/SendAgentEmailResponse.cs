using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Email;

public class SendAgentEmailResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
