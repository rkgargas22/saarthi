using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Nach;

public class UpdateNachStatusRequest
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonPropertyName("isNach")]
    public bool IsNach { get; set; }

}
