using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Credit
{
    public class CreditDashboardResponse
    {
        [JsonPropertyName("ApplicationId")]
        public long ApplicationId { get; set; }

        [JsonPropertyName("CustomerName")]
        public string CustomerName { get; set; }

        [JsonPropertyName("AssingedDate")]
        public string AssingedDate { get; set; } = string.Empty;

        [JsonPropertyName("ExprDate")]
        public string ExprDate { get; set; } = string.Empty;

        [JsonPropertyName("Status")]
        public string Status { get; set; } = string.Empty;


    }
}
