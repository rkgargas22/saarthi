using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Otp;

public class OtpLogRequest
{
    [JsonPropertyName("bpNumber")]
    public long BPNumber { get; set; }
    [JsonPropertyName("loginTime")]
    public DateTime? LoginTime { get; set; }
    [JsonPropertyName("logoutTime")]
    public DateTime? LogoutTime { get; set; }
    [JsonPropertyName("mobileNo")]
    public string MobileNo { get; set; } = string.Empty;
    [JsonPropertyName("ipAddress")]
    public string IpAddress { get; set; } = string.Empty;
    [JsonPropertyName("loginDeviceType")]
    public string LoginDeviceType { get; set; } = string.Empty;
    [JsonPropertyName("latitude")]
    public string Latitude { get; set; } = string.Empty;
    [JsonPropertyName("longitude")]
    public string Longitude { get; set; } = string.Empty;
    [JsonPropertyName("otpRequestID")]
    public int? OtpRequestID { get; set; }
    [JsonPropertyName("otp")]
    public int? Otp { get; set; }
    [JsonPropertyName("logType")]
    public string LogType { get; set; } = string.Empty;
}
