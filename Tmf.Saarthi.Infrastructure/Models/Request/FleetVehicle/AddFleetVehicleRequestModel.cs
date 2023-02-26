using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;

public class AddFleetVehicleRequestModel
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("rCNo")]
    public string RCNo { get; set; } = string.Empty;
    [JsonPropertyName("isSubmitted")]
    public bool IsSubmitted { get; set; }
    [JsonPropertyName("isApproved")]
    public bool IsApproved { get; set; }
    [JsonPropertyName("reject_Reason")]
    public string Reject_Reason { get; set; } = string.Empty;
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
    [JsonPropertyName("isCallCenterApproved")]
    public bool IsCallCenterApproved { get; set; }
    [JsonPropertyName("agentRemark")]
    public string AgentRemark { get; set; } = string.Empty;
    [JsonPropertyName("ssAdminApproved")]
    public bool? IsAdminApproved { get; set; }
    [JsonPropertyName("adminRemark")]
    public string AdminRemark { get; set; } = string.Empty;
    [JsonPropertyName("registrationDate")]
    public DateTime? RegistrationDate { get; set; }
    [JsonPropertyName("expiryDate")]
    public DateTime? ExpiryDate { get; set; }
    [JsonPropertyName("vehicleType")]
    public string VehicleType { get; set; } = string.Empty;
    [JsonPropertyName("chassisNo")]
    public string ChassisNo { get; set; } = string.Empty;
    [JsonPropertyName("engineNo")]
    public string EngineNo { get; set; } = string.Empty;
    [JsonPropertyName("vehicleCompany")]
    public string VehicleCompany { get; set; } = string.Empty;
    [JsonPropertyName("vehicleModel")]
    public string VehicleModel { get; set; } = string.Empty;
    [JsonPropertyName("ownerName")]
    public string OwnerName { get; set; } = string.Empty;
    [JsonPropertyName("firNumber")]
    public string FirNumber { get; set; } = string.Empty;
    [JsonPropertyName("firDate")]
    public DateTime? FirDate { get; set; }
    [JsonPropertyName("isBlacklisted")]
    public bool IsBlacklisted { get; set; }
    [JsonPropertyName("blackListedReason")]
    public string BlackListedReason { get; set; } = string.Empty;
    [JsonPropertyName("createdBy")]
    public long CreatedBy { get; set; }
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
}
