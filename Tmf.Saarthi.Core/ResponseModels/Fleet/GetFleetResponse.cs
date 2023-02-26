using System.Text.Json.Serialization;
using Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class GetFleetResponse
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
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
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }
    [JsonPropertyName("IRR")]
    public decimal? IRR { get; set; }
    [JsonPropertyName("AIR")]
    public decimal? AIR { get; set; }
    [JsonPropertyName("loanAmount")]
    public decimal? LoanAmount { get; set; }
    [JsonPropertyName("processingFeeAmount")]
    public decimal? ProcessingFeeAmount { get; set; }
    [JsonPropertyName("stampDutyAmount")]
    public decimal? StampDutyAmount { get; set; }
    [JsonPropertyName("agreementDate")]
    public DateTime? AgreementDate { get; set; }
    [JsonPropertyName("requestedIRR")]
    public decimal? RequestedIRR { get; set; }
    [JsonPropertyName("requestedProcessingFees")]
    public decimal? RequestedProcessingFees { get; set; }
    [JsonPropertyName("newIRR")]
    public decimal? NewIRR { get; set; }
    [JsonPropertyName("newAIR")]
    public decimal? NewAIR { get; set; }
    [JsonPropertyName("newProcessing")]
    public decimal? NewProcessing { get; set; }
    [JsonPropertyName("requestedAIR")]
    public decimal? RequestedAIR { get; set; }
    [JsonPropertyName("additionalInformation")]
    public string AdditionalInformation { get; set; } = string.Empty;
    [JsonPropertyName("departmentType")]
    public string DepartmentType { get; set; } = string.Empty;
    [JsonPropertyName("assignedTo")]
    public long? AssignedTo { get; set; }
    [JsonPropertyName("assignedToRoleId")]
    public long? AssignedToRoleId { get; set; }
    [JsonPropertyName("agentId")]
    public long? AgentId { get; set; }
    [JsonPropertyName("adminId")]
    public long? AdminId { get; set; }
    [JsonPropertyName("creditId")]
    public long? CreditId { get; set; }
    [JsonPropertyName("cpcFiId")]
    public long? CpcFiId { get; set; }
    [JsonPropertyName("cpcTlFiId")]
    public long? CpcTlFiId { get; set; }
    [JsonPropertyName("cpcFcId")]
    public long? CpcFcId { get; set; }
    [JsonPropertyName("cpcTlFcId")]
    public long? CpcTlFcId { get; set; }
    [JsonPropertyName("isAddressChanged")]
    public bool? IsAddressChanged { get; set; }
    [JsonPropertyName("vehicleList")]
    public IEnumerable<GetFleetVehicleResponse> VehicleList { get; set; }
}
