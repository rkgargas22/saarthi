using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Comments
{
    public class CommentsResponse
    {
        [JsonPropertyName("CommentId")]
        public long CommentId { get; set; }

        [JsonPropertyName("FleetId")]
        public long FleetId { get; set; } = 0;

        [JsonPropertyName("StageId")]
        public int StageId { get; set; } = 0;

        [JsonPropertyName("CommentDescription")]
        public string CommentDescription { get; set; } = string.Empty;

        [JsonPropertyName("AssignedTo")]
        public long AssignedTo { get; set; } = 0;

        [JsonPropertyName("CreatedDate")]
        public DateTime? CreatedDate { get; set; }
    }
}
