using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Admin
{
    public class AdminDashbaordResponse
    {
        [JsonPropertyName("ApplicationId")]
        public long ApplicationId { get; set; }

        [JsonPropertyName("CustomerName")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("AssignDateTime")]
        public string AssignDateTime { get; set; } = string.Empty;

        [JsonPropertyName("ExprDate")]
        public string ExprDate { get; set; } = string.Empty;

        [JsonPropertyName("Status")]
        public string Status { get; set; } = string.Empty;

    }
}
