using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Admin
{
    public class AdminFleetDeviationRequest
    {
        [JsonPropertyName("IsIRR")]
        public bool IsIRR { get; set; }
        [JsonPropertyName("NewIRR")]
        public decimal NewIRR { get; set; }
        [JsonPropertyName("IsAIR")]
        public bool IsAIR { get; set; }

        [JsonPropertyName("NewAIR")]
        public decimal NewAIR { get; set; }
        [JsonPropertyName("IsProcessing")]
        public bool IsProcessing { get; set; }

        [JsonPropertyName("NewProcessing")]
        public decimal NewProcessing { get; set; }
    }
}
