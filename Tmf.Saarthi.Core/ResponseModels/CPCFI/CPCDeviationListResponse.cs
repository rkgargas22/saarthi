using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.CPCFI;

public class CPCDeviationListResponse
{
    [JsonPropertyName("devId")]
    public int DevId { get; set; }
    [JsonPropertyName("Deviation")]
    public string Deviation { get; set; } = string.Empty;
}
