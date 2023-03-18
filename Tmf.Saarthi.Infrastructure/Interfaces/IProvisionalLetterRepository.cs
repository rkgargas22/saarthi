using Tmf.Saarthi.Infrastructure.Models.Request.ProvisionalLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.ProvisionalLetter;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IProvisionalLetterRepository
{
    Task<DisagreeProvisionalResponseModel> DisagreeProvisionalLetter(DisagreeProvisionalResquestModel disagreeProvisionalResquestModel);
    Task<ProvisionalLetteResponseModel> GenerateprovisionalLetter(ProvisionalLetterRequestModel provisionalLetterRequestModel);
}
