using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Natch;

public class UpdateNatchStatusRequestModel
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonPropertyName("isNach")]
    public bool IsNach { get; set; }

}
