using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class CommentResponse
{
    [JsonPropertyName("commentId")]
    public long CommentId { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
