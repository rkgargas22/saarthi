using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Response.Payment;

public class SavePaymentStatusResponseModel
{
    [JsonPropertyName("reqID")]
    public long ReqID { get; set; }
}
