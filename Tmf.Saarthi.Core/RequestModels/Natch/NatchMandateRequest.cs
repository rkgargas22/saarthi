using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Natch;

public class NatchMandateRequest
{
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
