﻿using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Admin;

public class AdminFleetDeviationResponse
{
    [JsonPropertyName("FleetId")]
    public long FleetId { get; set; }

    [JsonPropertyName("OgIRR")]
    public decimal OgIRR { get; set; } = 0;

    [JsonPropertyName("OgAIR")]
    public decimal OgAIR { get; set; } = 0;

    [JsonPropertyName("OgProcessingFee")]
    public decimal OgProcessingFee { get; set; } = 0;

    [JsonPropertyName("StampDuty")]
    public decimal StampDuty { get; set; } = 0;

    [JsonPropertyName("RequestedIRR")]
    public decimal RequestedIRR { get; set; } = 0;

    [JsonPropertyName("RequestedARR")]
    public decimal RequestedARR { get; set; } = 0;

    [JsonPropertyName("RequestedProcessingFees")]
    public decimal RequestedProcessingFees { get; set; }
    [JsonPropertyName("NewIRR")]
    public string NewIRR { get; set; }=string.Empty;
    [JsonPropertyName("NewAIR")]
    public string NewAIR { get; set; } = string.Empty;
    [JsonPropertyName("NewProcessing")] 
    public string NewProcessing { get; set; } = string.Empty;

}
