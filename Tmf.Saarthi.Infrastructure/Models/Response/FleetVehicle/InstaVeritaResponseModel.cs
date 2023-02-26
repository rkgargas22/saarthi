using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.FleetVehicle;

public class InstaVeritaResponseModel
{
    [JsonPropertyName("blacklisted")]
    public dynamic? Blacklisted { get; set; } = string.Empty;
    [JsonPropertyName("blacklisted_reason")]
    public string BlacklistedReason { get; set; } = string.Empty;
    [JsonPropertyName("chassis_number")]
    public string ChassisNumber { get; set; } = string.Empty;
    [JsonPropertyName("engine_number")]
    public string EngineNumber { get; set; } = string.Empty;
    [JsonPropertyName("expiry_date")]
    public dynamic? ExpiryDate { get; set; } = string.Empty;
    [JsonPropertyName("fitness_certificate_expiry_date")]
    public dynamic? FitnessCertificateExpiryDate { get; set; } = string.Empty;
    [JsonPropertyName("financing_authority")]
    public string FinancingAuthority { get; set; } = string.Empty;
    [JsonPropertyName("fuel_type")]
    public string FuelType { get; set; } = string.Empty;
    [JsonPropertyName("mv_tax_paid_upto")]
    public string MVTaxPaidUpto { get; set; } = string.Empty;
    [JsonPropertyName("mv_tax_structure")]
    public string MVTaxStructure { get; set; } = string.Empty;
    [JsonPropertyName("owners_name")]
    public string OwnersName { get; set; } = string.Empty;
    [JsonPropertyName("owner_serial_number")]
    public string OwnerSerialNumber { get; set; } = string.Empty;
    [JsonPropertyName("pucc_upto")]
    public string PuccUpto { get; set; } = string.Empty;
    [JsonPropertyName("registration_number")]
    public string RegistrationNumber { get; set; } = string.Empty;
    [JsonPropertyName("registration_date")]
    public dynamic? RegistrationDate { get; set; } = string.Empty;
    [JsonPropertyName("registering_authority")]
    public string RegisteringAuthority { get; set; } = string.Empty;
    [JsonPropertyName("registration_state")]
    public string RegistrationState { get; set; } = string.Empty;
    [JsonPropertyName("vehicle_company")]
    public string VehicleCompany { get; set; } = string.Empty;
    [JsonPropertyName("vehicle_model")]
    public string VehicleModel { get; set; } = string.Empty;
    [JsonPropertyName("vehicle_type")]
    public string VehicleType { get; set; } = string.Empty;
    [JsonPropertyName("vehicle_age")]
    public string VehicleAge { get; set; } = string.Empty;
    [JsonPropertyName("blacklisted_details")]
    public List<BlackListDetailsModel>? BlacklistedDetails { get; set; }
}

public class BlackListDetailsModel
{
    [JsonPropertyName("registration_state")]
    public string RegistrationState { get; set; } = string.Empty;
    [JsonPropertyName("registering_authority")]
    public string RegisteringAuthority { get; set; } = string.Empty;
    [JsonPropertyName("rc_number")]
    public string RcNumber { get; set; } = string.Empty;
    [JsonPropertyName("fir_number")]
    public string FirNumber { get; set; } = string.Empty;
    [JsonPropertyName("fir_date")]
    public dynamic? FirDate { get; set; } = string.Empty;
    [JsonPropertyName("blacklisted_reason")]
    public string BlacklistedReason { get; set; } = string.Empty;
    [JsonPropertyName("blacklisted_date")]
    public dynamic? BlacklistedDate { get; set; } = string.Empty;

}
