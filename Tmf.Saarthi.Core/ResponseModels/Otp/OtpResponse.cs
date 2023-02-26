using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Otp;

public class OtpResponse
{
    [JsonPropertyName("requestId")]
    public string RequestId { get; set; } = string.Empty;
}
