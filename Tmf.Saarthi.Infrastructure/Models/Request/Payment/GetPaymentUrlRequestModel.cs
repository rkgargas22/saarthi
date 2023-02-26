using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Payment;

public class GetPaymentUrlRequestModel
{
    [JsonPropertyName("payemi")]
    public string PayEMI { get; set; } = string.Empty;
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;
    [JsonPropertyName("viewFrom")]
    public string ViewFrom { get; set; } = string.Empty;
    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }
    [JsonPropertyName("payemi")]
    public decimal? CC { get; set; }
    [JsonPropertyName("bPNumber")]
    public long BPNumber { get; set; }
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;
    [JsonPropertyName("reqID")]
    public string ReqID { get; set; } = string.Empty;
    [JsonPropertyName("mobileNo")]
    public string MobileNo { get; set; } = string.Empty;
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("fleetID")]
    public bool IsActive { get; set; }
    [JsonPropertyName("fleetID")]
    public long CreatedBy { get; set; }
    [JsonPropertyName("fleetID")]
    public DateTime CreatedDate { get; set; }
}
