using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Core.RequestModels.DMS
{
    internal class GenerateTokenRequest
    {
        [JsonPropertyName("domain_id")]
        public string DomainId { get; set; } = string.Empty;
    }
}
