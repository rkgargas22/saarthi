using Tmf.Saarthi.Core.RequestModels.CPCFacility;
using Tmf.Saarthi.Core.ResponseModels.CPCFacility;
using Tmf.Saarthi.Core.ResponseModels.CPCFI;
using Tmf.Saarthi.Core.ResponseModels.Natch;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface ICPCFacilityManager
{
    Task<List<ApprovedFleetResponse>> GetApprovedFleet(long fleetId);
    Task<List<DealDetailsResponse>> GetDealDetails(long fleetId);
    Task<InwardFIDetailResponse> GetInwardFIDetail(long fleetId);
    Task<List<DropResponse>> GetInwardFIDeviationList();
    Task<List<NachDetailsResponse>> GetNachDetails(long fleetId);
    Task<UpdateCPCFleetDeviationResponse> UpdateCPCFleetDeviation(UpdateCPCFleetDeviationRequest updateCPCFleetDeviationRequest);

    Task<List<CPCDashboardResponse>> CPCDashboard(long AgentId);

    Task<List<CPCDashboardResponse>> CPCTLDashboard(long AgentId);

    Task<List<CPCDashboardResponse>> CPCPoolDashboard();
}
