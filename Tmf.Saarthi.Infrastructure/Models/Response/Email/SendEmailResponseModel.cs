using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Email; 

public class SendEmailResponseModel 
{
    [JsonPropertyName("message")] 
    public string Message { get; set; } = string.Empty;
}
