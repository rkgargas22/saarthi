using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class CommentRequest
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    [JsonPropertyName("documents")]
    public List<DocumentDataRequest> Documents { get; set; }
}
