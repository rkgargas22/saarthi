using System.Text.Json.Serialization;
using Tmf.Saarthi.Core.ResponseModels.Customer;

namespace Tmf.Saarthi.Core.ResponseModels.Login;

public class LoginResponse
{
    //[JsonPropertyName("mobileNo")]
    //public string MobileNo { get; set; } = string.Empty;

    //[JsonPropertyName("userId")]
    //public string UserId { get; set; } = string.Empty;

    //[JsonPropertyName("bpNo")]
    //public string BpNo { get; set; } = string.Empty;

    //[JsonPropertyName("email")]
    //public string Email { get; set; } = string.Empty;
    [JsonPropertyName("customer")]
    public CustomerResponse customerResponse { get; set; } = new CustomerResponse();
}
