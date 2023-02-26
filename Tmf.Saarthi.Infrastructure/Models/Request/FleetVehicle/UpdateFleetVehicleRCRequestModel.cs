using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;

public class UpdateFleetVehicleRCRequestModel
{
    [JsonPropertyName("rCNo")]
    public string RCNo { get; set; } = string.Empty;
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
    [JsonPropertyName("isSubmitted")]
    public bool IsSubmitted { get; set; }
    [JsonPropertyName("updatedBy")]
    public long UpdatedBy { get; set; }
    [JsonPropertyName("updatedDate")]
    public DateTime UpdatedDate { get; set; }
}
