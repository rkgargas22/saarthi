using Tmf.Saarthi.Core.RequestModels.Natch;

namespace Tmf.Saarthi.Api.Validators.Nach;

public class UpdateTimeSlotStatusValidator : AbstractValidator<UpdateNatchTimeSlotRequest>
{
    public UpdateTimeSlotStatusValidator()
    {
        RuleFor(x => x.IsNach).NotEmpty().WithMessage(ValidationMessages.IsNach);
        RuleFor(x => x.TimeSlot).NotEmpty().WithMessage(ValidationMessages.TimeSlot);
    }
}
