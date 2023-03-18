using Tmf.Saarthi.Infrastructure.Models.Request.FI;
using Tmf.Saarthi.Infrastructure.Models.Response.FI;

namespace Tmf.Saarthi.Infrastructure.Interfaces
{
    public interface IFIRepository
    {
        Task<InitiateFIDataDetailsResponseModel> GetFIRossInitiateData(long fleetId);

        Task<InitiateFIResponseModel> InitiateFI(InitiateFIRequestModel initiateFIRequestModel);

        Task UpdateInitiateFIResponse(UpdateInitiateFIRequestModel initiateFIRequestModel);
    }
}
