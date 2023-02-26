using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Agent;

public class AgentCustomerResponse
{
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;

    [JsonPropertyName("panNo")]
    public string PanNo { get; set; } = string.Empty;

    [JsonPropertyName("mobileNo")]
    public string MobileNo { get; set; } = string.Empty;

    [JsonPropertyName("bpNo")]
    public long BpNo { get; set; }

    [JsonPropertyName("fleedId")]
    public long FleedId { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("middleName")]
    public string MiddleName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
}
