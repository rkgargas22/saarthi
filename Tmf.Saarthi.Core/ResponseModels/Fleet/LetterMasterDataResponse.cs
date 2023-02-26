using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class LetterMasterDataResponse
{
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;

    [JsonPropertyName("borrowerAuthorisedPersonName")]
    public string BorrowerAuthorisedPersonName { get; set; } = string.Empty;

    [JsonPropertyName("coBorrowerAuthorisedPersonName")]
    public string CoBorrowerAuthorisedPersonName { get; set; } = string.Empty;

    [JsonPropertyName("agreementDate")]
    public DateTime? AgreementDate { get; set; }

    [JsonPropertyName("agreementPlace")]
    public string AgreementPlace { get; set; } = string.Empty;

    [JsonPropertyName("fileAccountNumber")]
    public int FileAccountNumber { get; set; }

    [JsonPropertyName("officeOrbranchAddress")]
    public string OfficeOrbranchAddress { get; set; } = string.Empty;

    [JsonPropertyName("borrowerName")]
    public string BorrowerName { get; set; } = string.Empty;

    [JsonPropertyName("borrowerConstitution")]
    public string BorrowerConstitution { get; set; } = string.Empty;

    [JsonPropertyName("borrowerAddressLine1")]
    public string BorrowerAddressLine1 { get; set; } = string.Empty;

    [JsonPropertyName("borrowerAddressLine2")]
    public string BorrowerAddressLine2 { get; set; } = string.Empty;

    [JsonPropertyName("borrowerAddressLine3")]
    public string BorrowerAddressLine3 { get; set; } = string.Empty;

    [JsonPropertyName("borrowerMobileNumber")]
    public string BorrowerMobileNumber { get; set; } = string.Empty;

    [JsonPropertyName("borrowerEmailID")]
    public string BorrowerEmailID { get; set; } = string.Empty;

    [JsonPropertyName("coBorrowerName")]
    public string CoBorrowerName { get; set; } = string.Empty;

    [JsonPropertyName("coBorrowerConstitution")]
    public string CoBorrowerConstitution { get; set; } = string.Empty;

    [JsonPropertyName("coBorrowerAddressLine1")]
    public string CoBorrowerAddressLine1 { get; set; } = string.Empty;

    [JsonPropertyName("coBorrowerAddressLine2")]
    public string CoBorrowerAddressLine2 { get; set; } = string.Empty;

    [JsonPropertyName("coBorrowerAddressLine3")]
    public string CoBorrowerAddressLine3 { get; set; } = string.Empty;

    [JsonPropertyName("coBorrowerMobileNumber")]
    public string CoBorrowerMobileNumber { get; set; } = string.Empty;

    [JsonPropertyName("coBorrowerEmailID")]
    public string CoBorrowerEmailID { get; set; } = string.Empty;

    [JsonPropertyName("totalAmountofLoan")]
    public decimal? TotalAmountofLoan { get; set; }

    [JsonPropertyName("limit")]
    public decimal? Limit { get; set; }

    [JsonPropertyName("cutOffLimit")]
    public decimal? CutOffLimit { get; set; }

    [JsonPropertyName("interestRate")]
    public decimal? InterestRate { get; set; }

    [JsonPropertyName("typeofInterest")]
    public string TypeofInterest { get; set; } = string.Empty;

    [JsonPropertyName("acceleratedInterest")]
    public decimal? AcceleratedInterest { get; set; }

    [JsonPropertyName("purposeoftheLoan")]
    public string PurposeoftheLoan { get; set; } = string.Empty;

    [JsonPropertyName("availabilityPeriod")]
    public string AvailabilityPeriod { get; set; } = string.Empty;

    [JsonPropertyName("oilCompanyName")]
    public string OilCompanyName { get; set; } = string.Empty;

    [JsonPropertyName("fuelProgrammeName")]
    public string FuelProgrammeName { get; set; } = string.Empty;

    [JsonPropertyName("oilCompanyDesignatedAccount")]
    public string OilCompanyDesignatedAccount { get; set; } = string.Empty;

    [JsonPropertyName("legalExpenses")]
    public string LegalExpenses { get; set; } = string.Empty;

    [JsonPropertyName("serviceCharges")]
    public string ServiceCharges { get; set; } = string.Empty;

    [JsonPropertyName("chequeBouncingCharges")]
    public decimal? ChequeBouncingCharges { get; set; }

    [JsonPropertyName("retainerCharges")]
    public string RetainerCharges { get; set; } = string.Empty;

    [JsonPropertyName("processingFees")]
    public decimal? ProcessingFees { get; set; }

    [JsonPropertyName("stampDuty")]
    public decimal? StampDuty { get; set; }

    [JsonPropertyName("cli")]
    public string Cli { get; set; } = string.Empty;

    [JsonPropertyName("aetna")]
    public string Aetna { get; set; } = string.Empty;

    [JsonPropertyName("otherCharges")]
    public string OtherCharges { get; set; } = string.Empty;
}