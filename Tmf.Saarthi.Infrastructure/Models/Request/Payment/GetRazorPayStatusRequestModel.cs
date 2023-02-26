using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Payment;

public class GetRazorPayStatusRequestModel
{
    [JsonPropertyName("reqNo")]
    public string ReqNo { get; set; } = string.Empty;

    [JsonPropertyName("reqFlag")]
    public string ReqFlag { get; set; } = string.Empty;

    [JsonPropertyName("T_ID")]
    public string TID { get; set; } = string.Empty;

    [JsonPropertyName("Token")]
    public string Token { get; set; } = string.Empty;
}
