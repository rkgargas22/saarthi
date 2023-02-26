using Tmf.Saarthi.Core.RequestModels.Email;
using Tmf.Saarthi.Core.ResponseModels.Email;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IEmailManager
{
    Task<SendEmailResponse> SendEmail(SendEmailRequest sendEmailRequest);
}
