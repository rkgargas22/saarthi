using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Ocr; 

public class TaskDetailRequestModel 
{
    [JsonPropertyName("requestId")]
    public string RequestId { get; set; }
    [JsonPropertyName("requestType")]
    public string RequestType { get; set; }
}
