using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.ProvisionalLette;

public class ProvisionalLetterResponse
{
    [JsonPropertyName("letter")]
    public string Letter { get; set; } = string.Empty;
}