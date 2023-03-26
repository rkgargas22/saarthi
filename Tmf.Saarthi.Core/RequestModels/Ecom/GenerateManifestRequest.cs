using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.RequestModels.Ecom
{
    public class GenerateManifestRequest
    {
        [JsonPropertyName("ORDER_NUMBER")]
        public string OrderNumber { get; set; } = string.Empty;

        [JsonPropertyName("CONSIGNEE")]
        public string Consignee { get; set; } = string.Empty;

        [JsonPropertyName("CONSIGNEE_ADDRESS1")]
        public string ConsigneeAddress1 { get; set; } = string.Empty;

        [JsonPropertyName("CONSIGNEE_ADDRESS2")]
        public string ConsigneeAddress2 { get; set; } = string.Empty;

        [JsonPropertyName("CONSIGNEE_ADDRESS3")]
        public string ConsigneeAddress3 { get; set; } = string.Empty;

        [JsonPropertyName("CONSIGNEE_ADDRESS4")]
        public string ConsigneeAddress4 { get; set; } = string.Empty;

        [JsonPropertyName("COLLECTABLE_VALUE")]
        public string CollectableValue { get; set; } = string.Empty;

        [JsonPropertyName("DESTINATION_CITY")]
        public string DestinationCity { get; set; } = string.Empty;

        [JsonPropertyName("PINCODE")]
        public string Pincode { get; set; } = string.Empty;

        [JsonPropertyName("STATE")]
        public string State { get; set; } = string.Empty;

        [JsonPropertyName("MOBILE")]
        public string Mobile { get; set; } = string.Empty;

        [JsonPropertyName("TELEPHONE")]
        public string Telephone { get; set; } = string.Empty;

        [JsonPropertyName("ITEM_DESCRIPTION")]
        public string ItemDescription { get; set; } = string.Empty;

        [JsonPropertyName("DROP_VENDOR_CODE")]
        public string DropVendorCode { get; set; } = string.Empty;

        [JsonPropertyName("DROP_NAME")]
        public string DropName { get; set; } = string.Empty;

        [JsonPropertyName("DROP_ADDRESS_LINE1")]
        public string DropAddressLine1 { get; set; } = string.Empty;

        [JsonPropertyName("DROP_ADDRESS_LINE2")]
        public string DropAddressLine2 { get; set; } = string.Empty;

        [JsonPropertyName("DROP_ADDRESS_LINE3")]
        public string DropAddressLine3 { get; set; } = string.Empty;

        [JsonPropertyName("DROP_ADDRESS_LINE4")]
        public string DropAddressLine4 { get; set; } = string.Empty;

        [JsonPropertyName("DROP_PINCODE")]
        public string DropPincode { get; set; } = string.Empty;

        [JsonPropertyName("DROP_MOBILE")]
        public string DropMobile { get; set; } = string.Empty;

        [JsonPropertyName("ACTIVITIES")]
        public List<Activity>? Activites { get; set; }

        [JsonPropertyName("ADDITIONAL_INFORMATION")]
        public AdditionalInformation? AdditionalInformation { get; set; }
    }

    public class Activity
    {
        [JsonPropertyName("CODE")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("DOCUMENT_REF_NUMBER")]
        public string DocumentRefNumber { get; set; } = string.Empty;

        [JsonPropertyName("REMARKS")]
        public string Remarks { get; set; } = string.Empty;

        [JsonPropertyName("OPTIONAL")]
        public string Optional { get; set; } = string.Empty;
    }

    public class AdditionalInformation
    {
        [JsonPropertyName("DATE")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("TIMESLOT")]
        public string Timeslot { get; set; } = string.Empty;

        [JsonPropertyName("FORM_PRINT")]
        public string FormPrint { get; set; } = string.Empty;
    }

}
