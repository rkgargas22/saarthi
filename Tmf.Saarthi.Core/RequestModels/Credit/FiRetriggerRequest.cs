using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Core.RequestModels.Credit
{
    public class FiRetriggerRequest
    {
        [JsonPropertyName("fleetId")]
        public long fleetId { get; set; }

        [JsonPropertyName("UserId")]
        public long UserId { get; set; }
    }
}
