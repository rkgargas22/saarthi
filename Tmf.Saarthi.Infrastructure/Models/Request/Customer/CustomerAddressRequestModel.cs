namespace Tmf.Saarthi.Infrastructure.Models.Request.Customer;

public class CustomerAddressRequestModel
{
    public int AddressID { get; set; }
    public long BPNumber { get; set; }
    public string Type { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string Landmark { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string District { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public int Pincode { get; set; }
    public bool IsDefault { get; set; }
}
