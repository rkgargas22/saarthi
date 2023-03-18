using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Natch;

public class UpdateNatchTimeSlotRequest
{
    [JsonPropertyName("isNach")]
    public bool IsNach { get; set; }

    [JsonPropertyName("timeSlot")]
    public DateTime TimeSlot { get; set; }
}
