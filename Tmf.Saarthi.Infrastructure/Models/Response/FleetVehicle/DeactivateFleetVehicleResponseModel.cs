using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.FleetVehicle;

public class DeactivateFleetVehicleResponseModel
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
}
