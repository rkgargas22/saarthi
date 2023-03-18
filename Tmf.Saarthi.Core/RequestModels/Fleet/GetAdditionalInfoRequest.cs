using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class GetAdditionalInfoRequest
{
    [JsonPropertyName("fleetId")]
    public long FleetId { get; set; }

    [JsonPropertyName("departmentCode")]
    public string DepartmentCode { get; set; } = string.Empty;
}
