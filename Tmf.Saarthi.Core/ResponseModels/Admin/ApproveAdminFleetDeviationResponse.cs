using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Admin
{
    public class ApproveAdminFleetDeviationResponse
    {
        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }
}
