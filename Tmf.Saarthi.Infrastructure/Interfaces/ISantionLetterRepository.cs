using Tmf.Saarthi.Infrastructure.Models.Request.SanctionLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.SanctionLetter;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface ISantionLetterRepository
{
    Task<SanctionLetterResponseModel> GenerateSanctionLetter(SanctionLetterRequestModel sanctionLetterRequestModel);
}
