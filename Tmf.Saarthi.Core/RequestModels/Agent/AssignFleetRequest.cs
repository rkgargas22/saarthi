using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Agent;

public class AssignFleetRequest
{
    [JsonPropertyName("fleetIDs")]
    public List<int> FleetIDs { get; set; }
    [JsonPropertyName("agentId")]
    public long AgentId { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;
}
