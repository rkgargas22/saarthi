using Tmf.Saarthi.Core.RequestModels.Email;

namespace Tmf.Saarthi.Api.Validators.Email;

public class SendAgentEmailValidator : AbstractValidator<SendAgentEmailRequest>
{
    public SendAgentEmailValidator() 
    {
        RuleFor(x => x.Url).NotEmpty().WithMessage(ValidationMessages.Url);
        RuleFor(x => x.Template).NotEmpty().WithMessage(ValidationMessages.Template);
    }
}
