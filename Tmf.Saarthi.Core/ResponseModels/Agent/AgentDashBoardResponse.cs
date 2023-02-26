using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Agent;

public class AgentDashBoardResponse
{
    [JsonPropertyName("fleedId")]
    public string FleedId { get; set; } = string.Empty;

    [JsonPropertyName("customerName")]
    public string CustomerName { get; set; } = string.Empty;
    
    [JsonPropertyName("assignedDateTime")]
    public DateTime AssignedDateTime { get; set; }

    [JsonPropertyName("expiryDate")]
    public DateTime ExpiryDate { get; set; }
    
    [JsonPropertyName("caseApplicationStatus")]
    public string CaseApplicationStatus { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

}
