using Tmf.Saarthi.Core.RequestModels.Agent;
using Tmf.Saarthi.Core.ResponseModels.Agent;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IAgentManager
{
    Task<List<AgentDashBoardResponse>> GetAgentDashBoardData(long? agentId);

    Task<List<AgentCaseOverViewResponse>> GetAgentCaseOverViewData(long fleetID);

    Task<AgentCustomerResponse> GetAgentCustomerData(string mobileNo);

    Task<List<AgentHistoryResponse>> GetAgentHistoryData(long agentId);

    Task<List<AgentRejectedFleetResponse>> GetAgentRejectedFleetData(long fleetId);

    Task<List<AgentApprovedFleetResponse>> GetAgentApprovedFleetData(long fleetId);

    Task<AgentSalesDeviationResponse> GetAgentSalesDeviation(long fleetId);

    Task<AgentSalesDeviationUpdateResponse> UpdateAgentSalesDeviationData(long fleetId, AgentSalesDeviationRequest agentSalesDeviationRequest);

    Task<AgentFIResponse> GetAgentFIData(long fleetId);

    Task<AssignFleetResponse> AssignFleet(AssignFleetRequest assignFleetRequest);

    Task<AgentCustomerResponse> GetAgentCustomerDataByFleetID(long FleetID);
}
