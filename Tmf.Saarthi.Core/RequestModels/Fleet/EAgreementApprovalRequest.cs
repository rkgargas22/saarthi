using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Fleet;

public class EAgreementApprovalRequest
{
    [JsonPropertyName("isApproved")]
    public bool IsApproved { get; set; }
}
