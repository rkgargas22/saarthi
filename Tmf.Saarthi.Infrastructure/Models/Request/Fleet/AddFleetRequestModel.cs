using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Infrastructure.Models.Request.Fleet;

public class AddFleetRequestModel
{
    [JsonPropertyName("bpNumber")]
    public long BPNumber { get; set; }
    [JsonPropertyName("fanNo")]
    public string FanNo { get; set; } = string.Empty;
    [JsonPropertyName("vehicleLimit")]
    public int VehicleLimit { get; set; }
    [JsonPropertyName("categoryType")]
    public string CategoryType { get; set; } = string.Empty;
    [JsonPropertyName("subCategoryType")]
    public string SubCategoryType { get; set; } = string.Empty;
    [JsonPropertyName("perVehicleSanction")]
    public int PerVehicleSanction { get; set; }
    [JsonPropertyName("stampDuty")]
    public decimal StampDuty { get; set; }
    [JsonPropertyName("vehicleAgeCriteria")]
    public int VehicleAgeCriteria { get; set; }
    [JsonPropertyName("processingFee")]
    public decimal ProcessingFee { get; set; }
    [JsonPropertyName("isProvisionLetterApproved")]
    public bool? IsProvisionLetterApproved { get; set; }
    [JsonPropertyName("isSanctionLetterApproved")]
    public bool? IsSanctionLetterApproved { get; set; }
    [JsonPropertyName("isAgreementLetterApproved")]
    public bool? IsAgreementLetterApproved { get; set; }
    [JsonPropertyName("isENachApproved")]
    public bool? IsENachApproved { get; set; }
    [JsonPropertyName("isMNachApproved")]
    public bool? IsMNachApproved { get; set; }
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }
    [JsonPropertyName("IRR")]
    public decimal? IRR { get; set; }
    [JsonPropertyName("AIR")]
    public decimal? AIR { get; set; }
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    [JsonPropertyName("createdBy")]
    public long CreatedBy { get; set; }
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
}
