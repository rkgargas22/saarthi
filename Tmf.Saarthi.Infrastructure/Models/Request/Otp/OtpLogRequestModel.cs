using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Otp;

public class OtpLogRequestModel
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
    [JsonPropertyName("createdBy")]
    public long CreatedBy { get; set; }
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
    [JsonPropertyName("userType")]
    public string UserType { get; set; } = string.Empty;
    [JsonPropertyName("logType")]
    public string LogType { get; set; } = string.Empty;
}
