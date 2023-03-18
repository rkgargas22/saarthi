using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class GetDepartmentListResponse
{
    [JsonPropertyName("departmentCode")]
    public string DepartmentCode { get; set; } = string.Empty;

    [JsonPropertyName("departmentName")]
    public string DepartmentName { get; set; } = string.Empty;
}
