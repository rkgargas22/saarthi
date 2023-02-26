using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;

public class GetFleetVehicleByFleetIDRequestModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
}
