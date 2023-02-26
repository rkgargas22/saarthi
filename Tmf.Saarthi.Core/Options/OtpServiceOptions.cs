namespace Tmf.Saarthi.Core.Options;

public class OtpServiceOptions
{
    public const string OtpService = "OtpService";
    public string BaseUrl { get; set; } = string.Empty;
    public string SendOtpEndpoint { get; set; } = string.Empty;
    public string VerifyOtpEndpoint { get; set; } = string.Empty;
}
