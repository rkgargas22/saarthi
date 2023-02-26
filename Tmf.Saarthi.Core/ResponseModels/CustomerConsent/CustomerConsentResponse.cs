using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.CustomerConsent;

public class CustomerConsentResponse
{
    [JsonPropertyName("letter")]
    public string Letter { get; set; } = string.Empty;
}
public class CustomerConsentDocumentByFleetResponse
{
    [JsonPropertyName("FleetId")]
    public int FleetId { get; set; }

    [JsonPropertyName("DocumentUrl")]
    public string DocumentUrl { get; set; } = string.Empty;

    [JsonPropertyName("CreatedBy")]
    public string CreatedBy { get; set; } = string.Empty;

    [JsonPropertyName("IsActive")]
    public Boolean IsActive { get; set; }

    [JsonPropertyName("Documenttype")]
    public string Documenttype { get; set; } = string.Empty;
}