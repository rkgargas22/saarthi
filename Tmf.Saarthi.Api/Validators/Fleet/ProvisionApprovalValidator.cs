using Tmf.Saarthi.Core.RequestModels.Fleet;

namespace Tmf.Saarthi.Api.Validators.Fleet;

public class ProvisionApprovalValidator : AbstractValidator<ProvisionApprovalRequest>
{
    public ProvisionApprovalValidator()
    {
        RuleFor(x => x.IsApproved).NotNull().WithMessage(ValidationMessages.ApprovalFlag);
    }
}
