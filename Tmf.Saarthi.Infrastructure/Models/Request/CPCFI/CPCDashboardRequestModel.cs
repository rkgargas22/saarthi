using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.CPCFI;

public class CPCDashboardRequestModel
{
    [JsonPropertyName("agentId")]
    public long AgentId { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;
}
