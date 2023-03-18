using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Fleet;

public class GetAdditionalInfoRequestModel
{
    [JsonPropertyName("fleetId")]
    public long FleetId { get; set; }

    [JsonPropertyName("departmentCode")]
    public string DepartmentCode { get; set; } = string.Empty;
}
