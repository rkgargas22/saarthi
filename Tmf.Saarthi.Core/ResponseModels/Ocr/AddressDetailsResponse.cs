using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Ocr;

public class AddressDetailsResponse
{
    [JsonPropertyName("BPNo")]
    public long BPNo { get; set; } = 0;
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
    [JsonPropertyName("streetAddress")]
    public string StreetAddress { get; set; } = string.Empty;
    [JsonPropertyName("district")]
    public string District { get; set; } = string.Empty;
    [JsonPropertyName("pincode")]
    public string Pincode { get; set; } = string.Empty;
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;
}
