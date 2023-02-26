using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

public class UpdateFleetVehicleRCResponse
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("rCNo")]
    public string RCNo { get; set; } = string.Empty;
}
