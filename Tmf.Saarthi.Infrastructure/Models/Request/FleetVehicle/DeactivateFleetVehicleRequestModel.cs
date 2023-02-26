using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;

public class DeactivateFleetVehicleRequestModel
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
    [JsonPropertyName("updatedBy")]
    public long UpdatedBy { get; set; }
    [JsonPropertyName("updatedDate")]
    public DateTime UpdatedDate { get; set; }
}
