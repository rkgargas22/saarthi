using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

public class InstaVeritaLogResponse
{
    [JsonPropertyName("log_Id")]
    public long Log_Id { get; set; }
}
