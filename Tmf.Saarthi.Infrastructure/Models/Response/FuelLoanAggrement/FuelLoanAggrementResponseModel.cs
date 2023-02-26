using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.FuelLoanAggrement;

public class FuelLoanAggrementResponseModel
{
    [JsonPropertyName("letter")]
    public string Letter { get; set; } = string.Empty;
}
