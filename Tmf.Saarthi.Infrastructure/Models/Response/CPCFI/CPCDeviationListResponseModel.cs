using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.CPCFI;

public class CPCDeviationListResponseModel
{
    [JsonPropertyName("devId")]
    public int DevId { get; set; }
    [JsonPropertyName("Deviation")]
    public string Deviation { get; set; } = string.Empty;
}
