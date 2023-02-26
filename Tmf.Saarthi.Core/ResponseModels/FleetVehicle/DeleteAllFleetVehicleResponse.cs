using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

public class DeleteAllFleetVehicleResponse
{
    [JsonPropertyName("fleetId")]
    public long FleetId { get; set; }
}
