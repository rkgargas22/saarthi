using Tmf.Saarthi.Infrastructure.Models.Request.CPCFacility;
using Tmf.Saarthi.Infrastructure.Models.Request.CPCFI;
using Tmf.Saarthi.Infrastructure.Models.Response.CPCFacility;
using Tmf.Saarthi.Infrastructure.Models.Response.CPCFI;
using Tmf.Saarthi.Infrastructure.Models.Response.Natch;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface ICPCFacilityRepository
{
    Task<List<ApprovedFleetResponseModel>> GetApprovedFleet(long fleetId);
    Task<List<DealDetailsResponseModel>> GetDealDetails(object fleetId);
    Task<InwardFIDetailResponseModel> GetInwardFIDetail(long fleetId);
    Task<List<DropdownResponseModel>> GetInwardFIDeviationList();
    Task<List<NachDetailsResponseModel>> GetNachDetails(long fleetId);
    Task<UpdateCPCFleetDeviationResponseModel> UpdateCPCFleetDeviation(UpdateCPCFleetDeviationRequestModel updateCPCFleetDeviationRequestModel);

    Task<List<CPCDashboardResponseModel>> CPCDashboardData(CPCDashboardRequestModel cPCDashboardRequestModel);

    Task<List<CPCDashboardResponseModel>> CPCPoolDashboardData();
}
