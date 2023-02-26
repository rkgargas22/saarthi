using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class UpdateFleetAmountResponse
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
}
