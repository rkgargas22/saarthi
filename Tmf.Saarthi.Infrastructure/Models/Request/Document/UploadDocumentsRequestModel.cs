using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Document
{
    public class UploadDocumentsRequestModel
    {

        [JsonPropertyName("fleetId")]
        public long FleetId { get; set; }

        [JsonPropertyName("docTypeId")]
        public int DocTypeId { get; set; }

        [JsonPropertyName("stageId")]
        public int StageId { get; set; }

        [JsonPropertyName("extension")]
        public string Extension { get; set; } = string.Empty;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("documentName")]
        public string DocumentName { get; set; } = string.Empty;                        

        [JsonPropertyName("createdBy")]
        public long CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("createdUserType")]
        public string CreatedUserType { get; set; } = string.Empty;

        [JsonPropertyName("entityCode")]
        public string EntityCode { get; set; } = string.Empty;

        [JsonPropertyName("entityId")]
        public long EntityId { get; set; }

    }
}
