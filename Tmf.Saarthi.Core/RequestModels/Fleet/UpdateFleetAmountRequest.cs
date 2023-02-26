using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class UpdateFleetAmountRequest
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    [JsonPropertyName("loanAmount")]
    public decimal? LoanAmount { get; set; }
    [JsonPropertyName("processingFeeAmount")]
    public decimal? ProcessingFeeAmount { get; set; }
    [JsonPropertyName("stampDutyAmount")]
    public decimal? StampDutyAmount { get; set; }
}
