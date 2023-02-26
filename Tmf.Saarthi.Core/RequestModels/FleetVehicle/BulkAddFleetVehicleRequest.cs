using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.FleetVehicle;

public class BulkAddFleetVehicleRequest
{
    [JsonPropertyName("rCNos")]
    public List<string> RCNoList { get; set; } = new List<string>();
}
