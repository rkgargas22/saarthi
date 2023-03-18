using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Comments
{
    public class CommentsResponseModel
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
