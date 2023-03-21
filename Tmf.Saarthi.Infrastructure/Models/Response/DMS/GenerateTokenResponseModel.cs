using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Response.DMS
{
    public class GenerateTokenResponseModel
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }
}
