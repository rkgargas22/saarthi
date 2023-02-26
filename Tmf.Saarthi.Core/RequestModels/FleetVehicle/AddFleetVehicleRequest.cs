using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.FleetVehicle;

public class AddFleetVehicleRequest
{
    [JsonPropertyName("rCNo")]
    public string RCNo { get; set; } = string.Empty;
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
}
