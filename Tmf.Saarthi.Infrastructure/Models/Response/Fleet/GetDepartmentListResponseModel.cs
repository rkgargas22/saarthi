using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Fleet; 

public class GetDepartmentListResponseModel 
{
    [JsonPropertyName("departmentCode")]
    public string DepartmentCode { get; set; } = string.Empty;

    [JsonPropertyName("departmentName")]
    public string DepartmentName { get; set; } = string.Empty;
}
