using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.CustomerConsent;

public class CustomerConsentRequestModel
{
    [JsonPropertyName("borrower")]
    public string Borrower { get; set; } = string.Empty;
    [JsonPropertyName("coBorrower")]
    public string CoBorrower { get; set; } = string.Empty;
}

