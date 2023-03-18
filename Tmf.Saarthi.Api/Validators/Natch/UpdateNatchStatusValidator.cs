using Tmf.Saarthi.Core.RequestModels.Natch;

namespace Tmf.Saarthi.Api.Validators.Nach;

public class UpdateNatchStatusValidator : AbstractValidator<UpdateNatchStatusRequest>
{
    public UpdateNatchStatusValidator()
    {
        RuleFor(x => x.Status).NotEmpty().WithMessage(ValidationMessages.Status);
        RuleFor(x => x.IsNach).NotEmpty().WithMessage(ValidationMessages.IsNach);
    }
}
