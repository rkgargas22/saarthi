using System.Text.Json.Serialization;
using Tmf.Saarthi.Core.RequestModels.Fleet;

namespace Tmf.Saarthi.Core.RequestModels.Agent;

public class SendToDeviationAgentRequest
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("vehicleIds")]
    public List<long> VehicleIds { get; set; }
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    [JsonPropertyName("documents")]
    public List<DocumentDataRequest> Documents { get; set; }
}
