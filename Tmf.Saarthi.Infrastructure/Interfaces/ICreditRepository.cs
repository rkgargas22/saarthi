using Tmf.Saarthi.Core.RequestModels.Credit;
using Tmf.Saarthi.Infrastructure.Models.Request.Credit;
using Tmf.Saarthi.Infrastructure.Models.Response.Credit;

namespace Tmf.Saarthi.Infrastructure.Interfaces
{
    public interface ICreditRepository
    {
        Task<FiDetailResponseModel> FIRetrigger(FiRetriggerRequestModel fiRetriggerRequestModel);
        Task<List<CreditDashboardResponseModel>> GetCreditDashboard();
        Task<FiDetailResponseModel> GetFiDetail(long FleetId);
        Task<FiDetailResponseModel> UpdateFiDetail(UpdateFiDetailRequestModel updateFiDetailRequestModel);
    }
}
