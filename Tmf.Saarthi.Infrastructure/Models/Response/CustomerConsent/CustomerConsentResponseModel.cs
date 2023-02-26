using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.CustomerConsent;

public class CustomerConsentResponseModel
{
    [JsonPropertyName("letter")]
    public string Letter { get; set; } = string.Empty;
}
public class CustomerConsentDocumentByFleetResponseModel
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


