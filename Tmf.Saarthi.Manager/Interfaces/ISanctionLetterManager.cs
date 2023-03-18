using Tmf.Saarthi.Core.ResponseModels.SanctionLetter;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface ISanctionLetterManager
{
    Task<SanctionLetterResponse> GenerateSanctionLetter(long FleetId, long CreatedBy);
}
