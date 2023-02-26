using Tmf.Saarthi.Infrastructure.Models.Request.DMS;
using Tmf.Saarthi.Infrastructure.Models.Response.DMS;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IDMSRepository
{
    Task<GenerateFanNoResponseModel> GenerateFanNo(GenerateFanNoRequestModel generateFanNoRequestModel);
}
