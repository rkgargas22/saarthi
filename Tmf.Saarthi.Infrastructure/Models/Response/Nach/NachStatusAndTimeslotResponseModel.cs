using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Nach;

public class NachStatusAndTimeslotResponseModel
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("Status")]
    public bool? Status { get; set; }
    
    [JsonPropertyName("TimeSlotDate")]
    public DateTime? TimeSlotDate { get; set; }

}
