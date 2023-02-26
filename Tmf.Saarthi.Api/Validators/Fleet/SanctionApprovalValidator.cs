using Tmf.Saarthi.Core.RequestModels.Fleet;

namespace Tmf.Saarthi.Api.Validators.Fleet;

public class SanctionApprovalValidator : AbstractValidator<SanctionApprovalRequest>
{
    public SanctionApprovalValidator() 
    {
        RuleFor(x => x.IsApproved).NotNull().WithMessage(ValidationMessages.ApprovalFlag);
    }
}
