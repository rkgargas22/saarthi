using Tmf.Saarthi.Core.RequestModels.Fleet;

namespace Tmf.Saarthi.Api.Validators.Fleet
{
    public class EAgreementApprovalValidator : AbstractValidator<EAgreementApprovalRequest>
    {
        public EAgreementApprovalValidator() 
        {
            RuleFor(x => x.IsApproved).NotNull().WithMessage(ValidationMessages.ApprovalFlag);
        }
    }
}
