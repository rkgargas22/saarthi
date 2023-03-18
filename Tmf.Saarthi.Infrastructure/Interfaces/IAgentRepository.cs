using Tmf.Saarthi.Infrastructure.Models.Request.Agent;
using Tmf.Saarthi.Infrastructure.Models.Response.Agent;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IAgentRepository
{
    Task<List<AgentDashBoardResponseModel>> GetAgentDashBoardData(long? agentId);

    Task<List<AgentCaseOverViewResponseModel>> GetAgentCaseOverViewData(long fleetID);

    Task<AgentCustomerResponseModel> GetAgentCustomerData(string mobileNo);

    Task<List<AgentHsitoryResponseModel>> GetAgentHistoryData(long agentId);

    Task<List<AgentRejectedFleetResponseModel>> GetAgentRejectedFleetData(long fleetId);

    Task<List<AgentApprovedFleetResponseModel>> GetAgentApprovedFleetData(long fleetId);

    Task<AgentSalesDeviationResponseModel> GetAgentSalesDeviation(long fleetId);

    Task<AgentSalesDeviationUpdateResponseModel> UpdateAgentSalesDeviationData(AgentSalesDeviationRequestModel agentSalesDeviationRequestModel);

    Task<AgentFIResponseModel> GetAgentFIData(long fleetId);

    Task<AssignFleetResponseModel> AssignFleet(AssignFleetRequestModel assignFleetRequestModel);

    Task<AgentCustomerResponseModel> GetAgentCustomerDataByFleetId(long fleetId);

    Task<List<AgentListDataResponseModel>> GetAgentLists(AgentListDataRequestModel agentListDataRequestModel);
}
