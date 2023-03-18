using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.DocumentTypeMstr
{
    public class DocumentTypeMstrResponseModel
    {
        [JsonPropertyName("docTypeId")]
        public int DocTypeId { get; set; }

        [JsonPropertyName("documentName")]
        public string DocumentName { get; set; } = null!;

        [JsonPropertyName("category")]
        public string Category { get; set; } = null!;

        [JsonPropertyName("rcuApplicable")]
        public bool RcuApplicable { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("documentCode")]
        public string DocumentCode { get; set; } = null!;
    }
}
