using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.ProvisionalLetter;

public class ProvisionalLetteResponseModel
{
    [JsonPropertyName("letter")]
    public string Letter { get; set; } = string.Empty;
}
