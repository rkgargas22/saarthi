using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Agent;

public class AgentHsitoryResponseModel
{
    [JsonPropertyName("bpNo")]
    public long BpNo { get; set; }

    [JsonPropertyName("agentName")]
    public string AgentName { get; set; } = string.Empty;

    [JsonPropertyName("fleetId")]
    public long FleetId { get; set; }

    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    [JsonPropertyName("logDate")]
    public DateTime LogDate { get; set; }

    [JsonPropertyName("StageName")]
    public string StageName { get; set; } = string.Empty;
}
