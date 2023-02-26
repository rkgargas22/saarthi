using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;

public class DeleteFleetVehicleRequestModel
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
}
