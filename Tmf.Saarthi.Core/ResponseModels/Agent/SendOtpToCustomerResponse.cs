using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Agent;

public class SendOtpToCustomerResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
