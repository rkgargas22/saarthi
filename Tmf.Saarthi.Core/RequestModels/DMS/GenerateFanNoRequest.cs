using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.DMS;

public class GenerateFanNoRequest
{
    [JsonPropertyName("branchCode")]
    public string BranchCode { get; set; } = string.Empty;
    [JsonPropertyName("processType")]
    public string ProcessType { get; set; } = string.Empty;
    [JsonPropertyName("schemeName")]
    public string SchemeName { get; set; } = string.Empty;
    [JsonPropertyName("loanType")]
    public string LoanType { get; set; } = string.Empty;
    [JsonPropertyName("applicantName")]
    public string ApplicantName { get; set; } = string.Empty;
    [JsonPropertyName("bdmName")]
    public string BdmName { get; set; } = string.Empty;
    [JsonPropertyName("dsaName")]
    public string DsaName { get; set; } = string.Empty;
    [JsonPropertyName("dealerName")]
    public string DealerName { get; set; } = string.Empty;
    [JsonPropertyName("dealerCode")]
    public string DealerCode { get; set; } = string.Empty;
}
