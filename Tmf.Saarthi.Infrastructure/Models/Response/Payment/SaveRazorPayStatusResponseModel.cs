using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Payment;

public class SaveRazorPayStatusResponseModel
{
    [JsonPropertyName("reqId")]
    public long ReqId { get; set; }
}
