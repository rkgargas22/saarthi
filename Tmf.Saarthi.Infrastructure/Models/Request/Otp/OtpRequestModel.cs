using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Otp;

public class OtpRequestModel
{
    [JsonPropertyName("mobile_number")]
    public string MobileNo { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("module")]
    public string Module { get; set; } = string.Empty;
}
