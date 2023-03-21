using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Request.DMS
{
    public class DMSDocumentAndFleetRequestModel
    {
        [JsonPropertyName("fleetId")]
        public long FleetId { get; set; }

        [JsonPropertyName("isRCUUpload")]
        public Boolean IsRCUUpload { get; set; }
    }
}
