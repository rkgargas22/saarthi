using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Agent;

public class AgentCaseOverViewResponse
{
    [JsonPropertyName("id")]
    public int ID { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("moduleName")]
    public string ModuleName { get; set; } = string.Empty;
    [JsonPropertyName("logDateTime")]
    public DateTime? LogDateTime { get; set; }
    [JsonPropertyName("comments")]
    public List<AgentOverViewCommentResponse> Comments { get; set; }
}

public class AgentOverViewCommentResponse
{
    [JsonPropertyName("commentId")]
    public int CommentId { get; set; }
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
}