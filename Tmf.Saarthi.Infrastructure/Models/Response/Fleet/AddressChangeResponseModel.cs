using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Fleet;

public class AddressChangeResponseModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
}
