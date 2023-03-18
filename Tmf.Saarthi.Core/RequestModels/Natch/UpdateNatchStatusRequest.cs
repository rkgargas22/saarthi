using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Natch;

public class UpdateNatchStatusRequest
{
    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonPropertyName("isNach")]
    public bool IsNach { get; set; }

}
