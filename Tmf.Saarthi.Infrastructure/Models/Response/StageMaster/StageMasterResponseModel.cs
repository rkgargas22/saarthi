using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.StageMaster
{
    public class StageMasterResponseModel
    {
        [JsonPropertyName("stageId")]
        public int StageId { get; set; }

        [JsonPropertyName("stageName")]
        public string StageName { get; set; } = null!;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } 

        [JsonPropertyName("groupStageid")]
        public int GroupStageid { get; set; }

        [JsonPropertyName("stageCode")]
        public string StageCode { get; set; } = string.Empty;
    }
}
