using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

public class AddFleetVehicleResponse
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("rCNo")]
    public string RCNo { get; set; } = string.Empty;
    [JsonPropertyName("errorMessage")]
    [JsonIgnore]
    public string ErrorMessage { get; set; } = string.Empty;
}
