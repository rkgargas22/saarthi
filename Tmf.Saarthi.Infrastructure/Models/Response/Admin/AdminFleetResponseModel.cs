using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Admin
{
    public class AdminFleetResponseModel
    {
        [JsonPropertyName("VehicleId")]
        public long VehicleId { get; set; }
        [JsonPropertyName("FleetID")]
        public long FleetID { get; set; }

        [JsonPropertyName("REGISTRATIONNO")]
        public string RegistrationNo { get; set; }

        [JsonPropertyName("OWNERNAME")]
        public string OwnerName { get; set; } = string.Empty;

        [JsonPropertyName("YEAR")]
        public int? Year { get; set; }

        [JsonPropertyName("CATEGORY")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("MODELTYPE")]
        public string ModelType { get; set; } = string.Empty;

        [JsonPropertyName("MANUFACTURER")]
        public string Manufacturer { get; set; } = string.Empty;

        [JsonPropertyName("STATUS")]
        public bool Status { get; set; }
    }

}
