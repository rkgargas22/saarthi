using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Otp;

public class OtpLogResponseModel
{
    [JsonPropertyName("logId")]
    public long LogId { get; set; }
}
