using System.Text.RegularExpressions;
using Tmf.Saarthi.Core.RequestModels.CPCFacility;
using Tmf.Saarthi.Core.ResponseModels.CPCFacility;
using Tmf.Saarthi.Core.ResponseModels.CPCFI;
using Tmf.Saarthi.Core.ResponseModels.Natch;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.CPCFacility;
using Tmf.Saarthi.Infrastructure.Models.Request.CPCFI;
using Tmf.Saarthi.Infrastructure.Models.Response.CPCFacility;
using Tmf.Saarthi.Infrastructure.Models.Response.CPCFI;
using Tmf.Saarthi.Infrastructure.Models.Response.Natch;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class CPCFacilityManager : ICPCFacilityManager
{
    private readonly ICPCFacilityRepository _cPCFacilityRepository;
    public CPCFacilityManager(ICPCFacilityRepository cPCFacilityRepository)
    {
        _cPCFacilityRepository = cPCFacilityRepository;
    }

    public async Task<List<DealDetailsResponse>> GetDealDetails(long fleetId)
    {
        List<DealDetailsResponseModel> dealDetailsResponseModelList = await _cPCFacilityRepository.GetDealDetails(fleetId);
        List<DealDetailsResponse> dealDetailsResponses = new List<DealDetailsResponse>();

        foreach (DealDetailsResponseModel model in dealDetailsResponseModelList)
        {
            DealDetailsResponse dealDetailsResponse = new DealDetailsResponse();
            dealDetailsResponse.FleetID = model.FleetID;
            dealDetailsResponse.LoanAmount = model.LoanAmount;
            dealDetailsResponse.AIR = model.AIR;
            dealDetailsResponse.IRR = model.IRR;
            dealDetailsResponse.ProcessingFeeAmount = model.ProcessingFeeAmount;
            dealDetailsResponse.StampDutyAmount = model.StampDutyAmount;
            dealDetailsResponse.FacilityNumber = model.FacilityNumber;
            dealDetailsResponse.FacilityDate = model.FacilityDate;
            dealDetailsResponse.FleetCount = model.FleetCount;
            dealDetailsResponses.Add(dealDetailsResponse);
        }

        return dealDetailsResponses;
    }

    public async Task<List<ApprovedFleetResponse>> GetApprovedFleet(long fleetId)
    {
        List<ApprovedFleetResponseModel> approvedFleetResponseModelList = await _cPCFacilityRepository.GetApprovedFleet(fleetId);
        List<ApprovedFleetResponse> approvedFleetResponses = new List<ApprovedFleetResponse>();

        foreach (ApprovedFleetResponseModel model in approvedFleetResponseModelList)
        {
            ApprovedFleetResponse approvedFleetResponse = new ApprovedFleetResponse();
            approvedFleetResponse.VehicleID = model.VehicleID;
            approvedFleetResponse.VehicleNo = model.VehicleNo;
            approvedFleetResponse.Comment = model.Comment;
            approvedFleetResponse.ApprovedBy = model.ApprovedBy;
            approvedFleetResponses.Add(approvedFleetResponse);
        }

        return approvedFleetResponses;
    }


    public async Task<List<NachDetailsResponse>> GetNachDetails(long fleetId)
    {
        List<NachDetailsResponseModel> nachDetailsResponseModelList = await _cPCFacilityRepository.GetNachDetails(fleetId);
        List<NachDetailsResponse> nachDetailsResponses = new List<NachDetailsResponse>();

        foreach (NachDetailsResponseModel model in nachDetailsResponseModelList)
        {
            NachDetailsResponse nachDetailsResponse = new NachDetailsResponse();
            nachDetailsResponse.NachId = model.NachId;
            nachDetailsResponse.FleetId = model.FleetId;
            nachDetailsResponse.IsEnach = model.IsEnach;
            nachDetailsResponse.UMRN = model.UMRN;
            nachDetailsResponse.EMandateId = model.EMandateId;
            nachDetailsResponse.EDate = model.EDate;
            nachDetailsResponse.Amount = model.Amount;
            nachDetailsResponse.Frequency = model.Frequency;
            nachDetailsResponse.BankName = model.BankName;
            nachDetailsResponse.AccountType = model.AccountType;
            nachDetailsResponse.AccountNumber = model.AccountNumber;
            nachDetailsResponse.IFSCCode = model.IFSCCode;
            nachDetailsResponses.Add(nachDetailsResponse);
        }

        return nachDetailsResponses;
    }

    public async Task<InwardFIDetailResponse> GetInwardFIDetail(long fleetId)
    {
        InwardFIDetailResponseModel inwardFIDetailResponseModelList = await _cPCFacilityRepository.GetInwardFIDetail(fleetId);
            InwardFIDetailResponse inwardFIDetailResponse = new InwardFIDetailResponse();
            inwardFIDetailResponse.FiID = inwardFIDetailResponseModelList.FiID;
            inwardFIDetailResponse.FleetId = inwardFIDetailResponseModelList.FleetId;
            inwardFIDetailResponse.FIStatus = inwardFIDetailResponseModelList.FIStatus;
            inwardFIDetailResponse.VerificationDate = inwardFIDetailResponseModelList.VerificationDate;

        return inwardFIDetailResponse;
    }

    public async Task<List<DropResponse>> GetInwardFIDeviationList()
    {
        List<DropdownResponseModel> dropdownResponseModelList = await _cPCFacilityRepository.GetInwardFIDeviationList();
        List<DropResponse> dropResponses = new List<DropResponse>();

        foreach (DropdownResponseModel model in dropdownResponseModelList)
        {
            DropResponse dropResponse = new DropResponse();
            dropResponse.Id = model.Id;
            dropResponse.DisplayName = model.DisplayName;
            dropResponses.Add(dropResponse);
        }

        return dropResponses;
    }

    public async Task<UpdateCPCFleetDeviationResponse> UpdateCPCFleetDeviation(UpdateCPCFleetDeviationRequest updateCPCFleetDeviationRequest)
    {
        UpdateCPCFleetDeviationRequestModel updateCPCFleetDeviationRequestModel = new UpdateCPCFleetDeviationRequestModel();
        updateCPCFleetDeviationRequestModel.FleetId = updateCPCFleetDeviationRequest.FleetId;
        updateCPCFleetDeviationRequestModel.DeviationId = updateCPCFleetDeviationRequest.DeviationId;
        updateCPCFleetDeviationRequestModel.CPCCheckStatus = updateCPCFleetDeviationRequest.CPCCheckStatus;
        updateCPCFleetDeviationRequestModel.Comment = updateCPCFleetDeviationRequest.Comment;

        UpdateCPCFleetDeviationResponseModel updateCPCFleetDeviationResponseModel = await _cPCFacilityRepository.UpdateCPCFleetDeviation(updateCPCFleetDeviationRequestModel);

        UpdateCPCFleetDeviationResponse updateCPCFleetDeviationResponse = new UpdateCPCFleetDeviationResponse();
        if (updateCPCFleetDeviationResponseModel.FleetId == 0)
        {
            updateCPCFleetDeviationResponse.Message = "Update Failed, No data found for this fleet.";
        }
        else
        {
            updateCPCFleetDeviationResponse.Message = "Updated Successfully";
        }
        return updateCPCFleetDeviationResponse;
    }

    public async Task<List<CPCDashboardResponse>> CPCDashboard(long AgentId)
    {
        List<CPCDashboardResponse> cPCDashboardResponses = new List<CPCDashboardResponse>();

        CPCDashboardRequestModel cPCDashboardRequestModel = new CPCDashboardRequestModel();
        cPCDashboardRequestModel.AgentId = AgentId;
        cPCDashboardRequestModel.Role = "CPCFC";

        List<CPCDashboardResponseModel> cPCDashboardResponseModels = await _cPCFacilityRepository.CPCDashboardData(cPCDashboardRequestModel);

        foreach (var data in cPCDashboardResponseModels)
        {
            CPCDashboardResponse cPCDashboardResponse = new CPCDashboardResponse();
            cPCDashboardResponse.FleetID = data.FleetID;
            cPCDashboardResponse.CustomerName = data.CustomerName;
            cPCDashboardResponse.AssignDate = data.AssignDate;
            cPCDashboardResponse.ExpiryDate = data.ExpiryDate;
            cPCDashboardResponse.AssignedAgent = data.AssignedAgent;
            cPCDashboardResponse.Status = data.Status;
            cPCDashboardResponses.Add(cPCDashboardResponse);
        }

        return cPCDashboardResponses;
    }

    public async Task<List<CPCDashboardResponse>> CPCTLDashboard(long AgentId)
    {
        List<CPCDashboardResponse> cPCDashboardResponses = new List<CPCDashboardResponse>();

        CPCDashboardRequestModel cPCDashboardRequestModel = new CPCDashboardRequestModel();
        cPCDashboardRequestModel.AgentId = AgentId;
        cPCDashboardRequestModel.Role = "CPCTLFC";

        List<CPCDashboardResponseModel> cPCDashboardResponseModels = await _cPCFacilityRepository.CPCDashboardData(cPCDashboardRequestModel);

        foreach (var data in cPCDashboardResponseModels)
        {
            CPCDashboardResponse cPCDashboardResponse = new CPCDashboardResponse();
            cPCDashboardResponse.FleetID = data.FleetID;
            cPCDashboardResponse.CustomerName = data.CustomerName;
            cPCDashboardResponse.AssignDate = data.AssignDate;
            cPCDashboardResponse.ExpiryDate = data.ExpiryDate;
            cPCDashboardResponse.AssignedAgent = data.AssignedAgent;
            cPCDashboardResponse.Status = data.Status;
            cPCDashboardResponses.Add(cPCDashboardResponse);
        }

        return cPCDashboardResponses;
    }

    public async Task<List<CPCDashboardResponse>> CPCPoolDashboard()
    {
        List<CPCDashboardResponse> cPCDashboardResponses = new List<CPCDashboardResponse>();

        List<CPCDashboardResponseModel> cPCDashboardResponseModels = await _cPCFacilityRepository.CPCPoolDashboardData();

        foreach (var data in cPCDashboardResponseModels)
        {
            CPCDashboardResponse cPCDashboardResponse = new CPCDashboardResponse();
            cPCDashboardResponse.FleetID = data.FleetID;
            cPCDashboardResponse.CustomerName = Regex.Replace(data.CustomerName, @"\s+", " ").Trim();
            cPCDashboardResponse.AssignDate = data.AssignDate;
            cPCDashboardResponse.ExpiryDate = data.ExpiryDate;
            cPCDashboardResponse.AssignedAgent = data.AssignedAgent;
            cPCDashboardResponse.Status = data.Status;
            cPCDashboardResponses.Add(cPCDashboardResponse);
        }

        return cPCDashboardResponses;
    }
}
