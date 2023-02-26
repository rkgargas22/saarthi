using System.Text.RegularExpressions;
using System;
using Tmf.Saarthi.Core.RequestModels.Agent;
using Tmf.Saarthi.Core.ResponseModels.Agent;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Agent;
using Tmf.Saarthi.Infrastructure.Models.Response.Agent;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class AgentManager : IAgentManager
{
    private readonly IAgentRepository _agentRepository;
    public AgentManager(IAgentRepository agentRepository)
    {
        _agentRepository = agentRepository;
    }

    public async Task<List<AgentDashBoardResponse>> GetAgentDashBoardData(long? agentId)
    {
        List<AgentDashBoardResponseModel> agentDashBoardResponseModels = await _agentRepository.GetAgentDashBoardData(agentId);
        List<AgentDashBoardResponse> agentDashBoardResponses = new List<AgentDashBoardResponse>();
        if (agentDashBoardResponseModels.Count > 0)
        {
            foreach (var agentDashBoardResponseModelslist in agentDashBoardResponseModels)
            {
                AgentDashBoardResponse agentDashBoardResponse = new AgentDashBoardResponse();
                agentDashBoardResponse.FleedId = agentDashBoardResponseModelslist.FleedId;
                agentDashBoardResponse.CustomerName = agentDashBoardResponseModelslist.CustomerName;
                agentDashBoardResponse.AssignedDateTime = agentDashBoardResponseModelslist.AssignedDateTime;
                agentDashBoardResponse.ExpiryDate = agentDashBoardResponseModelslist.ExpiryDate;
                agentDashBoardResponse.CaseApplicationStatus = agentDashBoardResponseModelslist.CaseApplicationStatus;
                agentDashBoardResponse.Status = agentDashBoardResponseModelslist.Status;
                agentDashBoardResponses.Add(agentDashBoardResponse);
            }
        }
        return agentDashBoardResponses;
    }

    public async Task<List<AgentCaseOverViewResponse>> GetAgentCaseOverViewData(long fleetID)
    {
        List<AgentCaseOverViewResponseModel> agentCaseOverViewResponseModels = await _agentRepository.GetAgentCaseOverViewData(fleetID);
        List<AgentCaseOverViewResponse> agentCaseOverViewResponses = new List<AgentCaseOverViewResponse>();
        if (agentCaseOverViewResponseModels.Count > 0)
        {
            foreach (var agentCaseOverViewResponsesList in agentCaseOverViewResponseModels)
            {
                AgentCaseOverViewResponse agentCaseOverViewResponse = new AgentCaseOverViewResponse();
                agentCaseOverViewResponse.ID = agentCaseOverViewResponsesList.ID;
                agentCaseOverViewResponse.Status = agentCaseOverViewResponsesList.Status;
                agentCaseOverViewResponse.ModuleName = agentCaseOverViewResponsesList.ModuleName;
                agentCaseOverViewResponse.LogDateTime = agentCaseOverViewResponsesList.LogDateTime;
                agentCaseOverViewResponse.Comments = new List<AgentOverViewCommentResponse>();
                if(agentCaseOverViewResponsesList.Comments.Count > 0)
                {
                    List<AgentOverViewCommentResponse> agentOverViewCommentResponses = new List<AgentOverViewCommentResponse>();
                    foreach (var comment in agentCaseOverViewResponsesList.Comments)
                    {
                        AgentOverViewCommentResponse agentOverViewCommentResponse = new AgentOverViewCommentResponse();
                        agentOverViewCommentResponse.CommentId = comment.CommentId;
                        agentOverViewCommentResponse.Comment = comment.Comment;
                        agentOverViewCommentResponses.Add(agentOverViewCommentResponse);
                    }
                    agentCaseOverViewResponse.Comments = agentOverViewCommentResponses;
                }
                agentCaseOverViewResponses.Add(agentCaseOverViewResponse);
            }
        }
        return agentCaseOverViewResponses;
    }

    public async Task<AgentCustomerResponse> GetAgentCustomerData(string mobileNo)
    {
        AgentCustomerResponseModel agentCustomerResponseModels = await _agentRepository.GetAgentCustomerData(mobileNo);
        AgentCustomerResponse agentCustomerResponse = new AgentCustomerResponse();
        if (agentCustomerResponseModels != null && agentCustomerResponseModels.BpNo != 0)
        {
            agentCustomerResponse.FleedId = agentCustomerResponseModels.FleedId;
            agentCustomerResponse.FanNo = agentCustomerResponseModels.FanNo;
            agentCustomerResponse.PanNo = agentCustomerResponseModels.PanNo;
            agentCustomerResponse.MobileNo = agentCustomerResponseModels.MobileNo;
            agentCustomerResponse.BpNo = agentCustomerResponseModels.BpNo;
            agentCustomerResponse.FirstName = agentCustomerResponseModels.FirstName;
            agentCustomerResponse.MiddleName = agentCustomerResponseModels.MiddleName;
            agentCustomerResponse.LastName = agentCustomerResponseModels.LastName;
            agentCustomerResponse.Comment = agentCustomerResponseModels.Comment;
        }
        return agentCustomerResponse;
    }

    public async Task<List<AgentHistoryResponse>> GetAgentHistoryData(long fleetID)
    {
        List<AgentHsitoryResponseModel> agentHsitoryResponseModels = await _agentRepository.GetAgentHistoryData(fleetID);
        List<AgentHistoryResponse> agentHistoryResponses = new List<AgentHistoryResponse>();
        if (agentHsitoryResponseModels.Count > 0)
        {
            foreach (var agentHsitoryResponseModel in agentHsitoryResponseModels)
            {
                AgentHistoryResponse agentHistoryResponse = new AgentHistoryResponse();
                agentHistoryResponse.BpNo = agentHsitoryResponseModel.BpNo;
                agentHistoryResponse.AgentName = Regex.Replace(agentHsitoryResponseModel.AgentName, @"\s+", " ").Trim();
                agentHistoryResponse.FleetId = agentHsitoryResponseModel.FleetId;
                agentHistoryResponse.Comment = agentHsitoryResponseModel.Comment;
                agentHistoryResponse.LogDate = agentHsitoryResponseModel.LogDate;
                agentHistoryResponse.StageName = agentHsitoryResponseModel.StageName;
                agentHistoryResponses.Add(agentHistoryResponse);
            }
        }
        return agentHistoryResponses;
    }

    public async Task<List<AgentRejectedFleetResponse>> GetAgentRejectedFleetData(long fleetId)
    {
        List<AgentRejectedFleetResponseModel> agentRejectedFleetResponseModels = await _agentRepository.GetAgentRejectedFleetData(fleetId);
        List<AgentRejectedFleetResponse> agentRejectedFleetResponses = new List<AgentRejectedFleetResponse>();
        if (agentRejectedFleetResponseModels.Count > 0)
        {
            foreach (var agentRejectedFleetResponse in agentRejectedFleetResponseModels)
            {
                AgentRejectedFleetResponse agentRejectedFleet = new AgentRejectedFleetResponse();
                agentRejectedFleet.FleetId = agentRejectedFleetResponse.FleetId;
                agentRejectedFleet.RcNo = agentRejectedFleetResponse.RcNo;
                agentRejectedFleet.OwnerName = agentRejectedFleetResponse.OwnerName;
                agentRejectedFleet.RegistrationDate = agentRejectedFleetResponse.RegistrationDate;
                agentRejectedFleet.VehicleType = agentRejectedFleetResponse.VehicleType;
                agentRejectedFleet.VehicleModel = agentRejectedFleetResponse.VehicleModel;
                agentRejectedFleet.VehicleCompany = agentRejectedFleetResponse.VehicleCompany;
                agentRejectedFleetResponses.Add(agentRejectedFleet);
            }
        }
        return agentRejectedFleetResponses;
    }

    public async Task<List<AgentApprovedFleetResponse>> GetAgentApprovedFleetData(long fleetId)
    {
        List<AgentApprovedFleetResponseModel> agentApprovedFleetResponseModels = await _agentRepository.GetAgentApprovedFleetData(fleetId);
        List<AgentApprovedFleetResponse> agentApprovedFleetResponses = new List<AgentApprovedFleetResponse>();
        if (agentApprovedFleetResponseModels.Count > 0)
        {
            foreach (var agentApprovedFleetResponse in agentApprovedFleetResponseModels)
            {
                AgentApprovedFleetResponse agentApprovedFleet = new AgentApprovedFleetResponse();
                agentApprovedFleet.FleetId = agentApprovedFleetResponse.FleetId;
                agentApprovedFleet.RcNo = agentApprovedFleetResponse.RcNo;
                agentApprovedFleet.OwnerName = agentApprovedFleetResponse.OwnerName;
                agentApprovedFleet.RegistrationDate = agentApprovedFleetResponse.RegistrationDate;
                agentApprovedFleet.VehicleType = agentApprovedFleetResponse.VehicleType;
                agentApprovedFleet.VehicleModel = agentApprovedFleetResponse.VehicleModel;
                agentApprovedFleet.VehicleCompany = agentApprovedFleetResponse.VehicleCompany;
                agentApprovedFleetResponses.Add(agentApprovedFleet);
            }
        }
        return agentApprovedFleetResponses;
    }

    public async Task<AgentSalesDeviationResponse> GetAgentSalesDeviation(long fleetId)
    {
        AgentSalesDeviationResponseModel agentSalesDeviationResponseModel = await _agentRepository.GetAgentSalesDeviation(fleetId);
        AgentSalesDeviationResponse agentSalesDeviationResponse = new AgentSalesDeviationResponse();
        if (agentSalesDeviationResponseModel != null && agentSalesDeviationResponseModel.FleetId != 0)
        {
            agentSalesDeviationResponse.FleetId = agentSalesDeviationResponseModel.FleetId;
            agentSalesDeviationResponse.ProcessingFee = agentSalesDeviationResponseModel.ProcessingFee;
            agentSalesDeviationResponse.StampDuty = agentSalesDeviationResponseModel.StampDuty;
            agentSalesDeviationResponse.IRR = agentSalesDeviationResponseModel.IRR;
            agentSalesDeviationResponse.AIR = agentSalesDeviationResponseModel.AIR;
            agentSalesDeviationResponse.RequestedAIR = agentSalesDeviationResponseModel.RequestedAIR;
            agentSalesDeviationResponse.RequestedIRR = agentSalesDeviationResponseModel.RequestedIRR;
            agentSalesDeviationResponse.RequestedProcessingFees = agentSalesDeviationResponseModel.RequestedProcessingFees;
        }
        return agentSalesDeviationResponse;
    }

    public async Task<AgentSalesDeviationUpdateResponse> UpdateAgentSalesDeviationData(long fleetId, AgentSalesDeviationRequest agentSalesDeviationRequest)
    {
        AgentSalesDeviationRequestModel agentSalesDeviationRequestModel = new AgentSalesDeviationRequestModel();
        agentSalesDeviationRequestModel.RequestedProcessingFees = agentSalesDeviationRequest.IsRequestedProcessingFees ? agentSalesDeviationRequest.RequestedProcessingFees : null;
        agentSalesDeviationRequestModel.RequestedIRR = agentSalesDeviationRequest.IsRequestedIRR ? agentSalesDeviationRequest.RequestedIRR : null;
        agentSalesDeviationRequestModel.RequestedAIR = agentSalesDeviationRequest.IsRequestedAIR ? agentSalesDeviationRequest.RequestedAIR : null;
        agentSalesDeviationRequestModel.FleetId = fleetId;

        AgentSalesDeviationUpdateResponseModel agentSalesDeviationUpdateResponseModel = await _agentRepository.UpdateAgentSalesDeviationData(agentSalesDeviationRequestModel);

        AgentSalesDeviationUpdateResponse agentSalesDeviationUpdateResponse = new AgentSalesDeviationUpdateResponse();
        agentSalesDeviationUpdateResponse.FleetId = agentSalesDeviationUpdateResponseModel.FleetId;

        return agentSalesDeviationUpdateResponse;
    }

    public async Task<AgentFIResponse> GetAgentFIData(long fleetId)
    {
        AgentFIResponseModel agentFIResponseModel = await _agentRepository.GetAgentFIData(fleetId);
        AgentFIResponse agentFIResponse = new AgentFIResponse();
        if (agentFIResponseModel != null && agentFIResponseModel.FleetId != 0)
        {
            agentFIResponse.FleetId = agentFIResponseModel.FleetId;
            agentFIResponse.Status = agentFIResponseModel.Status;
            agentFIResponse.CreatedDate = agentFIResponseModel.CreatedDate;
        }
        return agentFIResponse;
    }

    public async Task<AssignFleetResponse> AssignFleet(AssignFleetRequest assignFleetRequest)
    {
        AssignFleetResponse assignFleetResponse = new AssignFleetResponse();
        AssignFleetRequestModel assignFleetRequestModel = new AssignFleetRequestModel();
        assignFleetRequestModel.FleetIDs = string.Join(",", assignFleetRequest.FleetIDs);
        assignFleetRequestModel.AgentId = assignFleetRequest.AgentId;
        assignFleetRequestModel.Role = assignFleetRequest.Role;
        assignFleetRequestModel.UpdatedBy = 41;
        assignFleetRequestModel.UpdatedDate = DateTime.Now;

        AssignFleetResponseModel assignFleetResponseModel = await _agentRepository.AssignFleet(assignFleetRequestModel);

        if(assignFleetResponseModel != null && assignFleetResponseModel.FleetID != 0)
        {
            assignFleetResponse.Message = "Updated Successfully";
        }

        return assignFleetResponse;
    }

    public async Task<AgentCustomerResponse> GetAgentCustomerDataByFleetID(long FleetID)
    {
        AgentCustomerResponseModel agentCustomerResponseModels = await _agentRepository.GetAgentCustomerDataByFleetId(FleetID);
        AgentCustomerResponse agentCustomerResponse = new AgentCustomerResponse();
        if (agentCustomerResponseModels != null && agentCustomerResponseModels.BpNo != 0)
        {
            agentCustomerResponse.FleedId = agentCustomerResponseModels.FleedId;
            agentCustomerResponse.FanNo = agentCustomerResponseModels.FanNo;
            agentCustomerResponse.PanNo = agentCustomerResponseModels.PanNo;
            agentCustomerResponse.MobileNo = agentCustomerResponseModels.MobileNo;
            agentCustomerResponse.BpNo = agentCustomerResponseModels.BpNo;
            agentCustomerResponse.FirstName = agentCustomerResponseModels.FirstName;
            agentCustomerResponse.MiddleName = agentCustomerResponseModels.MiddleName;
            agentCustomerResponse.LastName = agentCustomerResponseModels.LastName;
            agentCustomerResponse.Comment = agentCustomerResponseModels.Comment;
        }
        return agentCustomerResponse;
    }
}
