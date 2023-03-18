using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Natch;

public class UpdateNatchTimeSlotRequestModel
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("isNach")]
    public bool IsNach { get; set; }

    [JsonPropertyName("timeSlot")]
    public DateTime TimeSlot { get; set; }
}
