using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Comments
{
    public class SaveCommentsRequestModel
    {

        [JsonPropertyName("FleetId")]
        public long FleetId { get; set; } = 0;

        [JsonPropertyName("StageId")]
        public int StageId { get; set; } = 0;

        [JsonPropertyName("CommentDescription")]
        public string CommentDescription { get; set; } = string.Empty;

    }
}
