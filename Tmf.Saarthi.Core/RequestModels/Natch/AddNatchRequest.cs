using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Natch;

public class AddNatchRequest
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
}
