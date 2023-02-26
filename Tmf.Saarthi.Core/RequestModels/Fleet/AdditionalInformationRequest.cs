using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class AdditionalInformationRequest
{
    [JsonPropertyName("additionalInformation")]
    public string AdditionalInformation { get; set; } = string.Empty;
    [JsonPropertyName("departmentType")]
    public string DepartmentType { get; set; } = string.Empty;
}
