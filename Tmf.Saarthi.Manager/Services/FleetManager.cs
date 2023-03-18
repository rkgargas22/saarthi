using Microsoft.Extensions.Options;
using Tmf.Saarthi.Core.Enums;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.RequestModels.DMS;
using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.RequestModels.Fleet;
using Tmf.Saarthi.Core.RequestModels.FleetVehicle;
using Tmf.Saarthi.Core.ResponseModels.Customer;
using Tmf.Saarthi.Core.ResponseModels.DMS;
using Tmf.Saarthi.Core.ResponseModels.Document;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.FleetVehicle;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Fleet;
using Tmf.Saarthi.Infrastructure.Models.Response.DocumentTypeMstr;
using Tmf.Saarthi.Infrastructure.Models.Response.Fleet;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class FleetManager : IFleetManager
{
    private readonly IFleetRepository _fleetRepository;
    private readonly IFleetVehicleManager _fleetVehicleManager;
    private readonly ICustomerManager _customerManager;
    private readonly IDMSManager _dMSManager;
    private readonly IUploadManager _uploadManager;
    private readonly FleetConfigurationOptions _fleetConfigurationOptions;
    private readonly IDocumentTypeMstrManager _documentTypeMstrManager;

    public FleetManager(IFleetRepository fleetRepository, 
                        IFleetVehicleRepository fleetVehicleRepository, 
                        IFleetVehicleManager fleetVehicleManager, 
                        ICustomerManager customerManager, 
                        IOptions<FleetConfigurationOptions> fleetConfigurationOptions, 
                        IDMSManager dMSManager, 
                        IUploadManager uploadManager,
                        IDocumentTypeMstrManager documentTypeMstrManager)
    {
        _fleetRepository = fleetRepository;
        _fleetVehicleManager = fleetVehicleManager;
        _customerManager = customerManager;
        _fleetConfigurationOptions = fleetConfigurationOptions.Value;
        _dMSManager = dMSManager;
        _uploadManager = uploadManager;
        _documentTypeMstrManager = documentTypeMstrManager;
    }

    public async Task<GetFleetResponse> Add(long BPNumber)
    {
        GetFleetResponse getFleetResponse = new GetFleetResponse();

        AddFleetRequestModel addFleetRequestModel = new AddFleetRequestModel();
        addFleetRequestModel.BPNumber = BPNumber;
        addFleetRequestModel.FanNo = string.Empty;
        addFleetRequestModel.VehicleLimit = _fleetConfigurationOptions.VehicleLimit;
        addFleetRequestModel.CategoryType = _fleetConfigurationOptions.VehicleType;
        addFleetRequestModel.SubCategoryType = string.Empty;
        addFleetRequestModel.PerVehicleSanction = _fleetConfigurationOptions.PerVehicleSanction;
        addFleetRequestModel.StampDuty = _fleetConfigurationOptions.StampDuty;
        addFleetRequestModel.VehicleAgeCriteria = _fleetConfigurationOptions.VehicleAgeCriteria;
        addFleetRequestModel.ProcessingFee = _fleetConfigurationOptions.ProcessingFee;
        addFleetRequestModel.IsAgreementLetterApproved = null;
        addFleetRequestModel.IsENachApproved = null;
        addFleetRequestModel.IsMNachApproved = null;
        addFleetRequestModel.IsProvisionLetterApproved = null;
        addFleetRequestModel.IsSanctionLetterApproved = null;
        addFleetRequestModel.IsActive = true;
        addFleetRequestModel.Comment = string.Empty;
        addFleetRequestModel.Amount = null;
        addFleetRequestModel.IRR = _fleetConfigurationOptions.IRR;
        addFleetRequestModel.AIR = _fleetConfigurationOptions.AIR;
        addFleetRequestModel.CreatedBy = BPNumber;
        addFleetRequestModel.CreatedDate = DateTime.Now;
        FleetResponseModel fleetResponseModel = await _fleetRepository.AddFleet(addFleetRequestModel);

        if (fleetResponseModel != null)
        {
            getFleetResponse.FleetID = fleetResponseModel.FleetID;
            getFleetResponse.BPNumber = addFleetRequestModel.BPNumber;
            getFleetResponse.FanNo = addFleetRequestModel.FanNo;
            getFleetResponse.VehicleLimit = addFleetRequestModel.VehicleLimit;
            getFleetResponse.CategoryType = addFleetRequestModel.CategoryType;
            getFleetResponse.SubCategoryType = addFleetRequestModel.SubCategoryType;
            getFleetResponse.VehicleAgeCriteria = addFleetRequestModel.VehicleAgeCriteria;
            getFleetResponse.PerVehicleSanction = addFleetRequestModel.PerVehicleSanction;
            getFleetResponse.StampDuty = addFleetRequestModel.StampDuty;
            getFleetResponse.ProcessingFee = addFleetRequestModel.ProcessingFee;
            getFleetResponse.IsAgreementLetterApproved = addFleetRequestModel.IsAgreementLetterApproved;
            getFleetResponse.IsENachApproved = addFleetRequestModel.IsENachApproved;
            getFleetResponse.IsMNachApproved = addFleetRequestModel.IsMNachApproved;
            getFleetResponse.IsProvisionLetterApproved = addFleetRequestModel.IsProvisionLetterApproved;
            getFleetResponse.IsSanctionLetterApproved = addFleetRequestModel.IsSanctionLetterApproved;
            getFleetResponse.Comment = addFleetRequestModel.Comment;
        }

        return getFleetResponse;
    }

    public async Task<GetFleetResponse> GetByBPNumber(long BPNumber)
    {
        GetFleetRequestModel getFleetRequestModel = new GetFleetRequestModel();
        getFleetRequestModel.BPNumber = BPNumber;

        FleetResponseModel getFleetResponseModel = await _fleetRepository.GetFleet(getFleetRequestModel);

        GetFleetResponse getFleetResponse = new GetFleetResponse();
        if (getFleetResponseModel != null && getFleetResponseModel.FleetID > 0)
        {
            getFleetResponse.FleetID = getFleetResponseModel.FleetID;
            getFleetResponse.BPNumber = getFleetResponseModel.BPNumber;
            getFleetResponse.FanNo = getFleetResponseModel.FanNo;
            getFleetResponse.CategoryType = getFleetResponseModel.CategoryType;
            getFleetResponse.SubCategoryType = getFleetResponseModel.SubCategoryType;
            getFleetResponse.VehicleLimit = getFleetResponseModel.VehicleLimit;
            getFleetResponse.PerVehicleSanction = getFleetResponseModel.PerVehicleSanction;
            getFleetResponse.StampDuty = getFleetResponseModel.StampDuty;
            getFleetResponse.VehicleAgeCriteria = getFleetResponseModel.VehicleAgeCriteria;
            getFleetResponse.ProcessingFee = getFleetResponseModel.ProcessingFee;
            getFleetResponse.IsAgreementLetterApproved = getFleetResponseModel.IsAgreementLetterApproved;
            getFleetResponse.IsENachApproved = getFleetResponseModel.IsENachApproved;
            getFleetResponse.IsMNachApproved = getFleetResponseModel.IsMNachApproved;
            getFleetResponse.IsProvisionLetterApproved = getFleetResponseModel.IsProvisionLetterApproved;
            getFleetResponse.IsSanctionLetterApproved = getFleetResponseModel.IsSanctionLetterApproved;
            getFleetResponse.Comment = getFleetResponseModel.Comment;
            getFleetResponse.Amount = getFleetResponseModel.Amount;
            getFleetResponse.IRR = getFleetResponseModel.IRR;
            getFleetResponse.AIR = getFleetResponseModel.AIR;
            getFleetResponse.LoanAmount = getFleetResponseModel.LoanAmount;
            getFleetResponse.ProcessingFeeAmount = getFleetResponseModel.ProcessingFeeAmount;
            getFleetResponse.StampDutyAmount = getFleetResponseModel.StampDutyAmount;
            getFleetResponse.AdditionalInformation = getFleetResponseModel.AdditionalInformation;
            getFleetResponse.DepartmentType = getFleetResponseModel.DepartmentType;
            getFleetResponse.AgreementDate = getFleetResponseModel.AgreementDate;
            getFleetResponse.RequestedIRR = getFleetResponseModel.RequestedIRR;
            getFleetResponse.RequestedProcessingFees = getFleetResponseModel.RequestedProcessingFees;
            getFleetResponse.NewIRR = getFleetResponseModel.NewIRR;
            getFleetResponse.NewAIR = getFleetResponseModel.NewAIR;
            getFleetResponse.NewProcessing = getFleetResponseModel.NewProcessing;
            getFleetResponse.RequestedAIR = getFleetResponseModel.RequestedAIR;
            getFleetResponse.AssignedTo = getFleetResponseModel.AssignedTo;
            getFleetResponse.AssignedToRoleId = getFleetResponseModel.AssignedToRoleId;
            getFleetResponse.AgentId = getFleetResponseModel.AgentId;
            getFleetResponse.AdminId = getFleetResponseModel.AdminId;
            getFleetResponse.CreditId = getFleetResponseModel.CreditId;
            getFleetResponse.CpcFiId = getFleetResponseModel.CpcFiId;
            getFleetResponse.CpcTlFiId = getFleetResponseModel.CpcTlFiId;
            getFleetResponse.CpcFcId = getFleetResponseModel.CpcFcId;
            getFleetResponse.CpcTlFcId = getFleetResponseModel.CpcTlFcId;
            getFleetResponse.IsAddressChanged = getFleetResponseModel.IsAddressChanged;

            IEnumerable<GetFleetVehicleByFleetIDResponse> getFleetVehicleByFleetIDResponses = await _fleetVehicleManager.GetFleetVehicleByFleetID(getFleetResponseModel.FleetID);
            List<GetFleetVehicleResponse> getFleetVehicleResponses = new List<GetFleetVehicleResponse>();

            foreach (GetFleetVehicleByFleetIDResponse getFleetVehicleByFleetIDResponse in getFleetVehicleByFleetIDResponses)
            {
                GetFleetVehicleResponse getFleetVehicleResponse = new GetFleetVehicleResponse();
                getFleetVehicleResponse.FleetID = getFleetVehicleByFleetIDResponse.FleetID;
                getFleetVehicleResponse.VehicleID = getFleetVehicleByFleetIDResponse.VehicleID;
                getFleetVehicleResponse.RCNo = getFleetVehicleByFleetIDResponse.RCNo;
                getFleetVehicleResponses.Add(getFleetVehicleResponse);
            }

            getFleetResponse.VehicleList = getFleetVehicleResponses;
        }

        return getFleetResponse;
    }

    public async Task<VerifyFleetResponse> Verify(long fleetId)
    {
        VerifyFleetResponse verifyFleetResponse = await GetFleetByFleetId(fleetId);
        if (verifyFleetResponse != null && verifyFleetResponse.FleetID != 0)
        {
            CustomerResponse customerResponse = await _customerManager.GetCustomerByBPNumber(verifyFleetResponse.BPNumber);
            bool isUpdate = false;
            if (customerResponse != null && verifyFleetResponse.FleetVehicles.Count > 0)
            {
                //List<InstaVeritaResponse> instaVeritaResponses = new List<InstaVeritaResponse>();
                foreach (var vehicle in verifyFleetResponse.FleetVehicles)
                {
                    if (!vehicle.IsSubmitted)
                    {
                        isUpdate = true;
                        InstaVeritaResponse instaVeritaResponse = await _fleetVehicleManager.InstaVeritaData(vehicle.RCNo);

                        if (instaVeritaResponse != null && !string.IsNullOrEmpty(instaVeritaResponse.ChassisNumber))
                        {
                            await InsertInstaVeritaLogs(vehicle.VehicleID, instaVeritaResponse);
                        }
                        string customerFullName = customerResponse.FirstName + (string.IsNullOrEmpty(customerResponse.MiddleName) ? "" : " " + customerResponse.MiddleName) + (string.IsNullOrEmpty(customerResponse.LastName) ? "" : " " + customerResponse.LastName);
                        customerFullName = customerFullName.Replace(".", "").Replace(" ", "");
                        vehicle.IsApproved = false;
                        if (customerFullName.ToLower() == instaVeritaResponse?.OwnersName.Replace(".", "").Replace(" ", "").ToLower())
                        {
                            vehicle.IsApproved = true;
                        }
                        else
                        {
                            vehicle.Reject_Reason = "Owner Name of vehicle " + vehicle.RCNo + " did not match.";
                        }
                        if (vehicle.IsApproved && instaVeritaResponse.RegistrationDate != null)
                        {
                            DateTime zeroTime = new DateTime(1, 1, 1);
                            var diff = (DateTime.Now - instaVeritaResponse.RegistrationDate).Value;
                            int years = (zeroTime + diff).Year - 1;
                            if (years > verifyFleetResponse.VehicleAgeCriteria)
                            {
                                vehicle.IsApproved = false;
                                vehicle.Reject_Reason = "Vehicle Age of vehicle " + vehicle.RCNo + " exceeds loan criteria.";
                            }
                        }
                        if (vehicle.IsApproved && !string.IsNullOrEmpty(instaVeritaResponse.VehicleModel))
                        {
                            string VehicleType = await _fleetRepository.GetVehicleType(instaVeritaResponse.VehicleModel);

                            if (string.IsNullOrEmpty(VehicleType) || VehicleType.ToLower() != "mhcv")
                            {
                                vehicle.IsApproved = false;
                                vehicle.Reject_Reason = "Vehicle Type of " + vehicle.RCNo + " is not MHCV";
                            }
                        }

                        //instaVeritaResponses.Add(instaVeritaResponse);
                        vehicle.IsSubmitted = true;
                        vehicle.ChassisNo = instaVeritaResponse.ChassisNumber;
                        vehicle.EngineNo = instaVeritaResponse.EngineNumber;
                        vehicle.ExpiryDate = instaVeritaResponse.ExpiryDate;
                        vehicle.IsBlacklisted = instaVeritaResponse.Blacklisted ?? false;
                        vehicle.OwnerName = instaVeritaResponse.OwnersName;
                        vehicle.RegistrationDate = instaVeritaResponse.RegistrationDate;
                        vehicle.VehicleCompany = instaVeritaResponse.VehicleCompany;
                        vehicle.VehicleType = instaVeritaResponse.VehicleType;
                        vehicle.VehicleModel = instaVeritaResponse.VehicleModel;
                        if (instaVeritaResponse.BlacklistedDetails!.Count > 0)
                        {
                            vehicle.FirNumber = instaVeritaResponse.BlacklistedDetails[0].FirNumber;
                            vehicle.FirDate = instaVeritaResponse.BlacklistedDetails[0].FirDate;
                            vehicle.BlackListedReason = instaVeritaResponse.BlacklistedDetails[0].BlacklistedReason;
                        }
                    }
                }
            }
            if (isUpdate)
            {
                verifyFleetResponse.FleetVehicles = await _fleetVehicleManager.BulkUpdateFleetDetails(verifyFleetResponse.FleetVehicles);
            }
        }

        return verifyFleetResponse;
    }

    public async Task<VerifyFleetResponse> GetFleetByFleetId(long fleetId)
    {
        VerifyFleetResponseModel fleetResponseModel = await _fleetRepository.GetFleetDetailByFleetId(fleetId);

        VerifyFleetResponse verifyFleetResponse = new VerifyFleetResponse();
        if (verifyFleetResponse != null)
        {
            verifyFleetResponse.FleetID = fleetResponseModel.FleetID;
            verifyFleetResponse.BPNumber = fleetResponseModel.BPNumber;
            verifyFleetResponse.FanNo = fleetResponseModel.FanNo;
            verifyFleetResponse.VehicleLimit = fleetResponseModel.VehicleLimit;
            verifyFleetResponse.CategoryType = fleetResponseModel.CategoryType;
            verifyFleetResponse.SubCategoryType = fleetResponseModel.SubCategoryType;
            verifyFleetResponse.PerVehicleSanction = fleetResponseModel.PerVehicleSanction;
            verifyFleetResponse.StampDuty = fleetResponseModel.StampDuty;
            verifyFleetResponse.VehicleAgeCriteria = fleetResponseModel.VehicleAgeCriteria;
            verifyFleetResponse.ProcessingFee = fleetResponseModel.ProcessingFee;
            verifyFleetResponse.IsProvisionLetterApproved = fleetResponseModel.IsProvisionLetterApproved;
            verifyFleetResponse.IsSanctionLetterApproved = fleetResponseModel.IsSanctionLetterApproved;
            verifyFleetResponse.IsAgreementLetterApproved = fleetResponseModel.IsAgreementLetterApproved;
            verifyFleetResponse.IsENachApproved = fleetResponseModel.IsENachApproved;
            verifyFleetResponse.IsMNachApproved = fleetResponseModel.IsMNachApproved;
            verifyFleetResponse.Amount = fleetResponseModel.Amount;
            verifyFleetResponse.IRR = fleetResponseModel.IRR;
            verifyFleetResponse.AIR = fleetResponseModel.AIR;
            verifyFleetResponse.LoanAmount = fleetResponseModel.LoanAmount;
            verifyFleetResponse.ProcessingFeeAmount = fleetResponseModel.ProcessingFeeAmount;
            verifyFleetResponse.StampDutyAmount = fleetResponseModel.StampDutyAmount;
            verifyFleetResponse.Comment = fleetResponseModel.Comment;
            verifyFleetResponse.AdditionalInformation = fleetResponseModel.AdditionalInformation;
            verifyFleetResponse.DepartmentType = fleetResponseModel.DepartmentType;
            verifyFleetResponse.AgreementDate = fleetResponseModel.AgreementDate;
            verifyFleetResponse.RequestedIRR = fleetResponseModel.RequestedIRR;
            verifyFleetResponse.RequestedProcessingFees = fleetResponseModel.RequestedProcessingFees;
            verifyFleetResponse.NewIRR = fleetResponseModel.NewIRR;
            verifyFleetResponse.NewAIR = fleetResponseModel.NewAIR;
            verifyFleetResponse.NewProcessing = fleetResponseModel.NewProcessing;
            verifyFleetResponse.RequestedAIR = fleetResponseModel.RequestedAIR;
            verifyFleetResponse.AssignedTo = fleetResponseModel.AssignedTo;
            verifyFleetResponse.AssignedToRoleId = fleetResponseModel.AssignedToRoleId;
            verifyFleetResponse.AgentId = fleetResponseModel.AgentId;
            verifyFleetResponse.AdminId = fleetResponseModel.AdminId;
            verifyFleetResponse.CreditId = fleetResponseModel.CreditId;
            verifyFleetResponse.CpcFiId = fleetResponseModel.CpcFiId;
            verifyFleetResponse.CpcTlFiId = fleetResponseModel.CpcTlFiId;
            verifyFleetResponse.CpcFcId = fleetResponseModel.CpcFcId;
            verifyFleetResponse.CpcTlFcId = fleetResponseModel.CpcTlFcId;
            verifyFleetResponse.IsAddressChanged = fleetResponseModel.IsAddressChanged;
            verifyFleetResponse.AccountNumber = fleetResponseModel.AccountNumber;
            verifyFleetResponse.IFSCCode = fleetResponseModel.IFSCCode;
            verifyFleetResponse.ApplicantName = fleetResponseModel.ApplicantName;
            verifyFleetResponse.FleetVehicles = new List<VerifyFleetVehicleResponse>();

            if (fleetResponseModel.FleetVehicles.Count > 0)
            {
                foreach (var fleetVehicle in fleetResponseModel.FleetVehicles)
                {
                    VerifyFleetVehicleResponse verifyFleetVehicleResponse = new VerifyFleetVehicleResponse();
                    verifyFleetVehicleResponse.VehicleID = fleetVehicle.VehicleID;
                    verifyFleetVehicleResponse.FleetID = fleetVehicle.FleetID;
                    verifyFleetVehicleResponse.RCNo = fleetVehicle.RCNo;
                    verifyFleetVehicleResponse.IsSubmitted = fleetVehicle.IsSubmitted;
                    verifyFleetVehicleResponse.IsApproved = fleetVehicle.IsApproved;
                    verifyFleetVehicleResponse.Reject_Reason = fleetVehicle.Reject_Reason;
                    verifyFleetVehicleResponse.IsActive = fleetVehicle.IsActive;
                    verifyFleetVehicleResponse.IsCallCenterApproved = fleetVehicle.IsCallCenterApproved;
                    verifyFleetVehicleResponse.AgentRemark = fleetVehicle.AgentRemark;
                    verifyFleetVehicleResponse.IsAdminApproved = fleetVehicle.IsAdminApproved;
                    verifyFleetVehicleResponse.AdminRemark = fleetVehicle.AdminRemark;
                    verifyFleetVehicleResponse.RegistrationDate = fleetVehicle.RegistrationDate;
                    verifyFleetVehicleResponse.ExpiryDate = fleetVehicle.ExpiryDate;
                    verifyFleetVehicleResponse.VehicleType = fleetVehicle.VehicleType;
                    verifyFleetVehicleResponse.ChassisNo = fleetVehicle.ChassisNo;
                    verifyFleetVehicleResponse.EngineNo = fleetVehicle.EngineNo;
                    verifyFleetVehicleResponse.VehicleCompany = fleetVehicle.VehicleCompany;
                    verifyFleetVehicleResponse.VehicleModel = fleetVehicle.VehicleModel;
                    verifyFleetVehicleResponse.OwnerName = fleetVehicle.OwnerName;
                    verifyFleetVehicleResponse.FirNumber = fleetVehicle.FirNumber;
                    verifyFleetVehicleResponse.FirDate = fleetVehicle.FirDate;
                    verifyFleetVehicleResponse.IsBlacklisted = fleetVehicle.IsBlacklisted;
                    verifyFleetVehicleResponse.BlackListedReason = fleetVehicle.BlackListedReason;
                    verifyFleetVehicleResponse.Comment = fleetVehicle.Comment;

                    verifyFleetResponse.FleetVehicles.Add(verifyFleetVehicleResponse);
                }
            }
        }

        return verifyFleetResponse;
    }

    private async Task InsertInstaVeritaLogs(long VehicleID, InstaVeritaResponse instaVeritaResponse)
    {
        InstaVeritaLogRequest instaVeritaLogRequest = new InstaVeritaLogRequest();
        instaVeritaLogRequest.VehicleID = VehicleID;
        instaVeritaLogRequest.Blacklisted = instaVeritaResponse.Blacklisted;
        instaVeritaLogRequest.BlacklistedReason = instaVeritaResponse.BlacklistedReason;
        instaVeritaLogRequest.ChassisNumber = instaVeritaResponse.ChassisNumber;
        instaVeritaLogRequest.EngineNumber = instaVeritaResponse.EngineNumber;
        instaVeritaLogRequest.ExpiryDate = instaVeritaResponse.ExpiryDate;
        instaVeritaLogRequest.FitnessCertificateExpiryDate = instaVeritaResponse.FitnessCertificateExpiryDate;
        instaVeritaLogRequest.FinancingAuthority = instaVeritaResponse.FinancingAuthority;
        instaVeritaLogRequest.FuelType = instaVeritaResponse.FuelType;
        instaVeritaLogRequest.MVTaxPaidUpto = instaVeritaResponse.MVTaxPaidUpto;
        instaVeritaLogRequest.MVTaxStructure = instaVeritaResponse.MVTaxStructure;
        instaVeritaLogRequest.OwnersName = instaVeritaResponse.OwnersName;
        instaVeritaLogRequest.OwnerSerialNumber = instaVeritaResponse.OwnerSerialNumber;
        instaVeritaLogRequest.PuccUpto = instaVeritaResponse.PuccUpto;
        instaVeritaLogRequest.RegistrationNumber = instaVeritaResponse.RegistrationNumber;
        instaVeritaLogRequest.RegistrationDate = instaVeritaResponse.RegistrationDate;
        instaVeritaLogRequest.RegisteringAuthority = instaVeritaResponse.RegisteringAuthority;
        instaVeritaLogRequest.RegistrationState = instaVeritaResponse.RegistrationState;
        instaVeritaLogRequest.VehicleCompany = instaVeritaResponse.VehicleCompany;
        instaVeritaLogRequest.VehicleModel = instaVeritaResponse.VehicleModel;
        instaVeritaLogRequest.VehicleType = instaVeritaResponse.VehicleType;
        instaVeritaLogRequest.VehicleAge = instaVeritaResponse.VehicleAge;

        InstaVeritaLogResponse instaVeritaLogResponse = await _fleetVehicleManager.InsertInstaVeritaDetails(instaVeritaLogRequest);

        if (instaVeritaLogResponse.Log_Id != 0 && instaVeritaResponse.BlacklistedDetails.Count > 0)
        {
            BlackListedDetailsRequest blackListedDetailsRequest = new BlackListedDetailsRequest();
            blackListedDetailsRequest.InstaLogId = instaVeritaLogResponse.Log_Id;
            blackListedDetailsRequest.RegistrationState = instaVeritaResponse.BlacklistedDetails[0].RegistrationState;
            blackListedDetailsRequest.RegisteringAuthority = instaVeritaResponse.BlacklistedDetails[0].RegisteringAuthority;
            blackListedDetailsRequest.RcNumber = instaVeritaResponse.BlacklistedDetails[0].RcNumber;
            blackListedDetailsRequest.FirNumber = instaVeritaResponse.BlacklistedDetails[0].FirNumber;
            blackListedDetailsRequest.FirDate = instaVeritaResponse.BlacklistedDetails[0].FirDate;
            blackListedDetailsRequest.BlacklistedReason = instaVeritaResponse.BlacklistedDetails[0].BlacklistedReason;
            blackListedDetailsRequest.BlacklistedDate = instaVeritaResponse.BlacklistedDetails[0].BlacklistedDate;

            BlackListedDetailsResponse blackListedDetailsResponse = await _fleetVehicleManager.InsertInstaVeritaBlackListedDetails(blackListedDetailsRequest);
        }
    }

    public async Task<ProvisionApprovalResponse> ProvisionApproval(long FleetID, ProvisionApprovalRequest provisionApprovalRequest)
    {
        ProvisionApprovalRequestModel provisionApprovalRequestModel = new ProvisionApprovalRequestModel();
        provisionApprovalRequestModel.FleetID = FleetID;
        provisionApprovalRequestModel.IsApproved = provisionApprovalRequest.IsApproved;
        provisionApprovalRequestModel.UpdatedBy = 41;
        provisionApprovalRequestModel.UpdatedDate = DateTime.Now;

        ProvisionApprovalResponseModel provisionApprovalResponseModel = await _fleetRepository.ProvisionApproval(provisionApprovalRequestModel);

        ProvisionApprovalResponse provisionApprovalResponse = new ProvisionApprovalResponse();
        if (provisionApprovalResponseModel.FleetID == 0)
        {
            provisionApprovalResponse.Message = "Update Failed";
        }
        else
        {
            provisionApprovalResponse.Message = "Updated Successfully";
        }
        return provisionApprovalResponse;
    }

    public async Task<SanctionApprovalResponse> SanctionApproval(long FleetID, SanctionApprovalRequest sanctionApprovalRequest)
    {
        SanctionApprovalRequestModel sanctionApprovalRequestModel = new SanctionApprovalRequestModel();
        sanctionApprovalRequestModel.FleetID = FleetID;
        sanctionApprovalRequestModel.IsApproved = sanctionApprovalRequest.IsApproved;
        sanctionApprovalRequestModel.UpdatedBy = 41;
        sanctionApprovalRequestModel.UpdatedDate = DateTime.Now;

        SanctionApprovalResponseModel sanctionApprovalResponseModel = await _fleetRepository.SanctionApproval(sanctionApprovalRequestModel);

        SanctionApprovalResponse sanctionApprovalResponse = new SanctionApprovalResponse();
        if (sanctionApprovalResponseModel.FleetID == 0)
        {
            sanctionApprovalResponse.Message = "Update Failed";
        }
        else
        {
            sanctionApprovalResponse.Message = "Updated Successfully";
        }
        return sanctionApprovalResponse;
    }

    public async Task<EAgreementApprovalResponse> EAgreementApproval(long FleetID, EAgreementApprovalRequest eAgreementApprovalRequest)
    {
        EAgreementApprovalRequestModel eAgreementApprovalRequestModel = new EAgreementApprovalRequestModel();
        eAgreementApprovalRequestModel.FleetID = FleetID;
        eAgreementApprovalRequestModel.IsApproved = eAgreementApprovalRequest.IsApproved;
        eAgreementApprovalRequestModel.UpdatedBy = 41;
        eAgreementApprovalRequestModel.UpdatedDate = DateTime.Now;

        EAgreementApprovalResponseModel eAgreementApprovalResponseModel = await _fleetRepository.EAgreementApproval(eAgreementApprovalRequestModel);

        EAgreementApprovalResponse eAgreementApprovalResponse = new EAgreementApprovalResponse();
        if (eAgreementApprovalResponseModel.FleetID == 0)
        {
            eAgreementApprovalResponse.Message = "Update Failed";
        }
        else
        {
            eAgreementApprovalResponse.Message = "Updated Successfully";
        }
        return eAgreementApprovalResponse;
    }

    public async Task<UpdateFleetAmountResponse> UpdateFleetAmount(UpdateFleetAmountRequest updateFleetAmountRequest)
    {
        UpdateFleetAmountRequestModel updateFleetAmountRequestModel = new UpdateFleetAmountRequestModel();
        updateFleetAmountRequestModel.FleetID = updateFleetAmountRequest.FleetID;
        updateFleetAmountRequestModel.Amount = updateFleetAmountRequest.Amount;
        updateFleetAmountRequestModel.LoanAmount = updateFleetAmountRequest.LoanAmount;
        updateFleetAmountRequestModel.ProcessingFeeAmount = updateFleetAmountRequest.ProcessingFeeAmount;
        updateFleetAmountRequestModel.StampDutyAmount = updateFleetAmountRequest.StampDutyAmount;
        updateFleetAmountRequestModel.UpdatedBy = 41;
        updateFleetAmountRequestModel.UpdatedDate = DateTime.Now;

        UpdateFleetAmountResponseModel updateFleetAmountResponseModel = await _fleetRepository.UpdateFleetAmount(updateFleetAmountRequestModel);

        UpdateFleetAmountResponse updateFleetAmountResponse = new UpdateFleetAmountResponse();
        updateFleetAmountResponse.FleetID = updateFleetAmountRequestModel.FleetID;

        return updateFleetAmountResponse;
    }

    public async Task<UpdateFleetFanNoResponse> UpdateFleetFanNo(UpdateFleetFanNoRequest updateFleetFanNoRequest)
    {
        UpdateFleetFanNoRequestModel updateFleetFanNoRequestModel = new UpdateFleetFanNoRequestModel();
        updateFleetFanNoRequestModel.FleetID = updateFleetFanNoRequest.FleetID;
        updateFleetFanNoRequestModel.FanNo = updateFleetFanNoRequest.FanNo;
        updateFleetFanNoRequestModel.UpdatedBy = 41;
        updateFleetFanNoRequestModel.UpdatedDate = DateTime.Now;

        UpdateFleetFanNoResponseModel updateFleetFanNoResponseModel = await _fleetRepository.UpdateFleetFanNo(updateFleetFanNoRequestModel);

        UpdateFleetFanNoResponse updateFleetFanNoResponse = new UpdateFleetFanNoResponse();
        updateFleetFanNoResponse.FleetID = updateFleetFanNoResponseModel.FleetID;

        return updateFleetFanNoResponse;
    }

    public async Task<LetterMasterDataResponse> LetterMasterData(long FleetID)
    {
        LetterMasterDataResponseModel letterMasterDataResponseModel = await _fleetRepository.LetterMasterData(FleetID);

        LetterMasterDataResponse letterMasterDataResponse = new LetterMasterDataResponse();

        if (letterMasterDataResponseModel != null)
        {
            letterMasterDataResponse.FanNo = letterMasterDataResponseModel.FanNo;
            letterMasterDataResponse.BorrowerAuthorisedPersonName = letterMasterDataResponseModel.BorrowerName;
            letterMasterDataResponse.AgreementDate = letterMasterDataResponseModel.AgreementDate;
            letterMasterDataResponse.AgreementPlace = letterMasterDataResponseModel.AgreementPlace;
            letterMasterDataResponse.FileAccountNumber = 1;
            letterMasterDataResponse.OfficeOrbranchAddress = letterMasterDataResponseModel.OfficeOrbranchAddress;
            letterMasterDataResponse.BorrowerName = letterMasterDataResponseModel.BorrowerName;
            letterMasterDataResponse.BorrowerConstitution = "Individual";
            letterMasterDataResponse.BorrowerAddressLine1 = letterMasterDataResponseModel.BorrowerAddressLine1;
            letterMasterDataResponse.BorrowerAddressLine2 = letterMasterDataResponseModel.BorrowerAddressLine2;
            letterMasterDataResponse.BorrowerAddressLine3 = letterMasterDataResponseModel.BorrowerAddressLine3;
            letterMasterDataResponse.BorrowerMobileNumber = letterMasterDataResponseModel.BorrowerMobileNumber;
            letterMasterDataResponse.BorrowerEmailID = letterMasterDataResponseModel.BorrowerEmailID;
            letterMasterDataResponse.TotalAmountofLoan = letterMasterDataResponseModel.TotalAmountofLoan;
            letterMasterDataResponse.Limit = letterMasterDataResponseModel.TotalAmountofLoan * 1;
            letterMasterDataResponse.CutOffLimit = letterMasterDataResponseModel.TotalAmountofLoan * 1;
            letterMasterDataResponse.InterestRate = letterMasterDataResponseModel.InterestRate;
            letterMasterDataResponse.TypeofInterest = "Fixed";
            letterMasterDataResponse.AcceleratedInterest = letterMasterDataResponseModel.AcceleratedInterest;
            letterMasterDataResponse.PurposeoftheLoan = "Lubricant (Diesel)";
            letterMasterDataResponse.AvailabilityPeriod = "12 months (1 year)";
            letterMasterDataResponse.LegalExpenses = "As per actuals";
            letterMasterDataResponse.ServiceCharges = "As per actuals";
            letterMasterDataResponse.ChequeBouncingCharges = 415;
            letterMasterDataResponse.RetainerCharges = "As per actuals";
            letterMasterDataResponse.ProcessingFees = letterMasterDataResponseModel.ProcessingFees;
            letterMasterDataResponse.StampDuty = letterMasterDataResponseModel.StampDuty;
            letterMasterDataResponse.Cli = "Not Applicable";
            letterMasterDataResponse.Aetna = "Not Applicable";
            letterMasterDataResponse.OtherCharges = "As per actuals";
        }

        return letterMasterDataResponse;
    }

    public async Task<CommentResponse> AddComment(CommentRequest commentRequest)
    {
        CommentResponse commentResponse = new CommentResponse();

        CommentRequestModel commentRequestModel = new CommentRequestModel();
        commentRequestModel.FleetID = commentRequest.FleetID;
        commentRequestModel.Comment = commentRequest.Comment;
        commentRequestModel.CreatedBy = 41;
        commentRequestModel.CreatedUserType = "AGENT";
        commentRequestModel.CreatedDate = DateTime.Now;

        CommentResponseModel commentResponseModel = await _fleetRepository.AddComment(commentRequestModel);

        commentResponse.CommentId = commentResponseModel.CommentId;
        commentResponse.Message = commentResponseModel.ErrorMessage;

        DocumentTypeMstrResponseModel documentTypeMstrResponseModel = await _documentTypeMstrManager.GetDocumentTypeMstrByDocumentCode(DocumentCodeFlag.OTHER);

        if (commentResponse.CommentId != 0)
        {
            int i = 0;
            foreach (var doc in commentRequest.Documents)
            {
                UploadDocumentsRequest uploadDocumentsRequest = new UploadDocumentsRequest()
                {
                    EntityCode = EntityCode.COMMENT.ToString(),
                    EntityId = commentResponse.CommentId,
                    FleetId = commentRequest.FleetID,
                    Extension = doc.Extension,
                    StageId = (int)commentResponseModel.StageId,
                    DocTypeId = documentTypeMstrResponseModel.DocTypeId,
                    DocumentName = "Comment_" + commentResponseModel.CommentId + "_" + i,
                    DocumentData = Convert.FromBase64String(doc.Data)
                };

                UploadDocumentsResponse uploadDocumentsResponse = await _uploadManager.UploadDocuments(uploadDocumentsRequest, 1);

                i++;
            }
        }

        return commentResponse;
    }

    public async Task<AdditionalInformationResponse> AddAdditionalInformation(AdditionalInformationRequest additionalInformationRequest)
    {
        AdditionalInformationResponse additionalInformationResponse = new AdditionalInformationResponse();

        AdditionalInformationRequestModel additionalInformationRequestModel = new AdditionalInformationRequestModel();
        additionalInformationRequestModel.FleetID = additionalInformationRequest.FleetID;
        additionalInformationRequestModel.DepartmentType = additionalInformationRequest.DepartmentType;
        additionalInformationRequestModel.AdditionalInformation = additionalInformationRequest.AdditionalInformation;
        additionalInformationRequestModel.CreatedBy = 41;
        additionalInformationRequestModel.CreatedUserType = "AGENT";
        additionalInformationRequestModel.CreatedDate = DateTime.Now;

        AdditionalInformationResponseModel additionalInformationResponseModel = await _fleetRepository.AddAdditionalInformation(additionalInformationRequestModel);

        additionalInformationResponse.AdditionalInfoId = additionalInformationResponseModel.AdditionalInfoId;
        additionalInformationResponse.Message = additionalInformationResponseModel.ErrorMessage;

        DocumentTypeMstrResponseModel documentTypeMstrResponseModel = await _documentTypeMstrManager.GetDocumentTypeMstrByDocumentCode(DocumentCodeFlag.OTHER);

        if (additionalInformationResponse.AdditionalInfoId != 0)
        {
            int i = 0;
            foreach(var doc in additionalInformationRequest.Documents)
            {
                UploadDocumentsRequest uploadDocumentsRequest = new UploadDocumentsRequest() {
                    EntityCode = EntityCode.ADDINFO.ToString(),
                    EntityId = additionalInformationResponse.AdditionalInfoId,
                    FleetId = additionalInformationRequest.FleetID,
                    Extension = doc.Extension,
                    StageId = (int)additionalInformationResponseModel.StageId,
                    DocTypeId = documentTypeMstrResponseModel.DocTypeId,
                    DocumentName = "AdditionalInfo_" + additionalInformationResponse.AdditionalInfoId + "_" + i,
                    DocumentData = Convert.FromBase64String(doc.Data)
                };

                UploadDocumentsResponse uploadDocumentsResponse = await _uploadManager.UploadDocuments(uploadDocumentsRequest, 1);

                i++;
            }
        }

        return additionalInformationResponse;
    }

    public async Task<AddressChangeResponse> AddressChange(long FleetID, AddressChangeRequest addressChangeRequest)
    {
        AddressChangeResponse addressChangeResponse = new AddressChangeResponse();

        AddressChangeRequestModel addressChangeRequestModel = new AddressChangeRequestModel();
        addressChangeRequestModel.FleetID = FleetID;
        addressChangeRequestModel.IsAddressChange = addressChangeRequest.IsAddressChange;
        addressChangeRequestModel.UpdatedBy = 41;
        addressChangeRequestModel.UpdatedDate = DateTime.Now;

        AddressChangeResponseModel addressChangeResponseModel = await _fleetRepository.AddressChange(addressChangeRequestModel);

        if (addressChangeResponseModel.FleetID == 0)
        {
            addressChangeResponse.Message = "Update Failed";
        }
        else
        {
            addressChangeResponse.Message = "Updated Successfully";
            VerifyFleetResponse verifyFleetResponse = await GetFleetByFleetId(FleetID);
            if (verifyFleetResponse != null && string.IsNullOrEmpty(verifyFleetResponse.FanNo))
            {
                GenerateFanNoRequest generateFanNoRequest = new GenerateFanNoRequest();
                generateFanNoRequest.BranchCode = verifyFleetResponse.IFSCCode;
                generateFanNoRequest.ProcessType = "TMFD";
                generateFanNoRequest.SchemeName = addressChangeRequest.IsAddressChange ? "RCUFL" : "NRCUFL";
                generateFanNoRequest.LoanType = "autoCV";
                generateFanNoRequest.ApplicantName = verifyFleetResponse.ApplicantName;
                generateFanNoRequest.BdmName = "";
                generateFanNoRequest.DsaName = "";
                generateFanNoRequest.DealerName = "DIRECT";
                generateFanNoRequest.DealerCode = "DIRECT";

                GenerateFanNoResponse generateFanNoResponse = await _dMSManager.GenerateFanNo(generateFanNoRequest);
                string FanNo = generateFanNoResponse.FanNo;

                UpdateFleetFanNoRequest updateFleetFanNoRequest = new UpdateFleetFanNoRequest();
                updateFleetFanNoRequest.FleetID = FleetID;
                updateFleetFanNoRequest.FanNo = FanNo;
                UpdateFleetFanNoResponse updateFleetFanNoResponse = await UpdateFleetFanNo(updateFleetFanNoRequest);
            }
        }

        return addressChangeResponse;
    }

    public async Task<List<GetDepartmentListResponse>> GetDepartmentList(GetDepartmentListRequest getDepartmentListRequest)
    {
        List<GetDepartmentListResponse> getDepartmentListResponses = new List<GetDepartmentListResponse>();

        GetDepartmentListRequestModel getDepartmentListRequestModel = new GetDepartmentListRequestModel()
        {
            FleetId = getDepartmentListRequest.FleetId,
            Role = getDepartmentListRequest.Role
        };

        List<GetDepartmentListResponseModel> getDepartmentListResponseModels = await _fleetRepository.GetDepartmentLists(getDepartmentListRequestModel);

        foreach(var dept in getDepartmentListResponseModels)
        {
            GetDepartmentListResponse getDepartmentListResponse = new GetDepartmentListResponse();
            getDepartmentListResponse.DepartmentCode = dept.DepartmentCode;
            getDepartmentListResponse.DepartmentName = dept.DepartmentName;

            getDepartmentListResponses.Add(getDepartmentListResponse);
        }

        return getDepartmentListResponses;
    }

    public async Task<List<GetAdditionalInfoResponse>> GetAdditionalInfo(GetAdditionalInfoRequest getAdditionalInfoRequest)
    {
        List<GetAdditionalInfoResponse> getAdditionalInfoResponses = new List<GetAdditionalInfoResponse>();

        GetAdditionalInfoRequestModel getAdditionalInfoRequestModel = new GetAdditionalInfoRequestModel();
        getAdditionalInfoRequestModel.FleetId = getAdditionalInfoRequest.FleetId;
        getAdditionalInfoRequestModel.DepartmentCode = getAdditionalInfoRequest.DepartmentCode;

        List<GetAdditionalInfoResponseModel> getAdditionalInfoResponseModels = await _fleetRepository.GetAdditionalInfos(getAdditionalInfoRequestModel);

        foreach(var addInfo in getAdditionalInfoResponseModels)
        {
            GetAdditionalInfoResponseModel getAdditionalInfoResponseModel = new GetAdditionalInfoResponseModel();
            getAdditionalInfoResponseModel.AdditionalInfoId = addInfo.AdditionalInfoId;
            getAdditionalInfoResponseModel.Comment = addInfo.Comment;
            getAdditionalInfoResponseModel.RoleName = addInfo.RoleName;
            getAdditionalInfoResponseModel.DepartmentCode = addInfo.DepartmentCode;
            getAdditionalInfoResponseModel.StageCode = addInfo.StageCode;
            getAdditionalInfoResponseModel.FleetID = addInfo.FleetID;
            getAdditionalInfoResponseModel.AssignedTo = addInfo.AssignedTo;

            getAdditionalInfoResponseModels.Add(getAdditionalInfoResponseModel);
        }

        return getAdditionalInfoResponses;
    }
}
