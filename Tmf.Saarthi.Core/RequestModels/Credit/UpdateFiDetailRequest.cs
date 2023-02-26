using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Credit
{
    public class UpdateFiDetailRequest
    {
        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("Comment")]
        public string Comment { get; set; }
    }
}
