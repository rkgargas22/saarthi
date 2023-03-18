using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class AdditionalInformationRequest
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("additionalInformation")]
    public string AdditionalInformation { get; set; } = string.Empty;
    [JsonPropertyName("departmentType")]
    public string DepartmentType { get; set; } = string.Empty;
    [JsonPropertyName("documents")]
    public List<DocumentDataRequest> Documents { get; set; }
}
