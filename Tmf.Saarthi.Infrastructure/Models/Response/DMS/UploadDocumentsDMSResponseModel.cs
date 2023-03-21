

using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.DMS
{
    public class UploadDocumentsDMSResponseModel
    {
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("objectId")]
        public string ObjectId { get; set; } = string.Empty;

        [JsonPropertyName("fanNo")]
        public string FanNo { get; set; } = string.Empty;

        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; } = string.Empty;
    }
}
