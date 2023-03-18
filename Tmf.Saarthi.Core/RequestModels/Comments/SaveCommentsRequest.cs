using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Core.RequestModels.Comments
{
    public class SaveCommentsRequest
    {

        [JsonPropertyName("FleetId")]
        public long FleetId { get; set; } = 0;

        [JsonPropertyName("StageId")]
        public int StageId { get; set; } = 0;

        [JsonPropertyName("CommentDescription")]
        public string CommentDescription { get; set; } = string.Empty;
    }
}
