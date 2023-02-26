using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.DMS;

public class GenerateFanNoResponseModel
{
    [JsonPropertyName("statusCode")]
    public string StatusCode { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("fanno")]
    public string Fanno { get; set; } = string.Empty;

    [JsonPropertyName("r_object_id")]
    public object RObjectId { get; set; } = string.Empty;
}
