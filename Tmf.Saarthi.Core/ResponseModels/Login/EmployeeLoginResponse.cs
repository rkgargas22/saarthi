namespace Tmf.Saarthi.Core.ResponseModels.Login;

public class EmployeeLoginResponse
{
    public long BPNumber { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string EmailID { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
