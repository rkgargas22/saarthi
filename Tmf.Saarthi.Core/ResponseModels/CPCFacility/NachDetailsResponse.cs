using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Core.ResponseModels.CPCFacility
{
    public class NachDetailsResponse
    {
        [JsonPropertyName("NachId")]
        public int NachId { get; set; }

        [JsonPropertyName("FleetId")]
        public long FleetId { get; set; }

        [JsonPropertyName("IsEnach")]
        public bool IsEnach { get; set; }

        [JsonPropertyName("UMRN")]
        public string UMRN { get; set; } = string.Empty;

        [JsonPropertyName("EMandateId")]
        public string EMandateId { get; set; } = string.Empty;

        [JsonPropertyName("EDate")]
        public string EDate { get; set; } = string.Empty;

        [JsonPropertyName("Amount")]
        public decimal Amount { get; set; } = 0;

        [JsonPropertyName("Frequency")]
        public string Frequency { get; set; } = string.Empty;

        [JsonPropertyName("BankName")]
        public string BankName { get; set; } = string.Empty;

        [JsonPropertyName("AccountType")]
        public string AccountType { get; set; } = string.Empty;

        [JsonPropertyName("AccountNumber")]
        public string AccountNumber { get; set; } = string.Empty;

        [JsonPropertyName("IFSCCode")]
        public string IFSCCode { get; set; } = string.Empty;
    }
}
