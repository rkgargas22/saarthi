using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Login;

public class EmployeeMasterResponseModel
{
    [JsonPropertyName("bpNumber")]
    public long BPNumber { get; set; }
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;
    [JsonPropertyName("middleName")]
    public string MiddleName { get; set; } = string.Empty;
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;
    [JsonPropertyName("mobileNo")]
    public string MobileNo { get; set; } = string.Empty;
    [JsonPropertyName("emailID")]
    public string EmailID { get; set; } = string.Empty;
    [JsonPropertyName("defaultRole")]
    public string DefaultRole { get; set; } = string.Empty;
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

}
