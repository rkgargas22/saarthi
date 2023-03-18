using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Natch;

public class NatchStatusAndTimeslotResponseModel
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("Status")]
    public bool? Status { get; set; }
    
    [JsonPropertyName("TimeSlotDate")]
    public DateTime? TimeSlotDate { get; set; }

}
