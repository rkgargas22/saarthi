using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Admin
{
    public class ApproveAdminFleetDeviationRequestModel
    {
        [JsonPropertyName("VehicleId")]
        public long[] VehicleId { get; set; }

        [JsonPropertyName("Comment")]
        public string Comment { get; set; }
    }
}
