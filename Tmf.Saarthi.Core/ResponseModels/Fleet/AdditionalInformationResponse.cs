using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class AdditionalInformationResponse
{
    [JsonPropertyName("additionalInfoId")]
    public long AdditionalInfoId { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
