using Tmf.Saarthi.Core.RequestModels.Agent;

namespace Tmf.Saarthi.Api.Validators.Agent;

public class SendOtpToCustomerValidator : AbstractValidator<SendOtpToCustomerRequest>
{
    public SendOtpToCustomerValidator() 
    {
        RuleFor(x => x.MobileNo).NotEmpty().WithMessage(ValidationMessages.MobileNo);
    }
}
