using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Credit
{
    public class UpdateFiDetailResponse
    {
        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }
}
