using FluentValidation;
using Tmf.Saarthi.Core.RequestModels.Email;

namespace Tmf.Saarthi.Api.Validators.Email;

public class SendEmailValidator : AbstractValidator<SendEmailRequest>
{
    public SendEmailValidator() 
    {
        RuleFor(x => x.BPNumber).NotEmpty().WithMessage(ValidationMessages.BpNumber);
        RuleFor(x => x.Module).NotEmpty().WithMessage(ValidationMessages.Module);
        RuleFor(x => x.TemplateType).NotEmpty().WithMessage(ValidationMessages.TemplateType);
    }
}
