using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class AddressChangeRequest
{
    [JsonPropertyName("isAddressChange")]
    public bool IsAddressChange { get; set; }
}
