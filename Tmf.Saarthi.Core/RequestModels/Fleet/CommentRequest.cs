using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class CommentRequest
{
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
}
