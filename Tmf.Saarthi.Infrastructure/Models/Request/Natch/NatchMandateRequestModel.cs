using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Natch;

public class NatchMandateRequestModel
{
    [JsonPropertyName("FleetID")]
    public long FleetID { get; set; }

    [JsonPropertyName("umrn")]
    public string UMRN { get; set; } = string.Empty;

    [JsonPropertyName("emandateId")]
    public string EmandateId { get; set; } = string.Empty;

    [JsonPropertyName("emandateDate")]
    public DateTime? EmandateDate { get; set; }

    [JsonPropertyName("corporateName")]
    public string CorporateName { get; set; } = string.Empty;

    [JsonPropertyName("utilityNo")]
    public string UtilityNo { get; set; } = string.Empty;
}
