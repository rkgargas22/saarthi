using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Agent;

public class AgentListDataRequestModel
{
    [JsonPropertyName("userType")]
    public string UserType { get; set; } = string.Empty;

    [JsonPropertyName("agentId")]
    public long? AgentId { get; set; }
}
