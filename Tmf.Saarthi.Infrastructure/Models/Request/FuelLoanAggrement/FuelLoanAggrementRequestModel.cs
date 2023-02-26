using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FuelLoanAggrement;

public class FuelLoanAggrementRequestModel
{
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
    [JsonPropertyName("borrowerAddress")]
    public string BorrowerAddress { get; set; } = string.Empty;
    [JsonPropertyName("borrowerMobileNumber")]
    public string BorrowerMobileNumber { get; set; } = string.Empty;
    [JsonPropertyName("borrowerEmailID")]
    public string BorrowerEmailID { get; set; } = string.Empty;
    [JsonPropertyName("coBorrowerName")]
    public string CoBorrowerName { get; set; } = string.Empty;
    [JsonPropertyName("coBorrowerConstitution")]
    public string CoBorrowerConstitution { get; set; } = string.Empty;
    [JsonPropertyName("coBorrowerAddress")]
    public string CoBorrowerAddress { get; set; } = string.Empty;
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
    [JsonPropertyName("processingFees")]
    public decimal? ProcessingFees { get; set; }
    [JsonPropertyName("stampDuty")]
    public decimal? StampDuty { get; set; }
    [JsonPropertyName("cli")]
    public string CLI { get; set; } = string.Empty;
    [JsonPropertyName("aETNA")]
    public string AETNA { get; set; } = string.Empty;
    [JsonPropertyName("otherCharges")]
    public string OtherCharges { get; set; } = string.Empty;
}
