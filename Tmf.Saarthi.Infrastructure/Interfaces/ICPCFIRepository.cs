using Tmf.Saarthi.Infrastructure.Models.Request.CPCFI;
using Tmf.Saarthi.Infrastructure.Models.Response.CPCFI;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface ICPCFIRepository
{
    Task<List<CPCDashboardResponseModel>> CPCDashboardData(CPCDashboardRequestModel cPCDashboardRequestModel);

    Task<List<CPCDashboardResponseModel>> CPCPoolDashboardData();

    Task<List<CPCDeviationListResponseModel>> GetCPCDeviationList();
}
