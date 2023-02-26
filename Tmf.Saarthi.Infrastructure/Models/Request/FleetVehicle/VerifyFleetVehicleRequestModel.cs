using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;

public class VerifyFleetVehicleRequestModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("vehicleModels")]
    public List<VehicleModel> VehicleModels { get; set; }
}

public class VehicleModel
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
    [JsonPropertyName("rCNo")]
    public string RCNo { get; set; } = string.Empty;
}
