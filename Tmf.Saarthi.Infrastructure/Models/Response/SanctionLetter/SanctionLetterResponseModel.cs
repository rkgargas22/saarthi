using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.SanctionLetter;

public class SanctionLetterResponseModel
{
    [JsonPropertyName("letter")]
    public string Letter { get; set; } = string.Empty;
}
