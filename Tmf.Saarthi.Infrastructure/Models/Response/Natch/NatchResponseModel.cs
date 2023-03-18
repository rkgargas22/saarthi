using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Natch;

public class NatchResponseModel
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; } 
}

public class NachResponseModelByFleetId
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("AccountNumber")]
    public string AccountNumber { get; set; } = string.Empty;

    [JsonPropertyName("ConfirmAccountNumber")]
    public string ConfirmAccountNumber { get; set; } = string.Empty;

    [JsonPropertyName("AccountType")]
    public string AccountType { get; set; } = string.Empty;

    [JsonPropertyName("IFSCCode")]
    public string IFSCCode { get; set; } = string.Empty;

    [JsonPropertyName("BankName")]
    public string BankName { get; set; } = string.Empty;

    [JsonPropertyName("AuthenticationMode")]
    public string AuthenticationMode { get; set; } = string.Empty;

    [JsonPropertyName("IsActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("CreatedBy")]
    public string CreatedBy { get; set; } = string.Empty;

    [JsonPropertyName("Amount")]
    public Decimal? Amount { get; set; } 

    [JsonPropertyName("StartDate")]
    public DateTime? StartDate { get; set; }

    [JsonPropertyName("EndDate")]
    public DateTime? EndDate { get; set; }

    [JsonPropertyName("Frequency")]
    public string Frequency { get; set; } = string.Empty;

    [JsonPropertyName("PurposeOfManadate")]
    public string PurposeOfManadate { get; set; } = string.Empty;

    [JsonPropertyName("isEnach")]
    public bool IsEnach { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("timeSlotDate")]
    public DateTime? TimeSlotDate { get; set; }

    [JsonPropertyName("umrn")]
    public string UMRN { get; set; } = string.Empty;

    [JsonPropertyName("emandateId")]
    public string EmandateId { get; set; } = string.Empty;

    [JsonPropertyName("emandateDate")]
    public DateTime? EmandateDate { get; set; }

    [JsonPropertyName("maxAmount")]
    public decimal? MaxAmount { get; set; }

    [JsonPropertyName("corporateName")]
    public string CorporateName { get; set; } = string.Empty;

    [JsonPropertyName("utilityNo")]
    public string UtilityNo { get; set; } = string.Empty;
}

public class DropdownResponseModel
{
    [JsonPropertyName("Id")]
    public long Id { get; set; }

    [JsonPropertyName("DisplayName")]
    public string DisplayName { get; set; } = string.Empty;

}
public class NachResponseModelIFSC
{

    [JsonPropertyName("IFSCCode")]
    public string IFSCCode { get; set; } = string.Empty;

}