using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Fleet;

public class ProvisionApprovalResponseModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
}
