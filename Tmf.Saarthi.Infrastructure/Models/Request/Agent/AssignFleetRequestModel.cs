using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Agent;

public class AssignFleetRequestModel
{
    [JsonPropertyName("fleetIDs")]
    public string FleetIDs { get; set; } = string.Empty;
    [JsonPropertyName("agentId")]
    public long AgentId { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;
    [JsonPropertyName("updatedBy")]
    public long UpdatedBy { get; set; }
    [JsonPropertyName("updatedDate")]
    public DateTime? UpdatedDate { get; set; }
}
