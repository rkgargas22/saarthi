using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.FleetVehicle;

public class BlackListedDetailsRequest
{
    [JsonPropertyName("instaLogId")]
    public long InstaLogId { get; set; }
    [JsonPropertyName("registration_state")]
    public string RegistrationState { get; set; } = string.Empty;
    [JsonPropertyName("registering_authority")]
    public string RegisteringAuthority { get; set; } = string.Empty;
    [JsonPropertyName("rc_number")]
    public string RcNumber { get; set; } = string.Empty;
    [JsonPropertyName("fir_number")]
    public string FirNumber { get; set; } = string.Empty;
    [JsonPropertyName("fir_date")]
    public DateTime? FirDate { get; set; }
    [JsonPropertyName("blacklisted_reason")]
    public string BlacklistedReason { get; set; } = string.Empty;
    [JsonPropertyName("blacklisted_date")]
    public DateTime? BlacklistedDate { get; set; }
}
