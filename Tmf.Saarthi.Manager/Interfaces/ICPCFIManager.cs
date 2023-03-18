using Tmf.Saarthi.Core.ResponseModels.CPCFI;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface ICPCFIManager
{
    Task<List<CPCDashboardResponse>> CPCDashboard(long AgentId);

    Task<List<CPCDashboardResponse>> CPCTLDashboard(long AgentId);

    Task<List<CPCDashboardResponse>> CPCPoolDashboard();

    Task<List<CPCDeviationListResponse>> GetCPCDeviationList();
}
