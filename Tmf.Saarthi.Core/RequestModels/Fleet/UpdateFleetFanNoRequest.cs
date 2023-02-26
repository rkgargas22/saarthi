using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class UpdateFleetFanNoRequest
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;
}
