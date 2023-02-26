using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.DMS; 

public class GenerateFanNoResponse 
{
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;
}
