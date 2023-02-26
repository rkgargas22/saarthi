using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

public class DeleteFleetVehicleResponse
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
}
