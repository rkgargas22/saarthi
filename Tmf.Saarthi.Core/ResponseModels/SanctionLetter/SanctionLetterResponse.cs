using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.SanctionLetter;

public class SanctionLetterResponse
{
    [JsonPropertyName("letter")]
    public string Letter { get; set; } = string.Empty;
}
