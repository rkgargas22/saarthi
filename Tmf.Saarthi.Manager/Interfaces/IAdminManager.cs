using Tmf.Saarthi.Core.RequestModels.Admin;
using Tmf.Saarthi.Core.ResponseModels.Admin;

namespace Tmf.Saarthi.Manager.Interfaces
{
    public interface IAdminManager
    {
        Task<ApproveAdminFleetDeviationResponse> ApproveAdminFleetDeviation(ApproveAdminFleetDeviationRequest approveAdminFleetDeviationRequest);
        Task<List<AdminCaseOverViewResponse>> GetAdminCaseOverViewData(long fleetId);
        Task<List<AdminDashbaordResponse>> GetAdminDashbaord();
        Task<List<AdminFleetResponse>> GetAdminFleet(long FleetId);
        Task<AdminFleetDeviationResponse> GetAdminFleetDeviation(long FleetId);
        Task<AdminFleetDeviationUpdateResponse> UpdateAdminFleetDeviation(long fleetID, AdminFleetDeviationRequest adminFleetDeviationRequest);
    }

}
