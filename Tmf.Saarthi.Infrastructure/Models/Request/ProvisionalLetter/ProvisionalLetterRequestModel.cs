using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.ProvisionalLetter;

public class ProvisionalLetterRequestModel
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = String.Empty;

    [JsonPropertyName("applicationNumber")]
    public long ApplicationNumber { get; set; }

    [JsonPropertyName("loanAmount")]
    public Decimal? LoanAmount { get; set; }

    [JsonPropertyName("loanTenure")]
    public Decimal LoanTenure { get; set; }

    [JsonPropertyName("rateOfInterest")]
    public Decimal? RateOfInterest { get; set; }

    [JsonPropertyName("processingFee")]
    public Decimal? ProcessingFee { get; set; }
}
