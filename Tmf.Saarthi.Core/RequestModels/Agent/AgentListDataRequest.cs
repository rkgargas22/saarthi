using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Agent;

public class AgentListDataRequest
{
    [JsonPropertyName("userType")]
    public string UserType { get; set; } = string.Empty;

    [JsonPropertyName("agentId")]
    public long? AgentId { get; set; }
}
