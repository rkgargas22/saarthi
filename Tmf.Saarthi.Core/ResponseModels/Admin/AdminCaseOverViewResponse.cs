using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Admin
{
    public class AdminCaseOverViewResponse
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
