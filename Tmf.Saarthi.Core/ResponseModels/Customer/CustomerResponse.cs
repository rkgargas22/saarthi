namespace Tmf.Saarthi.Core.ResponseModels.Customer;

public class CustomerResponse
{

    public bool Status { get; set; }
    public string Gender { get; set; } = string.Empty;
    public long BPNumber { get; set; }
    public string BPType { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string EmailID { get; set; } = string.Empty;
    public string NoOfVehicleOwned { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public string PanNo { get; set; } = string.Empty;
    public string CustomerType { get; set; } = string.Empty;
}
