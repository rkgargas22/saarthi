using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

public class GetFleetVehicleResponse
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("rCNo")]
    public string RCNo{ get; set; } = string.Empty;
}
