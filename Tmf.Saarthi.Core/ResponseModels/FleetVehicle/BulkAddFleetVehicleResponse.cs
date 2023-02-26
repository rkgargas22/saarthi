using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

public class BulkAddFleetVehicleResponse
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("vehicles")]
    public List<AddFleetVehicleResponse> Vehicles { get; set; } = new List<AddFleetVehicleResponse>();

}
