using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Tmf.Saarthi.Core.ResponseModels.CPCFI;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.CPCFI;
using Tmf.Saarthi.Infrastructure.Models.Response.Agent;
using Tmf.Saarthi.Infrastructure.Models.Response.CPCFI;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class CPCFIManager : ICPCFIManager
{
    private readonly ICPCFIRepository _cPCFIRepository;

    public CPCFIManager(ICPCFIRepository cPCFIRepository)
    {
        _cPCFIRepository = cPCFIRepository;
    }

    public async Task<List<CPCDashboardResponse>> CPCDashboard(long AgentId)
    {
        List<CPCDashboardResponse> cPCDashboardResponses = new List<CPCDashboardResponse>();

        CPCDashboardRequestModel cPCDashboardRequestModel = new CPCDashboardRequestModel();
        cPCDashboardRequestModel.AgentId = AgentId;
        cPCDashboardRequestModel.Role = "CPCFI";

        List<CPCDashboardResponseModel> cPCDashboardResponseModels = await _cPCFIRepository.CPCDashboardData(cPCDashboardRequestModel);

        foreach(var data in cPCDashboardResponseModels)
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

        List<CPCDashboardResponseModel> cPCDashboardResponseModels = await _cPCFIRepository.CPCPoolDashboardData();

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

    public async Task<List<CPCDashboardResponse>> CPCTLDashboard(long AgentId)
    {
        List<CPCDashboardResponse> cPCDashboardResponses = new List<CPCDashboardResponse>();

        CPCDashboardRequestModel cPCDashboardRequestModel = new CPCDashboardRequestModel();
        cPCDashboardRequestModel.AgentId = AgentId;
        cPCDashboardRequestModel.Role = "CPCTLFI";

        List<CPCDashboardResponseModel> cPCDashboardResponseModels = await _cPCFIRepository.CPCDashboardData(cPCDashboardRequestModel);

        foreach (var data in cPCDashboardResponseModels)
        {
            CPCDashboardResponse cPCDashboardResponse = new CPCDashboardResponse();
            cPCDashboardResponse.FleetID = data.FleetID;
            cPCDashboardResponse.CustomerName = Regex.Replace(data.CustomerName, @"\s+", " ").Trim(); ;
            cPCDashboardResponse.AssignDate = data.AssignDate;
            cPCDashboardResponse.ExpiryDate = data.ExpiryDate;
            cPCDashboardResponse.AssignedAgent = data.AssignedAgent;
            cPCDashboardResponse.Status = data.Status;
            cPCDashboardResponses.Add(cPCDashboardResponse);
        }

        return cPCDashboardResponses;
    }

    public async Task<List<CPCDeviationListResponse>> GetCPCDeviationList()
    {
        List<CPCDeviationListResponseModel> cPCDeviationListResponseModels = await _cPCFIRepository.GetCPCDeviationList();

        List<CPCDeviationListResponse> cPCDeviationListResponses = new List<CPCDeviationListResponse>();

        foreach(var data in cPCDeviationListResponseModels) 
        {
            CPCDeviationListResponse cPCDeviationListResponse = new CPCDeviationListResponse();
            cPCDeviationListResponse.DevId = data.DevId;
            cPCDeviationListResponse.Deviation = data.Deviation;
            cPCDeviationListResponses.Add(cPCDeviationListResponse);
        }

        return cPCDeviationListResponses;
    }
}
