using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Fleet;

public class UpdateFleetAmountResponseModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
}
