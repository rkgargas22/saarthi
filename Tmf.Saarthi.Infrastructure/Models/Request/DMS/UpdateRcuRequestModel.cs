using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Request.DMS
{
    public class UpdateRcuRequestModel
    {        

        [JsonPropertyName("fleetId")]
        public long FleetId { get; set; }

        [JsonPropertyName("isProcessed")]
        public bool IsProcessed { get; set; }

        [JsonPropertyName("isRCUUpload")]
        public bool IsRCUUpload { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("processedDate")]
        public DateTime? ProcessedDate { get; set; }

        [JsonPropertyName("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
