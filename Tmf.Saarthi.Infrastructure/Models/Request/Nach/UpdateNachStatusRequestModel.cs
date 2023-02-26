using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Nach;

public class UpdateNachStatusRequestModel
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonPropertyName("isNach")]
    public bool IsNach { get; set; }

}
