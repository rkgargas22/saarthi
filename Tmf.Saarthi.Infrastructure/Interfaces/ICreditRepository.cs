using Tmf.Saarthi.Infrastructure.Models.Request.Credit;
using Tmf.Saarthi.Infrastructure.Models.Response.Credit;

namespace Tmf.Saarthi.Infrastructure.Interfaces
{
    public interface ICreditRepository
    {
        Task<List<CreditDashboardResponseModel>> GetCreditDashboard();
        Task<List<FiDetailResponseModel>> GetFiDetail(long FleetId);
        Task<FiDetailResponseModel> UpdateFiDetail(UpdateFiDetailRequestModel updateFiDetailRequestModel);
    }
}
