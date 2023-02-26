using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Ocr; 

public class TaskDetailResponseModel 
{
    [JsonPropertyName("request_id")]
    public string RequestId { get; set; } = string.Empty;
}
