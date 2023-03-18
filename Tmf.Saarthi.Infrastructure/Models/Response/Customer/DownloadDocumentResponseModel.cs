using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Customer
{
    public class DownloadDocumentResponseModel
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
