using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Otp;

public class OtpLogResponse
{
    [JsonPropertyName("logId")]
    public long LogId { get; set; }
}
