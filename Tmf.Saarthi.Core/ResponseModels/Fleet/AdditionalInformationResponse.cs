using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class AdditionalInformationResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
