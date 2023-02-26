using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class SanctionApprovalRequest
{
    [JsonPropertyName("isApproved")]
    public bool IsApproved { get; set; }
}
