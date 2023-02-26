using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Nach;

public class UpdateNachTimeSlotRequest
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("isNach")]
    public bool IsNach { get; set; }

    [JsonPropertyName("timeSlot")]
    public DateTime TimeSlot { get; set; }
}
