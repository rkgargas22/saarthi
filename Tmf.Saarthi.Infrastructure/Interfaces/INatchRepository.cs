using Tmf.Saarthi.Infrastructure.Models.Request.Natch;
using Tmf.Saarthi.Infrastructure.Models.Response.Natch;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface INatchRepository
{
    Task<NatchResponseModel> UpdateNach(NatchRequestModel nachRequestModel);

    Task<NachResponseModelByFleetId> GetNachByFleetId(long FleetId,bool IsEnach);

    Task<List<DropdownResponseModel>> GetBank();

    Task<List<DropdownResponseModel>> GetState(int bankId);

    Task<NachResponseModelIFSC> GetIFSCCode(int BankId, int StateId, int CityId, int BranchId);

    Task<List<DropdownResponseModel>> GetCity(int stateId);

    Task<List<DropdownResponseModel>> GetBranch(int bankId, int stateId, int cityId);

    Task<NatchResponseModel> AddNach(AddNatchRequestModel nachRequestModel);

    Task<NatchResponseModel> UpdateNachStatus(UpdateNatchStatusRequestModel nachStatusModel);

    Task<NatchResponseModel> UpdateTimeSlotStatus(UpdateNatchTimeSlotRequestModel updateNachTimeSlotModel);

    Task<NatchStatusAndTimeslotResponseModel> GetTimeSlotAndStatusDate(long FleetId, bool IsEnach);

    Task<NatchResponseModel> UpdateNachMandate(NatchMandateRequestModel natchMandateRequestModel);
}
