using Tmf.Saarthi.Core.RequestModels.Credit;
using Tmf.Saarthi.Core.ResponseModels.Credit;

namespace Tmf.Saarthi.Manager.Interfaces
{
    public interface ICreditManager
    {
        Task<List<CreditDashboardResponse>> GetCreditDashboard();
        Task<List<FiDetailResponse>> GetFiDetail(long FleetId);
        Task<UpdateFiDetailResponse> UpdateFiDetail(long FleetID, UpdateFiDetailRequest adminFleetDeviationRequest);
    }
}
