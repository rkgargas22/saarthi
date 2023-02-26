using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Agent;

public class AssignFleetResponseModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
}
