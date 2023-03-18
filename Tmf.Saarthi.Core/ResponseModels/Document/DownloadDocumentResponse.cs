using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Document
{
    public class DownloadDocumentResponse
    {
        [JsonPropertyName("DocumentID")]
        public long DocumentID { get; set; }

        [JsonPropertyName("FleetId")]
        public long FleetId { get; set; } = 0;

        [JsonPropertyName("StageId")]
        public int StageId { get; set; } = 0;

        [JsonPropertyName("DocumentUrl")]
        public string DocumentUrl { get; set; } = string.Empty;

        [JsonPropertyName("Documenttype")]
        public string Documenttype { get; set; } = string.Empty;

        
    }
}
