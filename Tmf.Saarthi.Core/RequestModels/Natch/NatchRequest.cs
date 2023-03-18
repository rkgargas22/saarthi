using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Natch;

public class NatchRequest
{
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

    [JsonPropertyName("CreatedBy")]
    public string CreatedBy { get; set; } = string.Empty;

    [JsonPropertyName("isNach")]
    public bool IsNach { get; set; }

}
