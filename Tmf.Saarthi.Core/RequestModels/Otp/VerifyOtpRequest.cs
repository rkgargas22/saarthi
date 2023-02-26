using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Otp;

public class VerifyOtpRequest
{
    [JsonPropertyName("mobileNo")]
    public string? MobileNo { get; set; }
    [JsonPropertyName("requestId")]
    public string RequestId { get; set; } = string.Empty;
    [JsonPropertyName("otp")]
    public string Otp { get; set; } = string.Empty;
}
