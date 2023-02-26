using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.SanctionLetter;

public class SanctionLetterRequestModel
{
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;
    [JsonPropertyName("borrowerName")]
    public string BorrowerName { get; set; } = string.Empty;
    [JsonPropertyName("borrowerAddressLine1")]
    public string BorrowerAddressLine1 { get; set; } = string.Empty;
    [JsonPropertyName("borrowerAddressLine2")]
    public string BorrowerAddressLine2 { get; set; } = string.Empty;
    [JsonPropertyName("borrowerAddressLine3")]
    public string BorrowerAddressLine3 { get; set; } = string.Empty;
    [JsonPropertyName("coBorrowerName")]
    public string CoBorrowerName { get; set; } = string.Empty;
    [JsonPropertyName("coBorrowerAddressLine1")]
    public string CoBorrowerAddressLine1 { get; set; } = string.Empty;
    [JsonPropertyName("coBorrowerAddressLine2")]
    public string CoBorrowerAddressLine2 { get; set; } = string.Empty;
    [JsonPropertyName("coBorrowerAddressLine3")]
    public string CoBorrowerAddressLine3 { get; set; } = string.Empty;
    [JsonPropertyName("sanctionLimit")]
    public decimal? SanctionLimit { get; set; }
    [JsonPropertyName("cutOffLimit")]
    public decimal? CutOffLimit { get; set; }
    [JsonPropertyName("processingFee")]
    public decimal? ProcessingFee { get; set; }
    [JsonPropertyName("stampDuty")]
    public decimal? StampDuty { get; set; }
    [JsonPropertyName("cli")]
    public string CLI { get; set; } = string.Empty;
    [JsonPropertyName("aetna")]
    public string Aetna { get; set; } = string.Empty;
    [JsonPropertyName("legalExpenses")]
    public string LegalExpenses { get; set; } = string.Empty;
    [JsonPropertyName("chequeBouncingCharges")]
    public decimal? ChequeBouncingCharges { get; set; }
    [JsonPropertyName("retainerCharges")]
    public string RetainerCharges { get; set; } = string.Empty;
    [JsonPropertyName("interestRate")]
    public decimal? InterestRate { get; set; }
    [JsonPropertyName("acceleratedInterestrate")]
    public decimal? AcceleratedInterestrate { get; set; }
    [JsonPropertyName("borrowerAuthorisedPersonName")]
    public string BorrowerAuthorisedPersonName { get; set; } = string.Empty;
    [JsonPropertyName("coBorrowerAuthorisedPersonName")]
    public string CoBorrowerAuthorisedPersonName { get; set; } = string.Empty;
}
