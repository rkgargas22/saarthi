using Tmf.Saarthi.Core.RequestModels.Nach;
using Tmf.Saarthi.Core.ResponseModels.Nach;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface INachManager
{
    Task<UpdateNachResponse> UpdateNach(NachRequest nachRequest);
    Task<NachResponseByFleetId> GetMNachByFleetId(long FleetId);
    Task<NachResponseByFleetId> GetENachByFleetId(long FleetId);
    Task<NachResponseIFSC> GetIFSCCode(int BankId, int StateId, int CityId, int BranchId);
    Task<List<DropResponse>> GetBank();
    Task<List<DropResponse>> GetState(int BankId);
    Task<List<DropResponse>> GetCity(int stateId);
    Task<List<DropResponse>> GetBranch(int bankId, int stateId, int cityId);
    Task<NachResponse> AddNach(AddNachRequest nachRequest);
    Task<UpdateNachResponse> UpdateNachStatus(UpdateNachStatusRequest updateNachStatusRequest);
    Task<UpdateNachResponse> UpdateTimeSlotStatus(UpdateNachTimeSlotRequest updateNachTimeSlotRequest);
    Task<NachStatusAndTimeslotResponse> GetTimeSlotAndStatusDate(long FleetId, bool IsEnach);
}
