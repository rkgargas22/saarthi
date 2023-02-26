using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Admin
{
    public class AdminFleetDeviationUpdateResponse
    {
        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }
}
