using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Payment; 

public class SavePaymentStatusResponse 
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
