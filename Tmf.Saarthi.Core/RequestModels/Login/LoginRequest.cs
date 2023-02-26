using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Login;

public class LoginRequest
{
    [JsonPropertyName("userName")]
    public string Username { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
