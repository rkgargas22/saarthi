using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Nach;

public class NachResponse
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

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
}
public class UpdateNachResponse
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }
}
public class NachResponseByFleetId
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
}

public class DropResponse
{
    [JsonPropertyName("Id")]
    public long Id { get; set; }

    [JsonPropertyName("DisplayName")]
    public string DisplayName { get; set; } = string.Empty;

}

public class NachResponseIFSC
{

    [JsonPropertyName("IFSCCode")]
    public string IFSCCode { get; set; } = string.Empty;

}