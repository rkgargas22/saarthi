using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Agent;

public class SendOtpToCustomerRequest
{
    [JsonPropertyName("mobileNo")]
    public string MobileNo { get; set; } = string.Empty;
}
