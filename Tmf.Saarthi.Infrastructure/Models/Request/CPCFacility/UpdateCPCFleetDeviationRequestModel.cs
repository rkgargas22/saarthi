using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Request.CPCFacility
{
    public class UpdateCPCFleetDeviationRequestModel
    {
        [JsonPropertyName("FleetId")]
        public long FleetId { get; set; }

        [JsonPropertyName("DeviationId")]
        public long[] DeviationId { get; set; }

        [JsonPropertyName("CPCCheckStatus")]
        public bool CPCCheckStatus { get; set; }

        [JsonPropertyName("Comment")]
        public string Comment { get; set; }
    }
}
