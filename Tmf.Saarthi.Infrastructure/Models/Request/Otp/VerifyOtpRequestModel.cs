using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Otp;

public class VerifyOtpRequestModel
{
    [JsonPropertyName("id")]
    public string Id { get; set;} = string.Empty;
    [JsonPropertyName("otp")]
    public string Otp { get; set; } = string.Empty;
}
