using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.Exceptions;

public class ErrorResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("error")]
    public dynamic Error { get; set; } = string.Empty;
}
