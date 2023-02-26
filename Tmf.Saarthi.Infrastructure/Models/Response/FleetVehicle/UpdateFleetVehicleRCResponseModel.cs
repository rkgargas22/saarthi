using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.FleetVehicle;

public class UpdateFleetVehicleRCResponseModel
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("rCNo")]
    public string RCNo { get; set; } = string.Empty;
}
