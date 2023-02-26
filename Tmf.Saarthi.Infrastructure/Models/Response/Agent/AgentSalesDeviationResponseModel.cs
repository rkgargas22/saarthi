using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Agent;

public class AgentSalesDeviationResponseModel
{
    [JsonPropertyName("FleetId")]
    public long FleetId { get; set; }

    [JsonPropertyName("processingFee")]
    public Decimal? ProcessingFee { get; set; }

    [JsonPropertyName("stampDuty")]
    public Decimal? StampDuty { get; set; }
    
    [JsonPropertyName("iRR")]
    public Decimal? IRR { get; set; }

    [JsonPropertyName("aIR")]
    public Decimal? AIR { get; set; }

    [JsonPropertyName("requestedAIR")]
    public Decimal? RequestedAIR { get; set; }

    [JsonPropertyName("requestedIRR")]
    public Decimal? RequestedIRR { get; set; }

    [JsonPropertyName("requestedProcessingFees")]
    public Decimal? RequestedProcessingFees { get; set; }
}
public class AgentSalesDeviationUpdateResponseModel
{
    [JsonPropertyName("FleetId")]
    public long FleetId { get; set; }
}
