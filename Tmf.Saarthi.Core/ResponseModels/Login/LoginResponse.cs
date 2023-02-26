using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Login;

public class LoginResponse
{
    [JsonPropertyName("mobileNo")]
    public string MobileNo { get; set; } = string.Empty;

    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("bpNo")]
    public string BpNo { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}
