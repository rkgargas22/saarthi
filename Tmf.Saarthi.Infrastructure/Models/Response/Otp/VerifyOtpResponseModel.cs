using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Otp;

public  class VerifyOtpResponseModel
{
    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; }

    [JsonPropertyName("data")]
    public string Data { get; set; } = string.Empty;
}
