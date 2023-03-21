using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Request.DMS
{
    public class UpdateDmsDocumentStatusRequestModel
    {
        [JsonPropertyName("documentID")]
        public long DocumentID { get; set; }

        [JsonPropertyName("dMSUploadStatus")]
        public bool DMSUploadStatus { get; set; }
    
        [JsonPropertyName("dMSComment")]
        public string? DMSComment { get; set; }

        [JsonPropertyName("dMSDateTime")]
        public DateTime? DMSDateTime { get; set; }
    }
}
