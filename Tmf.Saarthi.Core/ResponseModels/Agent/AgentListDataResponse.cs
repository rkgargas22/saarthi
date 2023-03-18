using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Agent;

public class AgentListDataResponse
{
    [JsonPropertyName("empId")]
    public long EmpId { get; set; }

    [JsonPropertyName("empName")]
    public string EmpName { get; set; } = string.Empty;
}
