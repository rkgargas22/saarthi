using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Agent;

public class AgentRejectedFleetResponseModel
{
    [JsonPropertyName("fleetId")]
    public long FleetId { get; set; }

    [JsonPropertyName("rCNo")]
    public string RcNo { get; set; } = string.Empty;

    [JsonPropertyName("ownerName")]
    public string OwnerName { get; set; } = string.Empty;

    [JsonPropertyName("registrationDate")]
    public DateTime? RegistrationDate { get; set; } 

    [JsonPropertyName("vehicleType")]
    public string VehicleType { get; set; } = string.Empty;
    
    [JsonPropertyName("vehicleModel")]
    public string VehicleModel { get; set; } = string.Empty;
    
    [JsonPropertyName("vehicleCompany")]
    public string VehicleCompany { get; set; } = string.Empty;
}
