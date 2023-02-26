using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class VerifyFleetRequest
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
}
