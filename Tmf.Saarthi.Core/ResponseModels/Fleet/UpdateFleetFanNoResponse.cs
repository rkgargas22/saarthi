using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class UpdateFleetFanNoResponse
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
}
