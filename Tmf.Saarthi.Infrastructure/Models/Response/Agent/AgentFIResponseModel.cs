using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Agent;

public class AgentFIResponseModel
{
    [JsonPropertyName("fleetId")]
    public long FleetId { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }
}
