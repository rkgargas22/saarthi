

using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.DMS
{
    public class UploadDocumentsDMSRequestModel
    {
        [JsonPropertyName("fanno")]
        public string Fanno { get; set; } = string.Empty;

        [JsonPropertyName("appType")]
        public string AppType { get; set; } = string.Empty;

        [JsonPropertyName("binary")]
        public string Binary { get; set; } = string.Empty;

        [JsonPropertyName("documentName")]
        public string DocumentName { get; set; } = string.Empty;

        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; } = string.Empty;

        [JsonPropertyName("processType")]
        public string ProcessType { get; set; } = string.Empty;

        [JsonPropertyName("applicantName")]
        public string ApplicantName { get; set; } = string.Empty;
    }
}
