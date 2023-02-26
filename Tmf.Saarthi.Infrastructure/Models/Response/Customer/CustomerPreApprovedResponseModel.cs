namespace Tmf.Saarthi.Infrastructure.Models.Response.Customer;

public class CustomerPreApprovedResponseModel
{
    public long PreApprovedID { get; set; }
    public long BPNumber { get; set; }
    public int CustNotiSourceId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string EmailID { get; set; } = string.Empty;
    public string SharedLink { get; set; } = string.Empty;
    public string Days { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
