using Tmf.Saarthi.Core.RequestModels.Natch;

namespace Tmf.Saarthi.Api.Validators.Nach;

public class NatchMandateValidator : AbstractValidator<NatchMandateRequest>
{
    public NatchMandateValidator()
    {
        RuleFor(x => x.EmandateId).NotEmpty().WithMessage(ValidationMessages.EmandateId);
        RuleFor(x => x.EmandateDate).NotEmpty().WithMessage(ValidationMessages.EmandateDate);
        RuleFor(x => x.UtilityNo).NotEmpty().WithMessage(ValidationMessages.UtilityNo);
        RuleFor(x => x.CorporateName).NotEmpty().WithMessage(ValidationMessages.CorporateName);
        RuleFor(x => x.UMRN).NotEmpty().WithMessage(ValidationMessages.UMRN);
    }
}
