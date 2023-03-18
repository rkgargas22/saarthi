using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Login;

public class LoginResponseModel
{
    //[JsonPropertyName("status_code")]
    //public string StatusCode { get; set; } = string.Empty;

    //[JsonPropertyName("message")]
    //public string Message { get; set; } = string.Empty;

    //[JsonPropertyName("mobile_no")]
    //public string MobileNo { get; set; } = string.Empty;

    //[JsonPropertyName("user_name")]
    //public string UserName { get; set; } = string.Empty;

    //[JsonPropertyName("bp_no")]
    //public string BpNo { get; set; } = string.Empty;

    //[JsonPropertyName("email")]
    //public string Email { get; set; } = string.Empty;

    public int AddressID { get; set; }
    public bool Status { get; set; }
    public string Gender { get; set; } = string.Empty;
    public long BPNumber { get; set; }
    public long PreApprovedID { get; set; }
    public string BPType { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string EmailID { get; set; } = string.Empty;
    public string CKycNo { get; set; } = string.Empty;
    public string AadharNo { get; set; } = string.Empty;
    public string CustomerType { get; set; } = string.Empty;
    public string NoOfVehicleOwned { get; set; } = string.Empty;
    public string GStnNo { get; set; } = string.Empty;
    public string PanNo { get; set; } = string.Empty;
    public string GroupKey { get; set; } = string.Empty;
    public DateTime LastUpdateDate { get; set; }
    public DateTime Dob { get; set; }
}
