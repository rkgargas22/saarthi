using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class GetAdditionalInfoResponseModel
{
    [JsonPropertyName("additionalInfoId")]
    public long AdditionalInfoId { get; set; }
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    [JsonPropertyName("departmentCode")]
    public string DepartmentCode { get; set; } = string.Empty;
    [JsonPropertyName("fleetId")]
    public long FleetID { get; set; }
    [JsonPropertyName("stageCode")]
    public string StageCode { get; set; } = string.Empty;
    [JsonPropertyName("assignedTo")]
    public string AssignedTo { get; set; } = string.Empty;
    [JsonPropertyName("roleName")]
    public string RoleName { get; set; } = string.Empty;
}
