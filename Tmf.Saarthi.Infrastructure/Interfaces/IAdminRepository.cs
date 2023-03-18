using Tmf.Saarthi.Infrastructure.Models.Request.Admin;
using Tmf.Saarthi.Infrastructure.Models.Response.Admin;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;

namespace Tmf.Saarthi.Infrastructure.Interfaces
{
    public interface IAdminRepository
    {
        Task<AdminFleetResponseModel> ApproveAdminFleetDeviation(ApproveAdminFleetDeviationRequestModel approveAdminFleetDeviationRequestModel);
        Task<List<AdminCaseOverViewResponseModel>> GetAdminCaseOverViewData(long fleetId);
        Task<List<AdminDashbaordResponseModel>> GetAdminDashboard();
        Task<List<AdminFleetResponseModel>> GetAdminFleet(long FleetId);
        Task<AdminFleetDeviationResponseModel> GetAdminFleetDeviation(long fleetId);
        Task<List<CustomerDataResponseModel>> GetCustomerData(long fleetId);
        Task<AdminFleetDeviationResponseModel> UpdateAdminFleetDeviation(AdminFleetDeviationRequestModel adminFleetDeviationRequestModel);
    }
}
