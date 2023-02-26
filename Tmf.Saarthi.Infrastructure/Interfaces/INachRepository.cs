using Tmf.Saarthi.Infrastructure.Models.Request.Nach;
using Tmf.Saarthi.Infrastructure.Models.Response.Nach;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface INachRepository
{
    Task<NachResponseModel> UpdateNach(NachRequestModel nachRequestModel);
    Task<NachResponseModelByFleetId> GetNachByFleetId(long FleetId,bool IsEnach);
    Task<List<DropdownResponseModel>> GetBank();
    Task<List<DropdownResponseModel>> GetState(int bankId);
    Task<NachResponseModelIFSC> GetIFSCCode(int BankId, int StateId, int CityId, int BranchId);
    Task<List<DropdownResponseModel>> GetCity(int stateId);
    Task<List<DropdownResponseModel>> GetBranch(int bankId, int stateId, int cityId);
    Task<NachResponseModel> AddNach(AddNachRequestModel nachRequestModel);
    Task<NachResponseModel> UpdateNachStatus(UpdateNachStatusRequestModel nachStatusModel);
    Task<NachResponseModel> UpdateTimeSlotStatus(UpdateNachTimeSlotRequestModel updateNachTimeSlotModel);
    Task<NachStatusAndTimeslotResponseModel> GetTimeSlotAndStatusDate(long FleetId, bool IsEnach);
}
