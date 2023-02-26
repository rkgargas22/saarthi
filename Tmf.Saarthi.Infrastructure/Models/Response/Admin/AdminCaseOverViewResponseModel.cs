using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Admin
{
    public class AdminCaseOverViewResponseModel
    {
        [JsonPropertyName("isAgreementLetterApproved")]
        public bool? IsAgreementLetterApproved { get; set; }

        [JsonPropertyName("isProvisionLetterApproved")]
        public bool? IsProvisionLetterApproved { get; set; }

        [JsonPropertyName("isSanctionLetterApproved")]
        public bool? IsSanctionLetterApproved { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;

        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
