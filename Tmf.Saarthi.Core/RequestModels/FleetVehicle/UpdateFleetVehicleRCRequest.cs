using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.FleetVehicle;

public class UpdateFleetVehicleRCRequest
{
    [JsonPropertyName("rCNo")]
    public string RCNo { get; set; } = string.Empty;
}
