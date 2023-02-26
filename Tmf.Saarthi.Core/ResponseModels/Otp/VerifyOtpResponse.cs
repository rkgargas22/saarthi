using System.Text.Json.Serialization;
using Tmf.Saarthi.Core.ResponseModels.Customer;

namespace Tmf.Saarthi.Core.ResponseModels.Otp;

public class VerifyOtpResponse
{

    [JsonPropertyName("customer")]
    public CustomerResponse customerResponse { get; set; } = new CustomerResponse();
}
