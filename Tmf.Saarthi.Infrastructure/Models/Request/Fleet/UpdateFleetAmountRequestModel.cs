using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Fleet;

public class UpdateFleetAmountRequestModel
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
    [JsonPropertyName("updatedBy")]
    public long UpdatedBy { get; set; }
    [JsonPropertyName("updatedDate")]
    public DateTime? UpdatedDate { get; set; }
}
