using Tmf.Saarthi.Core.ResponseModels.FI;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IFIManager
{
    Task<InitiateFIResponse> InitiateFI(long fleetId, long queueId, long createdBy);
}
