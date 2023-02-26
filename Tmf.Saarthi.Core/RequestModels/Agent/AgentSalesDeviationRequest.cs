using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Agent;

public class AgentSalesDeviationRequest
{
    [JsonPropertyName("isRequestedProcessingFees")]
    public bool IsRequestedProcessingFees { get; set; }

    [JsonPropertyName("requestedProcessingFees")]
    public decimal? RequestedProcessingFees { get; set; }

    [JsonPropertyName("isRequestedIRR")]
    public bool IsRequestedIRR { get; set; }

    [JsonPropertyName("requestedIRR")]
    public decimal? RequestedIRR { get; set; }

    [JsonPropertyName("isRequestedAIR")]
    public bool IsRequestedAIR { get; set; }

    [JsonPropertyName("requestedAIR")]
    public decimal? RequestedAIR { get; set; }
}
