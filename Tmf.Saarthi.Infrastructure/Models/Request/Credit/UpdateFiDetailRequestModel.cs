using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Credit
{
    public class UpdateFiDetailRequestModel
    {
            [JsonPropertyName("FleetID")]
            public long FleetID { get; set; }

            [JsonPropertyName("Status")]
            public string Status { get; set; }

            [JsonPropertyName("Comment")]
            public string Comment { get; set; }
    }
}
