using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Admin
{
    public class AdminFleetDeviationUpdateResponseModel
    {
        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }

}
