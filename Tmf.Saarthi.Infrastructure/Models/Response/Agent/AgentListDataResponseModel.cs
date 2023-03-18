using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Agent;

public class AgentListDataResponseModel
{
    [JsonPropertyName("empId")]
    public long EmpId { get; set; }

    [JsonPropertyName("empName")]
    public string EmpName { get; set; } = string.Empty;
}
