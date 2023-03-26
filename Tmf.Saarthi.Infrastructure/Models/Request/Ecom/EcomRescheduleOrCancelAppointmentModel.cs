using System.Text.Json.Serialization;


namespace Tmf.Saarthi.Infrastructure.Models.Request.Ecom
{
    public class EcomRescheduleOrCancelAppointmentModel
    {
        [JsonPropertyName("BpNo")]
        public string BpNo { get; set; } = string.Empty;

        [JsonPropertyName("UserType")]
        public string UserType { get; set; } = string.Empty;

        [JsonPropertyName("json_input")]
        public List<EcomRescheduleOrCancelAppointmentJsonInput>? JsonInput { get; set; }
    }

    public class EcomRescheduleOrCancelAppointmentJsonInput
    {
        [JsonPropertyName("awb")]
        public string Awb { get; set; } = string.Empty;
        [JsonPropertyName("instruction")]
        public string Instruction { get; set; } = string.Empty;
        [JsonPropertyName("comments")]
        public string Comments { get; set; } = string.Empty;
        [JsonPropertyName("consignee_address")]
        public ConsigneeAddress? ConsigneeAddress { get; set; }
        [JsonPropertyName("mobile")]
        public string Mobile { get; set; } = string.Empty;
        [JsonPropertyName("scheduled_delivery_date")]
        public string ScheduledDeliveryDate { get; set; } = string.Empty;
        [JsonPropertyName("scheduled_delivery_slot")]
        public string ScheduledDeliverySlot { get; set; } = string.Empty;
    }

    public class ConsigneeAddress
    {
        [JsonPropertyName("CA1")]
        public string CA1 { get; set; } = string.Empty;
        [JsonPropertyName("CA2")]
        public string CA2 { get; set; } = string.Empty;
        [JsonPropertyName("CA3")]
        public string CA3 { get; set; } = string.Empty;
        [JsonPropertyName("CA4")]
        public string CA4 { get; set; } = string.Empty;
        [JsonPropertyName("pincode")]
        public string Pincode { get; set; } = string.Empty;
    }

}
