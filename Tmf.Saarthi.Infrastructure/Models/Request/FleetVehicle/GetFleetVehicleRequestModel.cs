using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;

public class GetFleetVehicleRequestModel
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
}
