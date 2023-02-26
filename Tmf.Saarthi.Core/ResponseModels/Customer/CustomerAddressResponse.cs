namespace Tmf.Saarthi.Core.ResponseModels.Customer;

public class CustomerAddressResponse
{
    public int AddressID { get; set; }
    public long BPNumber { get; set; }
    public string Type { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string Landmark { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty; 
    public int Pincode { get; set; }
}
