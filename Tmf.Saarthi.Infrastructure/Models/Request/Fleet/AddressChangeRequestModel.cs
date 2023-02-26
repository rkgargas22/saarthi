using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Fleet;

public class AddressChangeRequestModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("isAddressChange")]
    public bool IsAddressChange { get; set; }
    [JsonPropertyName("updatedBy")]
    public long UpdatedBy { get; set; }
    [JsonPropertyName("updatedDate")]
    public DateTime? UpdatedDate { get; set; }
}
