using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

public class BlackListedDetailsResponse
{
    [JsonPropertyName("blackListedId")]
    public long BlackListedId { get; set; }
}
