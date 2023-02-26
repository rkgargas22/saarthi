using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Email;

public class SendEmailRequestModel
{
    [JsonPropertyName("toEmail")]
    public string ToEmail { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;

    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;
}
