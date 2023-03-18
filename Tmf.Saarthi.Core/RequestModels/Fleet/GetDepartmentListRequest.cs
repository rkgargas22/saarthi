using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class GetDepartmentListRequest
{
    [JsonPropertyName("fleetId")]
    public long FleetId { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;
}
