using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Request.DMS
{
    public class GenerateTokenRequestModel
    {
        [JsonPropertyName("domain_id")]
        public string DomainId { get; set; } = string.Empty;
    }
}
