using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Agent;

public class AgentSalesDeviationResponse
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
public class AgentSalesDeviationUpdateResponse
{
    [JsonPropertyName("FleetId")]
    public long FleetId { get; set; }
}
