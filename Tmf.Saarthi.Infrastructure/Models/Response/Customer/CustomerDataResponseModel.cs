using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Customer
{
    public class CustomerDataResponseModel
    {
        [JsonPropertyName("BPNumber")]
        public long BPNumber { get; set; }

        [JsonPropertyName("FleetID")]
        public long FleetID { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("MiddleName")]
        public string MiddleName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("Dob")]
        public string Dob { get; set; } = string.Empty;

        [JsonPropertyName("Gender")]
        public string Gender { get; set; } = string.Empty;

        [JsonPropertyName("PanNo")]
        public string PanNo { get; set; }

        [JsonPropertyName("FanNo")]
        public string FanNo { get; set; }

        [JsonPropertyName("MobileNo")]
        public string MobileNo { get; set; }
    }
}
