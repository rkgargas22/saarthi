using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Ecom
{
    public class EcomPushShipmentTrackModel
    {
        [JsonPropertyName("awb_number")]
        public string AwbNumber { get; set; } = string.Empty;

        [JsonPropertyName("agent_name")]
        public string AgentName { get; set; } = string.Empty;

        [JsonPropertyName("agent_id")]
        public string AgentId { get; set; } = string.Empty;

        [JsonPropertyName("vendor_code")]
        public string VendorCode { get; set; } = string.Empty;

        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;

        [JsonPropertyName("rescheduled_date ")]
        public string RescheduledDate { get; set; } = string.Empty;

        [JsonPropertyName("rescheduled_time ")]
        public string RescheduledTime { get; set; } = string.Empty;

        [JsonPropertyName("reason_code_number")]
        public string ReasonCodeNumber { get; set; } = string.Empty;

        [JsonPropertyName("reason_code_description")]
        public string ReasonCodeDescription { get; set; } = string.Empty;

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; } = string.Empty;

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = string.Empty;

        [JsonPropertyName("document")]
        public List<Documents>? Document { get; set; }
    }

    public class Documents
    {
        [JsonPropertyName("image")]
        public List<string>? Image { get; set; }

        [JsonPropertyName("activity_code")]
        public string ActivityCode { get; set; } = string.Empty;
    }
}
