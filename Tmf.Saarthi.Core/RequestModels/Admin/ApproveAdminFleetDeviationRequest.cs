using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Admin;

public class ApproveAdminFleetDeviationRequest
{

    [JsonPropertyName("VehicleId")]
    public long[] VehicleId { get; set; }

    [JsonPropertyName("Comment")]
    public string Comment { get; set; }

}
