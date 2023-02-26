namespace Tmf.Saarthi.Infrastructure.Models.Response.Customer;

public class CustomerResponseModel
{
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
