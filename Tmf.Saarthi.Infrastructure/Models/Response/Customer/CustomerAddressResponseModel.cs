namespace Tmf.Saarthi.Infrastructure.Models.Response.Customer;

public class CustomerAddressResponseModel
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
    public bool IsDefault { get; set; }
}
