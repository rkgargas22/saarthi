using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Fleet;

public class GetFleetRequestModel
{
    [JsonPropertyName("bPNumber")]
    public long BPNumber { get; set; }
}
