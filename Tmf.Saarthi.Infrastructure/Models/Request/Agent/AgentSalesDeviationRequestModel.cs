using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Agent;

public class AgentSalesDeviationRequestModel
{
    [JsonPropertyName("requestedProcessingFees")]
    public decimal? RequestedProcessingFees { get; set; }   
    
    [JsonPropertyName("requestedIRR")]
    public decimal? RequestedIRR { get; set; } 
    
    [JsonPropertyName("requestedAIR")]
    public decimal? RequestedAIR { get; set; } 
    
    [JsonPropertyName("fleetId")]
    public long FleetId { get; set; } 
}
