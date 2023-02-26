using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Core.RequestModels.Admin
{
    public class ApproveAdminFleetDeviationRequest
    {

        [JsonPropertyName("VehicleId")]
        public long[] VehicleId { get; set; }

        [JsonPropertyName("Comment")]
        public string Comment { get; set; }

    }
}
