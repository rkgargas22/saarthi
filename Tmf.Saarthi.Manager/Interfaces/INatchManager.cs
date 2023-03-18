using Tmf.Saarthi.Core.RequestModels.Natch;
using Tmf.Saarthi.Core.ResponseModels.Natch;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface INatchManager
{
    Task<UpdateNachResponse> UpdateNatch(long FleetID, NatchRequest nachRequest);

    Task<NachResponseByFleetId> GetMNatchByFleetId(long FleetId);

    Task<NachResponseByFleetId> GetENatchByFleetId(long FleetId);

    Task<NachResponseIFSC> GetIFSCCode(int BankId, int StateId, int CityId, int BranchId);

    Task<List<DropResponse>> GetBank();

    Task<List<DropResponse>> GetState(int BankId);

    Task<List<DropResponse>> GetCity(int stateId);

    Task<List<DropResponse>> GetBranch(int bankId, int stateId, int cityId);

    Task<NachResponseByFleetId> AddNatch(AddNatchRequest nachRequest);

    Task<UpdateNachResponse> UpdateNatchStatus(long FleetID, UpdateNatchStatusRequest updateNachStatusRequest);

    Task<UpdateNachResponse> UpdateTimeSlotStatus(long FleetID, UpdateNatchTimeSlotRequest updateNachTimeSlotRequest);

    Task<NatchStatusAndTimeslotResponse> GetTimeSlotAndStatusDate(long FleetId, bool IsEnach);

    Task<UpdateNachResponse> UpdateNatchMandate(long FleetID, NatchMandateRequest natchMandateRequest);
}
