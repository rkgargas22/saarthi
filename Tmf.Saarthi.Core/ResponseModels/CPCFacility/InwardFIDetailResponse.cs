using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Core.ResponseModels.CPCFacility
{
    public class InwardFIDetailResponse
    {
        [JsonPropertyName("FiID")]
        public long FiID { get; set; }

        [JsonPropertyName("FleetId")]
        public long FleetId { get; set; } = 0;

        [JsonPropertyName("FIStatus")]
        public string FIStatus { get; set; } = string.Empty;

        [JsonPropertyName("VerificationDate")]
        public string VerificationDate { get; set; } = string.Empty;
    }
}
