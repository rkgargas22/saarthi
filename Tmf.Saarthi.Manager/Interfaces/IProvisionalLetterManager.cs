using Tmf.Saarthi.Core.RequestModels.Letter;
using Tmf.Saarthi.Core.ResponseModels.ProvisionalLette;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IProvisionalLetterManager
{
    Task<ProvisionalLetterResponse> GenerateProvisionalLetter(long FleetId, long CreatedBy);
    Task<DisagreeProvisionalResponse> DisagreeProvisionalLetter(DisagreeProvisionalResquest disagreeProvisionalResquest);
}
