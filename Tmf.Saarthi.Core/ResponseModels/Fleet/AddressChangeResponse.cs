using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class AddressChangeResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
