using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.CPCFI;

public class CPCDashboardResponseModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("customerName")]
    public string CustomerName { get; set; } = string.Empty;

    [JsonPropertyName("assignDate")]
    public DateTime? AssignDate { get; set; }

    [JsonPropertyName("expiryDate")]
    public DateTime? ExpiryDate { get; set; }

    [JsonPropertyName("assignedAgent")]
    public string AssignedAgent { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}
