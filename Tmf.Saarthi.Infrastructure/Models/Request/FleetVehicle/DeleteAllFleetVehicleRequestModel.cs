using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;

public class DeleteAllFleetVehicleRequestModel
{
    [JsonPropertyName("fleetId")]
    public long FleetID { get; set; }
}
