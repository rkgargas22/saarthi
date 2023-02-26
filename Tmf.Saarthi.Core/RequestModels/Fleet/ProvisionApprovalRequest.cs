using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class ProvisionApprovalRequest
{
    [JsonPropertyName("isApproved")]
    public bool IsApproved { get; set; }
}
