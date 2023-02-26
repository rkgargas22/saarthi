using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.FuelLoanAgreement;

public class FuelLoanAgreementResponse
{
    [JsonPropertyName("letter")]
    public string Letter { get; set; } = string.Empty;
}
