using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Agent;

public class AgentCaseOverViewResponseModel
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
    public List<AgentOverViewCommentResponseModel> Comments { get; set; }
}

public class AgentOverViewCommentResponseModel
{
    [JsonPropertyName("commentId")]
    public int CommentId { get; set; }
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
}
