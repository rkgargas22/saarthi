using Tmf.Saarthi.Infrastructure.Models.Request.Email;
using Tmf.Saarthi.Infrastructure.Models.Response.Email;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IEmailRepository
{
    Task<SendEmailResponseModel> SendEmail(SendEmailRequestModel sendEmailRequestModel);

    Task<TemplateDataResponse> GetTemplateData(TemplateDataRequest templateDataRequest);
}
