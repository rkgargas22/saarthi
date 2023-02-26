using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Login;

public class LoginRequestModel
{
    [JsonPropertyName("userName")]
    public string Username { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
