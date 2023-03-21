using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Agent;

public class SendToDeviationAgentResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
