using System.Text.Json.Serialization;

namespace Tmf.Saarthi.Core.ResponseModels.Fleet;

public class VerifyFleetResponse
{
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("bPNumber")]
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
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    [JsonPropertyName("additionalInformation")]
    public string AdditionalInformation { get; set; } = string.Empty;
    [JsonPropertyName("departmentType")]
    public string DepartmentType { get; set; } = string.Empty;
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

    [JsonPropertyName("AccountNumber")]
    public string AccountNumber { get; set; }
    [JsonPropertyName("IFSCCode")]
    public string IFSCCode { get; set; }
    [JsonPropertyName("ApplicantName")]
    public string ApplicantName { get; set; }

    [JsonPropertyName("fleetVehicles")]
    public List<VerifyFleetVehicleResponse> FleetVehicles { get; set; }
}

public class VerifyFleetVehicleResponse
{
    [JsonPropertyName("vehicleID")]
    public long VehicleID { get; set; }
    [JsonPropertyName("fleetID")]
    public long FleetID { get; set; }
    [JsonPropertyName("rCNo")]
    public string RCNo { get; set; } = string.Empty;
    [JsonPropertyName("isSubmitted")]
    public bool IsSubmitted { get; set; }
    [JsonPropertyName("isApproved")]
    public bool IsApproved { get; set; }
    [JsonPropertyName("reject_Reason")]
    public string Reject_Reason { get; set; } = string.Empty;
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
    [JsonPropertyName("isCallCenterApproved")]
    public bool IsCallCenterApproved { get; set; }
    [JsonPropertyName("agentRemark")]
    public string AgentRemark { get; set; } = string.Empty;
    [JsonPropertyName("ssAdminApproved")]
    public bool? IsAdminApproved { get; set; }
    [JsonPropertyName("adminRemark")]
    public string AdminRemark { get; set; } = string.Empty;
    [JsonPropertyName("registrationDate")]
    public DateTime? RegistrationDate { get; set; }
    [JsonPropertyName("expiryDate")]
    public DateTime? ExpiryDate { get; set; }
    [JsonPropertyName("vehicleType")]
    public string VehicleType { get; set; } = string.Empty;
    [JsonPropertyName("chassisNo")]
    public string ChassisNo { get; set; } = string.Empty;
    [JsonPropertyName("engineNo")]
    public string EngineNo { get; set; } = string.Empty;
    [JsonPropertyName("vehicleCompany")]
    public string VehicleCompany { get; set; } = string.Empty;
    [JsonPropertyName("vehicleModel")]
    public string VehicleModel { get; set; } = string.Empty;
    [JsonPropertyName("ownerName")]
    public string OwnerName { get; set; } = string.Empty;
    [JsonPropertyName("firNumber")]
    public string FirNumber { get; set; } = string.Empty;
    [JsonPropertyName("firDate")]
    public DateTime? FirDate { get; set; }
    [JsonPropertyName("isBlacklisted")]
    public bool IsBlacklisted { get; set; }
    [JsonPropertyName("blackListedReason")]
    public string BlackListedReason { get; set; } = string.Empty;
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
}
