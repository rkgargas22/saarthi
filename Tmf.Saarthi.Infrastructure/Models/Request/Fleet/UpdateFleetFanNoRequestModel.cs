using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Fleet;

public class UpdateFleetFanNoRequestModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;
    [JsonPropertyName("updatedBy")]
    public long UpdatedBy { get; set; }
    [JsonPropertyName("updatedDate")]
    public DateTime? UpdatedDate { get; set; }
}
