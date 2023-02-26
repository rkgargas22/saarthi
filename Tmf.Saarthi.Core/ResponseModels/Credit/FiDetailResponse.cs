using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Credit
{
    public class FiDetailResponse
    {
        [JsonPropertyName("FleetID")]
        public long FleetID { get; set; }

        [JsonPropertyName("VerificationDate")]
        public string VerificationDate { get; set; }

        [JsonPropertyName("FiStatus")]
        public string FiStatus { get; set; } = string.Empty;

        [JsonPropertyName("CPCStatus")]
        public string CPCStatus { get; set; } = string.Empty;

        [JsonPropertyName("FiDeviation")]
        public string FiDeviation { get; set; } = string.Empty;

    }
}
