using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Login;

public class LoginResponseModel
{
    [JsonPropertyName("status_code")]
    public string StatusCode { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("mobile_no")]
    public string MobileNo { get; set; } = string.Empty;

    [JsonPropertyName("user_name")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("bp_no")]
    public string BpNo { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}
