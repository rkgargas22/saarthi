using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Nach;

public class NachStatusAndTimeslotResponse
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("Status")]
    public bool? Status { get; set; }

    [JsonPropertyName("TimeSlotDate")]
    public DateTime? TimeSlotDate { get; set; }
}
