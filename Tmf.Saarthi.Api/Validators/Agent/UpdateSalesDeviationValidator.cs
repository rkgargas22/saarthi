using Tmf.Saarthi.Core.RequestModels.Agent;

namespace Tmf.Saarthi.Api.Validators.Agent;

public class UpdateSalesDeviationValidator : AbstractValidator<AgentSalesDeviationRequest>
{
    public UpdateSalesDeviationValidator()
    {
        //RuleFor(x => x.IsRequestedProcessingFees).NotEmpty().WithMessage(ValidationMessages.IsRequestedProcessingFees);
        //RuleFor(x => x.RequestedProcessingFees).GreaterThan(0).When(x => x.IsRequestedProcessingFees).WithMessage(ValidationMessages.RequestedProcessingFees);
        //RuleFor(x => x.IsRequestedIRR).NotEmpty().WithMessage(ValidationMessages.IsRequestedIRR);
        //RuleFor(x => x.RequestedIRR).GreaterThan(0).When(x => x.IsRequestedIRR).WithMessage(ValidationMessages.RequestedIRR);
        //RuleFor(x => x.IsRequestedAIR).NotEmpty().WithMessage(ValidationMessages.IsRequestedAIR);
        //RuleFor(x => x.RequestedAIR).GreaterThan(0).When(x => x.IsRequestedAIR).WithMessage(ValidationMessages.RequestedAIR);
    }
}
