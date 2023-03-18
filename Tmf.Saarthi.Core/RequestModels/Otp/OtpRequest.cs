using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Otp;

public class OtpRequest
{
    [JsonPropertyName("mobileNo")]
    public string MobileNo { get; set; } = string.Empty;
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    [JsonPropertyName("ipAddress")]
    public string IpAddress { get; set; } = string.Empty;
    [JsonPropertyName("loginDeviceType")]
    public string LoginDeviceType { get; set; } = string.Empty;
    [JsonPropertyName("latitude")]
    public string Latitude { get; set; } = string.Empty;
    [JsonPropertyName("longitude")]
    public string Longitude { get; set; } = string.Empty;
}
