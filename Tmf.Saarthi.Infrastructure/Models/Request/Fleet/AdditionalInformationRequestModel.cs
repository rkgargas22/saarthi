using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Fleet;

public class AdditionalInformationRequestModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("additionalInformation")]
    public string AdditionalInformation { get; set; } = string.Empty;
    [JsonPropertyName("departmentType")]
    public string DepartmentType { get; set; } = string.Empty;
    [JsonPropertyName("updatedBy")]
    public long UpdatedBy { get; set; }
    [JsonPropertyName("updatedDate")]
    public DateTime UpdatedDate { get; set; }
}
