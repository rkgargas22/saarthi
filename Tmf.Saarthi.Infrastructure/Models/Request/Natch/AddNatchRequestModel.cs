using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Natch;

public class AddNatchRequestModel
{
    [JsonPropertyName("fleetId")]
    public long FleetID { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("frequency")]
    public string Frequency { get; set; } = string.Empty;

    [JsonPropertyName("purposeOfManadate")]
    public string PurposeOfManadate { get; set; } = string.Empty;

    [JsonPropertyName("isEnach")]
    public bool IsEnach { get; set; }
    
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("maxAmount")]
    public decimal? MaxAmount { get; set; }
}
