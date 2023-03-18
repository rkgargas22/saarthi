using Tmf.Saarthi.Core.RequestModels.FleetVehicle;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.FleetVehicle;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;
using Tmf.Saarthi.Infrastructure.Models.Response.FleetVehicle;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class FleetVehicleManager : IFleetVehicleManager
{
    private readonly IFleetVehicleRepository _fleetVehicleRepository;
    public FleetVehicleManager(IFleetVehicleRepository fleetVehicleRepository)
    {
        _fleetVehicleRepository = fleetVehicleRepository;
    }

    public async Task<AddFleetVehicleResponse> AddFleetVehicle(AddFleetVehicleRequest addFleetVehicleRequest)
    {
        AddFleetVehicleRequestModel addFleetVehicleRequestModel = new AddFleetVehicleRequestModel();
        addFleetVehicleRequestModel.FleetID = addFleetVehicleRequest.FleetID;
        addFleetVehicleRequestModel.RCNo = addFleetVehicleRequest.RCNo;
        addFleetVehicleRequestModel.IsSubmitted = false;
        addFleetVehicleRequestModel.IsApproved = false;
        addFleetVehicleRequestModel.Reject_Reason = string.Empty;
        addFleetVehicleRequestModel.IsActive = true;
        addFleetVehicleRequestModel.IsCallCenterApproved = false;
        addFleetVehicleRequestModel.AgentRemark = string.Empty;
        addFleetVehicleRequestModel.IsAdminApproved = null;
        addFleetVehicleRequestModel.AdminRemark = string.Empty;
        addFleetVehicleRequestModel.RegistrationDate = null;
        addFleetVehicleRequestModel.ExpiryDate = null;
        addFleetVehicleRequestModel.VehicleType = string.Empty;
        addFleetVehicleRequestModel.ChassisNo = string.Empty;
        addFleetVehicleRequestModel.EngineNo = string.Empty;
        addFleetVehicleRequestModel.VehicleCompany = string.Empty;
        addFleetVehicleRequestModel.VehicleModel = string.Empty;
        addFleetVehicleRequestModel.OwnerName = string.Empty;
        addFleetVehicleRequestModel.FirNumber = string.Empty;
        addFleetVehicleRequestModel.FirDate = null;
        addFleetVehicleRequestModel.IsBlacklisted = false;
        addFleetVehicleRequestModel.BlackListedReason = string.Empty;
        addFleetVehicleRequestModel.CreatedBy = 83;
        addFleetVehicleRequestModel.CreatedDate = DateTime.Now;
        addFleetVehicleRequestModel.Comment = string.Empty;

        FleetVehicleResponseModel addFleetVehicleResponseModel = await _fleetVehicleRepository.AddFleetVehicle(addFleetVehicleRequestModel);

        AddFleetVehicleResponse addFleetVehicleResponse = new AddFleetVehicleResponse();
        addFleetVehicleResponse.VehicleID = addFleetVehicleResponseModel.VehicleID;
        addFleetVehicleResponse.FleetID = addFleetVehicleRequestModel.FleetID;
        addFleetVehicleResponse.RCNo = addFleetVehicleRequestModel.RCNo;
        addFleetVehicleResponse.ErrorMessage = addFleetVehicleResponseModel.ErrorMessage;

        return addFleetVehicleResponse;
    }

    public async Task<BulkAddFleetVehicleResponse> BulkAddFleetVehicle(long fleetId, BulkAddFleetVehicleRequest bulkAddFleetVehicleRequest)
    {
        List<AddFleetVehicleRequestModel> addFleetVehicleRequestModels = new List<AddFleetVehicleRequestModel>();
        foreach (string rcNo in bulkAddFleetVehicleRequest.RCNoList)
        {
            AddFleetVehicleRequestModel addFleetVehicleRequestModel = new AddFleetVehicleRequestModel();
            addFleetVehicleRequestModel.FleetID = fleetId;
            addFleetVehicleRequestModel.RCNo = rcNo;
            addFleetVehicleRequestModel.IsSubmitted = false;
            addFleetVehicleRequestModel.IsApproved = false;
            addFleetVehicleRequestModel.Reject_Reason = string.Empty;
            addFleetVehicleRequestModel.IsActive = true;
            addFleetVehicleRequestModel.IsCallCenterApproved = false;
            addFleetVehicleRequestModel.AgentRemark = string.Empty;
            addFleetVehicleRequestModel.IsAdminApproved = null;
            addFleetVehicleRequestModel.AdminRemark = string.Empty;
            addFleetVehicleRequestModel.RegistrationDate = null;
            addFleetVehicleRequestModel.ExpiryDate = null;
            addFleetVehicleRequestModel.VehicleType = string.Empty;
            addFleetVehicleRequestModel.ChassisNo = string.Empty;
            addFleetVehicleRequestModel.EngineNo = string.Empty;
            addFleetVehicleRequestModel.VehicleCompany = string.Empty;
            addFleetVehicleRequestModel.VehicleModel = string.Empty;
            addFleetVehicleRequestModel.OwnerName = string.Empty;
            addFleetVehicleRequestModel.FirNumber = string.Empty;
            addFleetVehicleRequestModel.FirDate = null;
            addFleetVehicleRequestModel.IsBlacklisted = false;
            addFleetVehicleRequestModel.BlackListedReason = string.Empty;
            addFleetVehicleRequestModel.CreatedBy = 83;
            addFleetVehicleRequestModel.CreatedDate = DateTime.Now;
            addFleetVehicleRequestModel.Comment = string.Empty;
            addFleetVehicleRequestModels.Add(addFleetVehicleRequestModel);
        }


        List<FleetVehicleResponseModel> addFleetVehicleResponseModelList = await _fleetVehicleRepository.BulkAddFleetVehicle(addFleetVehicleRequestModels);

        List<AddFleetVehicleResponse> addFleetVehicleResponseList = new List<AddFleetVehicleResponse>();
        BulkAddFleetVehicleResponse bulkAddFleetVehicleResponse = new BulkAddFleetVehicleResponse();
        foreach (FleetVehicleResponseModel fleetVehicleResponseModel in addFleetVehicleResponseModelList)
        {
            bulkAddFleetVehicleResponse.FleetID = fleetVehicleResponseModel.FleetID;
            AddFleetVehicleResponse addFleetVehicleResponse = new AddFleetVehicleResponse();
            addFleetVehicleResponse.VehicleID = fleetVehicleResponseModel.VehicleID;
            addFleetVehicleResponse.FleetID = fleetVehicleResponseModel.FleetID;
            addFleetVehicleResponse.RCNo = fleetVehicleResponseModel.RCNo;
            addFleetVehicleResponseList.Add(addFleetVehicleResponse);
        }

        bulkAddFleetVehicleResponse.Vehicles = addFleetVehicleResponseList;


        return bulkAddFleetVehicleResponse;
    }

    public async Task<List<VerifyFleetVehicleResponse>> BulkUpdateFleetDetails(List<VerifyFleetVehicleResponse> verifyFleetVehicleResponses)
    {
        List<VerifyFleetVehicleResponseModel> verifyFleetVehicleResponseModels = new List<VerifyFleetVehicleResponseModel>();
        foreach (var resp in verifyFleetVehicleResponses)
        {
            VerifyFleetVehicleResponseModel verifyFleetVehicleResponseModel = new VerifyFleetVehicleResponseModel();
            verifyFleetVehicleResponseModel.VehicleID = resp.VehicleID;
            verifyFleetVehicleResponseModel.FleetID = resp.FleetID;
            verifyFleetVehicleResponseModel.RCNo = resp.RCNo;
            verifyFleetVehicleResponseModel.IsSubmitted = resp.IsSubmitted;
            verifyFleetVehicleResponseModel.IsApproved = resp.IsApproved;
            verifyFleetVehicleResponseModel.Reject_Reason = resp.Reject_Reason;
            verifyFleetVehicleResponseModel.IsActive = resp.IsActive;
            verifyFleetVehicleResponseModel.IsCallCenterApproved = resp.IsCallCenterApproved;
            verifyFleetVehicleResponseModel.AgentRemark = resp.AgentRemark;
            verifyFleetVehicleResponseModel.IsAdminApproved = resp.IsAdminApproved;
            verifyFleetVehicleResponseModel.AdminRemark = resp.AdminRemark;
            verifyFleetVehicleResponseModel.RegistrationDate = resp.RegistrationDate;
            verifyFleetVehicleResponseModel.ExpiryDate = resp.ExpiryDate;
            verifyFleetVehicleResponseModel.VehicleType = resp.VehicleType;
            verifyFleetVehicleResponseModel.ChassisNo = resp.ChassisNo;
            verifyFleetVehicleResponseModel.EngineNo = resp.EngineNo;
            verifyFleetVehicleResponseModel.VehicleCompany = resp.VehicleCompany;
            verifyFleetVehicleResponseModel.VehicleModel = resp.VehicleModel;
            verifyFleetVehicleResponseModel.OwnerName = resp.OwnerName;
            verifyFleetVehicleResponseModel.FirNumber = resp.FirNumber;
            verifyFleetVehicleResponseModel.FirDate = resp.FirDate;
            verifyFleetVehicleResponseModel.IsBlacklisted = resp.IsBlacklisted;
            verifyFleetVehicleResponseModel.BlackListedReason = resp.BlackListedReason;
            verifyFleetVehicleResponseModel.Comment = resp.Comment;
            verifyFleetVehicleResponseModel.UpdatedBy = 41;
            verifyFleetVehicleResponseModel.UpdatedDate = DateTime.Now;
            verifyFleetVehicleResponseModels.Add(verifyFleetVehicleResponseModel);
        }
        verifyFleetVehicleResponseModels = await _fleetVehicleRepository.BulkUpdateFleetDetails(verifyFleetVehicleResponseModels);

        List<VerifyFleetVehicleResponse> verifyFleetVehicleResponses1 = new List<VerifyFleetVehicleResponse>();
        foreach (var fleetVehicle in verifyFleetVehicleResponseModels)
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

            verifyFleetVehicleResponses1.Add(verifyFleetVehicleResponse);
        }

        return verifyFleetVehicleResponses1;
    }

    public async Task<DeleteFleetVehicleResponse> DeleteFleetVehicle(long VehicleID)
    {
        DeleteFleetVehicleRequestModel deleteFleetVehicleRequestModel = new DeleteFleetVehicleRequestModel();
        deleteFleetVehicleRequestModel.VehicleID = VehicleID;

        FleetVehicleResponseModel deleteFleetVehicleResponseModel = await _fleetVehicleRepository.DeleteFleetVehicle(deleteFleetVehicleRequestModel);

        DeleteFleetVehicleResponse deleteFleetVehicleResponse = new DeleteFleetVehicleResponse();
        deleteFleetVehicleResponse.VehicleID = deleteFleetVehicleResponseModel.VehicleID;

        return deleteFleetVehicleResponse;
    }

    public async Task<List<GetFleetVehicleByFleetIDResponse>> GetFleetVehicleByFleetID(long FleetID)
    {
        GetFleetVehicleByFleetIDRequestModel getFleetVehicleByFleetIDRequestModel = new GetFleetVehicleByFleetIDRequestModel();
        getFleetVehicleByFleetIDRequestModel.FleetID = FleetID;

        List<FleetVehicleResponseModel> getFleetVehicleByFleetIDResponseModels = await _fleetVehicleRepository.GetFleetVehicleByFleetId(getFleetVehicleByFleetIDRequestModel);

        List<GetFleetVehicleByFleetIDResponse> getFleetVehicleByFleetIDResponses = new List<GetFleetVehicleByFleetIDResponse>();

        if (getFleetVehicleByFleetIDResponseModels.Count > 0)
        {
            foreach (var getFleetVehicleByFleetIDResponseModel in getFleetVehicleByFleetIDResponseModels)
            {
                GetFleetVehicleByFleetIDResponse getFleetVehicleByFleetIDResponse = new GetFleetVehicleByFleetIDResponse();
                getFleetVehicleByFleetIDResponse.VehicleID = getFleetVehicleByFleetIDResponseModel.VehicleID;
                getFleetVehicleByFleetIDResponse.FleetID = getFleetVehicleByFleetIDResponseModel.FleetID;
                getFleetVehicleByFleetIDResponse.RCNo = getFleetVehicleByFleetIDResponseModel.RCNo;
                getFleetVehicleByFleetIDResponses.Add(getFleetVehicleByFleetIDResponse);
            }
        }

        return getFleetVehicleByFleetIDResponses;
    }

    public async Task<GetFleetVehicleResponse> GetFleetVehicleById(long VehicleID)
    {
        GetFleetVehicleResponse getFleetVehicleResponse = new GetFleetVehicleResponse();
        GetFleetVehicleRequestModel getFleetVehicleRequestModel = new GetFleetVehicleRequestModel();
        getFleetVehicleRequestModel.VehicleID = VehicleID;

        FleetVehicleResponseModel getFleetVehicleResponseModel = await _fleetVehicleRepository.GetFleetVehicleById(getFleetVehicleRequestModel);

        if (getFleetVehicleResponseModel != null)
        {
            getFleetVehicleResponse.VehicleID = getFleetVehicleResponseModel.VehicleID;
            getFleetVehicleResponse.FleetID = getFleetVehicleResponseModel.FleetID;
            getFleetVehicleResponse.RCNo = getFleetVehicleResponseModel.RCNo;
        }

        return getFleetVehicleResponse;
    }

    public async Task<InstaVeritaResponse> InstaVeritaData(string RCNo)
    {
        InstaVeritaResponse instaVeritaResponse = new InstaVeritaResponse();

        InstaVeritaResponseModel instaVeritaResponseModel = await _fleetVehicleRepository.GetInstaVeritaDetailsByRC(RCNo);

        //if (instaVeritaResponseModel != null && string.IsNullOrEmpty(instaVeritaResponseModel.ChassisNumber))
        //{
        //    instaVeritaResponseModel = await _fleetVehicleRepository.GetInstaVeritaDetails(RCNo);
        //    if (instaVeritaResponseModel != null)
        //    {
        //        instaVeritaResponse.Blacklisted = (instaVeritaResponseModel.Blacklisted.ToString() is not "" and not "NA") ? Convert.ToBoolean(instaVeritaResponseModel.Blacklisted.ToString()) : null;
        //        instaVeritaResponse.BlacklistedReason = instaVeritaResponseModel.BlacklistedReason;
        //        instaVeritaResponse.ChassisNumber = instaVeritaResponseModel.ChassisNumber;
        //        instaVeritaResponse.EngineNumber = instaVeritaResponseModel.EngineNumber;
        //        instaVeritaResponse.ExpiryDate = (instaVeritaResponseModel.ExpiryDate.ToString() is not "" and not "NA") ? Convert.ToDateTime(instaVeritaResponseModel.ExpiryDate.ToString()) : null;
        //        instaVeritaResponse.FitnessCertificateExpiryDate = (instaVeritaResponseModel.FitnessCertificateExpiryDate.ToString() is not "" and not "NA") ? Convert.ToDateTime(instaVeritaResponseModel.FitnessCertificateExpiryDate.ToString()) : null;
        //        instaVeritaResponse.FinancingAuthority = instaVeritaResponseModel.FinancingAuthority;
        //        instaVeritaResponse.FuelType = instaVeritaResponseModel.FuelType;
        //        instaVeritaResponse.MVTaxPaidUpto = instaVeritaResponseModel.MVTaxPaidUpto;
        //        instaVeritaResponse.MVTaxStructure = instaVeritaResponseModel.MVTaxStructure;
        //        instaVeritaResponse.OwnersName = instaVeritaResponseModel.OwnersName;
        //        instaVeritaResponse.OwnerSerialNumber = instaVeritaResponseModel.OwnerSerialNumber;
        //        instaVeritaResponse.PuccUpto = instaVeritaResponseModel.PuccUpto;
        //        instaVeritaResponse.RegistrationNumber = instaVeritaResponseModel.RegistrationNumber;
        //        instaVeritaResponse.RegistrationDate = (instaVeritaResponseModel.RegistrationDate.ToString() is not "" and not "NA") ? Convert.ToDateTime(instaVeritaResponseModel.RegistrationDate.ToString()) : null;
        //        instaVeritaResponse.RegisteringAuthority = instaVeritaResponseModel.RegisteringAuthority;
        //        instaVeritaResponse.RegistrationState = instaVeritaResponseModel.RegistrationState;
        //        instaVeritaResponse.VehicleCompany = instaVeritaResponseModel.VehicleCompany;
        //        instaVeritaResponse.VehicleModel = instaVeritaResponseModel.VehicleModel;
        //        instaVeritaResponse.VehicleType = instaVeritaResponseModel.VehicleType;
        //        instaVeritaResponse.VehicleAge = instaVeritaResponseModel.VehicleAge;
        //        instaVeritaResponse.BlacklistedDetails = new List<BlackListDetails>();
        //        if (instaVeritaResponseModel.BlacklistedDetails.Count > 0)
        //        {
        //            foreach (var blackListedData in instaVeritaResponseModel.BlacklistedDetails)
        //            {
        //                BlackListDetails blackListDetails = new BlackListDetails();
        //                blackListDetails.RegistrationState = blackListedData.RegistrationState;
        //                blackListDetails.RegisteringAuthority = blackListedData.RegisteringAuthority;
        //                blackListDetails.RcNumber = blackListedData.RcNumber;
        //                blackListDetails.FirNumber = blackListedData.FirNumber;
        //                blackListDetails.FirDate = (blackListedData.FirDate.ToString() is not "" and not "NA") ? Convert.ToDateTime(blackListedData.FirDate.ToString()) : null;
        //                blackListDetails.BlacklistedReason = blackListedData.BlacklistedReason;
        //                blackListDetails.BlacklistedDate = (blackListedData.BlacklistedDate.ToString() is not "" and not "NA") ? Convert.ToDateTime(blackListedData.BlacklistedDate.ToString()) : null;
        //                instaVeritaResponse.BlacklistedDetails.Add(blackListDetails);
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    if (instaVeritaResponseModel != null)
        //    {
        //        instaVeritaResponse.Blacklisted = !(instaVeritaResponseModel.Blacklisted is String && (string.IsNullOrEmpty(instaVeritaResponseModel.Blacklisted) || Convert.ToString(instaVeritaResponseModel.Blacklisted) == "NA")) ? Convert.ToBoolean(instaVeritaResponseModel.Blacklisted) : null;
        //        instaVeritaResponse.BlacklistedReason = instaVeritaResponseModel.BlacklistedReason;
        //        instaVeritaResponse.ChassisNumber = instaVeritaResponseModel.ChassisNumber;
        //        instaVeritaResponse.EngineNumber = instaVeritaResponseModel.EngineNumber;
        //        instaVeritaResponse.ExpiryDate = !(instaVeritaResponseModel.ExpiryDate is String && (string.IsNullOrEmpty(instaVeritaResponseModel.ExpiryDate) || Convert.ToString(instaVeritaResponseModel.ExpiryDate) == "NA")) ? (instaVeritaResponseModel.ExpiryDate != null ? Convert.ToDateTime(instaVeritaResponseModel.ExpiryDate) : null) : null;
        //        instaVeritaResponse.FitnessCertificateExpiryDate = !(instaVeritaResponseModel.FitnessCertificateExpiryDate is String && (string.IsNullOrEmpty(instaVeritaResponseModel.FitnessCertificateExpiryDate) || Convert.ToString(instaVeritaResponseModel.FitnessCertificateExpiryDate) == "NA")) ? (instaVeritaResponseModel.FitnessCertificateExpiryDate != null ? Convert.ToDateTime(instaVeritaResponseModel.FitnessCertificateExpiryDate) : null) : null;
        //        instaVeritaResponse.FinancingAuthority = instaVeritaResponseModel.FinancingAuthority;
        //        instaVeritaResponse.FuelType = instaVeritaResponseModel.FuelType;
        //        instaVeritaResponse.MVTaxPaidUpto = instaVeritaResponseModel.MVTaxPaidUpto;
        //        instaVeritaResponse.MVTaxStructure = instaVeritaResponseModel.MVTaxStructure;
        //        instaVeritaResponse.OwnersName = instaVeritaResponseModel.OwnersName;
        //        instaVeritaResponse.OwnerSerialNumber = instaVeritaResponseModel.OwnerSerialNumber;
        //        instaVeritaResponse.PuccUpto = instaVeritaResponseModel.PuccUpto;
        //        instaVeritaResponse.RegistrationNumber = instaVeritaResponseModel.RegistrationNumber;
        //        instaVeritaResponse.RegistrationDate = !(instaVeritaResponseModel.RegistrationDate is String && (string.IsNullOrEmpty(instaVeritaResponseModel.RegistrationDate) || Convert.ToString(instaVeritaResponseModel.RegistrationDate) == "NA")) ? (instaVeritaResponseModel.RegistrationDate != null ? Convert.ToDateTime(instaVeritaResponseModel.RegistrationDate) : null) : null;
        //        instaVeritaResponse.RegisteringAuthority = instaVeritaResponseModel.RegisteringAuthority;
        //        instaVeritaResponse.RegistrationState = instaVeritaResponseModel.RegistrationState;
        //        instaVeritaResponse.VehicleCompany = instaVeritaResponseModel.VehicleCompany;
        //        instaVeritaResponse.VehicleModel = instaVeritaResponseModel.VehicleModel;
        //        instaVeritaResponse.VehicleType = instaVeritaResponseModel.VehicleType;
        //        instaVeritaResponse.VehicleAge = instaVeritaResponseModel.VehicleAge;
        //        instaVeritaResponse.BlacklistedDetails = new List<BlackListDetails>();
        //        if (instaVeritaResponseModel.BlacklistedDetails.Count > 0)
        //        {
        //            foreach (var blackListedData in instaVeritaResponseModel.BlacklistedDetails)
        //            {
        //                BlackListDetails blackListDetails = new BlackListDetails();
        //                blackListDetails.RegistrationState = blackListedData.RegistrationState;
        //                blackListDetails.RegisteringAuthority = blackListedData.RegisteringAuthority;
        //                blackListDetails.RcNumber = blackListedData.RcNumber;
        //                blackListDetails.FirNumber = blackListedData.FirNumber;
        //                blackListDetails.FirDate = !(blackListedData.FirDate is String && (string.IsNullOrEmpty(blackListedData.FirDate) || Convert.ToString(blackListedData.FirDate) == "NA")) ? (blackListedData.FirDate != null ? Convert.ToDateTime(blackListedData.FirDate) : null) : null;
        //                blackListDetails.BlacklistedReason = blackListedData.BlacklistedReason;
        //                blackListDetails.BlacklistedDate = !(blackListedData.BlacklistedDate is String && (string.IsNullOrEmpty(blackListedData.BlacklistedDate) || Convert.ToString(blackListedData.BlacklistedDate) == "NA")) ? (blackListedData.BlacklistedDate != null ? Convert.ToDateTime(blackListedData.BlacklistedDate) : null) : null;
        //                instaVeritaResponse.BlacklistedDetails.Add(blackListDetails);
        //            }
        //        }
        //    }
        //}

        if (instaVeritaResponseModel != null)
        {
            instaVeritaResponse.Blacklisted = !(instaVeritaResponseModel.Blacklisted is String && (string.IsNullOrEmpty(instaVeritaResponseModel.Blacklisted) || Convert.ToString(instaVeritaResponseModel.Blacklisted) == "NA")) ? Convert.ToBoolean(instaVeritaResponseModel.Blacklisted) : null;
            instaVeritaResponse.BlacklistedReason = instaVeritaResponseModel.BlacklistedReason;
            instaVeritaResponse.ChassisNumber = instaVeritaResponseModel.ChassisNumber;
            instaVeritaResponse.EngineNumber = instaVeritaResponseModel.EngineNumber;
            instaVeritaResponse.ExpiryDate = !(instaVeritaResponseModel.ExpiryDate is String && (string.IsNullOrEmpty(instaVeritaResponseModel.ExpiryDate) || Convert.ToString(instaVeritaResponseModel.ExpiryDate) == "NA")) ? (instaVeritaResponseModel.ExpiryDate != null ? Convert.ToDateTime(instaVeritaResponseModel.ExpiryDate) : null) : null;
            instaVeritaResponse.FitnessCertificateExpiryDate = !(instaVeritaResponseModel.FitnessCertificateExpiryDate is String && (string.IsNullOrEmpty(instaVeritaResponseModel.FitnessCertificateExpiryDate) || Convert.ToString(instaVeritaResponseModel.FitnessCertificateExpiryDate) == "NA")) ? (instaVeritaResponseModel.FitnessCertificateExpiryDate != null ? Convert.ToDateTime(instaVeritaResponseModel.FitnessCertificateExpiryDate) : null) : null;
            instaVeritaResponse.FinancingAuthority = instaVeritaResponseModel.FinancingAuthority;
            instaVeritaResponse.FuelType = instaVeritaResponseModel.FuelType;
            instaVeritaResponse.MVTaxPaidUpto = instaVeritaResponseModel.MVTaxPaidUpto;
            instaVeritaResponse.MVTaxStructure = instaVeritaResponseModel.MVTaxStructure;
            instaVeritaResponse.OwnersName = instaVeritaResponseModel.OwnersName;
            instaVeritaResponse.OwnerSerialNumber = instaVeritaResponseModel.OwnerSerialNumber;
            instaVeritaResponse.PuccUpto = instaVeritaResponseModel.PuccUpto;
            instaVeritaResponse.RegistrationNumber = instaVeritaResponseModel.RegistrationNumber;
            instaVeritaResponse.RegistrationDate = !(instaVeritaResponseModel.RegistrationDate is String && (string.IsNullOrEmpty(instaVeritaResponseModel.RegistrationDate) || Convert.ToString(instaVeritaResponseModel.RegistrationDate) == "NA")) ? (instaVeritaResponseModel.RegistrationDate != null ? Convert.ToDateTime(instaVeritaResponseModel.RegistrationDate) : null) : null;
            instaVeritaResponse.RegisteringAuthority = instaVeritaResponseModel.RegisteringAuthority;
            instaVeritaResponse.RegistrationState = instaVeritaResponseModel.RegistrationState;
            instaVeritaResponse.VehicleCompany = instaVeritaResponseModel.VehicleCompany;
            instaVeritaResponse.VehicleModel = instaVeritaResponseModel.VehicleModel;
            instaVeritaResponse.VehicleType = instaVeritaResponseModel.VehicleType;
            instaVeritaResponse.VehicleAge = instaVeritaResponseModel.VehicleAge;
            instaVeritaResponse.BlacklistedDetails = new List<BlackListDetails>();
            if (instaVeritaResponseModel.BlacklistedDetails.Count > 0)
            {
                foreach (var blackListedData in instaVeritaResponseModel.BlacklistedDetails)
                {
                    BlackListDetails blackListDetails = new BlackListDetails();
                    blackListDetails.RegistrationState = blackListedData.RegistrationState;
                    blackListDetails.RegisteringAuthority = blackListedData.RegisteringAuthority;
                    blackListDetails.RcNumber = blackListedData.RcNumber;
                    blackListDetails.FirNumber = blackListedData.FirNumber;
                    blackListDetails.FirDate = !(blackListedData.FirDate is String && (string.IsNullOrEmpty(blackListedData.FirDate) || Convert.ToString(blackListedData.FirDate) == "NA")) ? (blackListedData.FirDate != null ? Convert.ToDateTime(blackListedData.FirDate) : null) : null;
                    blackListDetails.BlacklistedReason = blackListedData.BlacklistedReason;
                    blackListDetails.BlacklistedDate = !(blackListedData.BlacklistedDate is String && (string.IsNullOrEmpty(blackListedData.BlacklistedDate) || Convert.ToString(blackListedData.BlacklistedDate) == "NA")) ? (blackListedData.BlacklistedDate != null ? Convert.ToDateTime(blackListedData.BlacklistedDate) : null) : null;
                    instaVeritaResponse.BlacklistedDetails.Add(blackListDetails);
                }
            }
        }


        return instaVeritaResponse;
    }

    public async Task<UpdateFleetVehicleRCResponse> UpdateFleetVehicleRC(long VehicleID, UpdateFleetVehicleRCRequest updateFleetVehicleRCRequest)
    {
        UpdateFleetVehicleRCRequestModel updateFleetVehicleRCRequestModel = new UpdateFleetVehicleRCRequestModel();
        updateFleetVehicleRCRequestModel.VehicleID = VehicleID;
        updateFleetVehicleRCRequestModel.RCNo = updateFleetVehicleRCRequest.RCNo;
        updateFleetVehicleRCRequestModel.IsSubmitted = false;
        updateFleetVehicleRCRequestModel.UpdatedBy = 41;
        updateFleetVehicleRCRequestModel.UpdatedDate = DateTime.Now;

        UpdateFleetVehicleRCResponseModel updateFleetVehicleRCResponseModel = await _fleetVehicleRepository.UpdateFleetVehicle(updateFleetVehicleRCRequestModel);

        UpdateFleetVehicleRCResponse updateFleetVehicleRCResponse = new UpdateFleetVehicleRCResponse();
        updateFleetVehicleRCResponse.VehicleID = updateFleetVehicleRCResponseModel.VehicleID;
        updateFleetVehicleRCResponse.Message = updateFleetVehicleRCResponseModel.Message;
        updateFleetVehicleRCResponse.RCNo = updateFleetVehicleRCResponseModel.RCNo;

        return updateFleetVehicleRCResponse;
    }

    public async Task<DeactivateFleetVehicleResponse> DeactivateFleetVehicle(long VehicleID)
    {
        DeactivateFleetVehicleRequestModel deactivateFleetVehicleRequestModel = new DeactivateFleetVehicleRequestModel();
        deactivateFleetVehicleRequestModel.VehicleID = VehicleID;
        deactivateFleetVehicleRequestModel.UpdatedBy = 41;
        deactivateFleetVehicleRequestModel.UpdatedDate = DateTime.Now;

        DeactivateFleetVehicleResponseModel deactivateFleetVehicleResponseModel = await _fleetVehicleRepository.DeactivateFleetVehicle(deactivateFleetVehicleRequestModel);

        DeactivateFleetVehicleResponse deactivateFleetVehicleResponse = new DeactivateFleetVehicleResponse();
        deactivateFleetVehicleResponse.VehicleID = deactivateFleetVehicleResponseModel.VehicleID;

        return deactivateFleetVehicleResponse;
    }

    public async Task<InstaVeritaLogResponse> InsertInstaVeritaDetails(InstaVeritaLogRequest instaVeritaLogRequest)
    {
        InstaVeritaLogRequestModel instaVeritaLogRequestModel = new InstaVeritaLogRequestModel();
        instaVeritaLogRequestModel.VehicleID = instaVeritaLogRequest.VehicleID;
        instaVeritaLogRequestModel.Blacklisted = instaVeritaLogRequest.Blacklisted;
        instaVeritaLogRequestModel.BlacklistedReason = instaVeritaLogRequest.BlacklistedReason;
        instaVeritaLogRequestModel.ChassisNumber = instaVeritaLogRequest.ChassisNumber;
        instaVeritaLogRequestModel.EngineNumber = instaVeritaLogRequest.EngineNumber;
        instaVeritaLogRequestModel.ExpiryDate = instaVeritaLogRequest.ExpiryDate;
        instaVeritaLogRequestModel.FitnessCertificateExpiryDate = instaVeritaLogRequest.FitnessCertificateExpiryDate;
        instaVeritaLogRequestModel.FinancingAuthority = instaVeritaLogRequest.FinancingAuthority;
        instaVeritaLogRequestModel.FuelType = instaVeritaLogRequest.FuelType;
        instaVeritaLogRequestModel.MVTaxPaidUpto = instaVeritaLogRequest.MVTaxPaidUpto;
        instaVeritaLogRequestModel.MVTaxStructure = instaVeritaLogRequest.MVTaxStructure;
        instaVeritaLogRequestModel.OwnersName = instaVeritaLogRequest.OwnersName;
        instaVeritaLogRequestModel.OwnerSerialNumber = instaVeritaLogRequest.OwnerSerialNumber;
        instaVeritaLogRequestModel.PuccUpto = instaVeritaLogRequest.PuccUpto;
        instaVeritaLogRequestModel.RegistrationNumber = instaVeritaLogRequest.RegistrationNumber;
        instaVeritaLogRequestModel.RegistrationDate = instaVeritaLogRequest.RegistrationDate;
        instaVeritaLogRequestModel.RegisteringAuthority = instaVeritaLogRequest.RegisteringAuthority;
        instaVeritaLogRequestModel.RegistrationState = instaVeritaLogRequest.RegistrationState;
        instaVeritaLogRequestModel.VehicleCompany = instaVeritaLogRequest.VehicleCompany;
        instaVeritaLogRequestModel.VehicleModel = instaVeritaLogRequest.VehicleModel;
        instaVeritaLogRequestModel.VehicleType = instaVeritaLogRequest.VehicleType;
        instaVeritaLogRequestModel.VehicleAge = instaVeritaLogRequest.VehicleAge;
        instaVeritaLogRequestModel.CreatedBy = 41;
        instaVeritaLogRequestModel.CreatedDate = DateTime.Now;

        InstaVeritaLogResponseModel instaVeritaLogResponseModel = await _fleetVehicleRepository.InsertInstaVeritaDetails(instaVeritaLogRequestModel);
        InstaVeritaLogResponse instaVeritaLogResponse = new InstaVeritaLogResponse();
        instaVeritaLogResponse.Log_Id = instaVeritaLogResponseModel.Log_Id;

        return instaVeritaLogResponse;
    }

    public async Task<BlackListedDetailsResponse> InsertInstaVeritaBlackListedDetails(BlackListedDetailsRequest blackListedDetailsRequest)
    {
        BlackListedDetailsRequestModel blackListedDetailsRequestModel = new BlackListedDetailsRequestModel();
        blackListedDetailsRequestModel.InstaLogId = blackListedDetailsRequest.InstaLogId;
        blackListedDetailsRequestModel.RegistrationState = blackListedDetailsRequest.RegistrationState;
        blackListedDetailsRequestModel.RegisteringAuthority = blackListedDetailsRequest.RegisteringAuthority;
        blackListedDetailsRequestModel.RcNumber = blackListedDetailsRequest.RcNumber;
        blackListedDetailsRequestModel.FirNumber = blackListedDetailsRequest.FirNumber;
        blackListedDetailsRequestModel.FirDate = blackListedDetailsRequest.FirDate;
        blackListedDetailsRequestModel.BlacklistedReason = blackListedDetailsRequest.BlacklistedReason;
        blackListedDetailsRequestModel.BlacklistedDate = blackListedDetailsRequest.BlacklistedDate;
        blackListedDetailsRequestModel.CreatedBy = 41;
        blackListedDetailsRequestModel.CreatedDate = DateTime.Now;

        BlackListedDetailsResponseModel blackListedDetailsResponseModel = await _fleetVehicleRepository.InsertInstaVeritaBlackListedDetails(blackListedDetailsRequestModel);

        BlackListedDetailsResponse blackListedDetailsResponse = new BlackListedDetailsResponse();
        blackListedDetailsResponse.BlackListedId = blackListedDetailsResponseModel.BlackListedId;

        return blackListedDetailsResponse;
    }

    public async Task<DeleteAllFleetVehicleResponse> DeleteAllFleetVehicleByFleetId(long fleetID)
    {
        DeleteAllFleetVehicleResponse deleteAllFleetVehicleResponse = new DeleteAllFleetVehicleResponse();
        deleteAllFleetVehicleResponse.FleetId = await _fleetVehicleRepository.DeleteAllFleetVehicleByFleetId(fleetID);
        return deleteAllFleetVehicleResponse;
    }
}
