using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.CPCFacility;

public class UpdateCPCFleetDeviationRequest
{
    [JsonPropertyName("FleetId")]
    public long FleetId { get; set; }

    [JsonPropertyName("DeviationId")]
    public long[] DeviationId { get; set; }

    [JsonPropertyName("CPCCheckStatus")]
    public bool CPCCheckStatus { get; set; }

    [JsonPropertyName("Comment")]
    public string Comment { get; set; } = string.Empty;
}
