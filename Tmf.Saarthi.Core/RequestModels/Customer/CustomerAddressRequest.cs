using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Customer;

public class CustomerAddressRequest
{
    [JsonPropertyName("bpNumber")]
    public long BPNumber { get; set; }
    [JsonPropertyName("Type")]
    public string Type { get; set; } = string.Empty;
    [JsonPropertyName("addressLine1")]
    public string AddressLine1 { get; set; } = string.Empty;
    [JsonPropertyName("addressLine2")]
    public string AddressLine2 { get; set; } = string.Empty;
    [JsonPropertyName("landmark")]
    public string Landmark { get; set; } = string.Empty;
    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;
    [JsonPropertyName("district")]
    public string District { get; set; } = string.Empty;
    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;
    [JsonPropertyName("pincode")]
    public int? Pincode { get; set; }
}
