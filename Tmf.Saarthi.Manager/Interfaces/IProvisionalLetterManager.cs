using Tmf.Saarthi.Core.ResponseModels.ProvisionalLette;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IProvisionalLetterManager
{
    Task<ProvisionalLetteResponse> GenerateprovisionalLetter(long FleetID);
}
