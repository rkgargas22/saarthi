using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Otp;

public class OtpRequest
{
    [JsonPropertyName("mobileNo")]
    public string? MobileNo { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
