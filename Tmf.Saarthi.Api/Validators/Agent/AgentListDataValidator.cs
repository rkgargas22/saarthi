using Tmf.Saarthi.Core.RequestModels.Agent;

namespace Tmf.Saarthi.Api.Validators.Agent;

public class AgentListDataValidator : AbstractValidator<AgentListDataRequest>
{
    public AgentListDataValidator() 
    {
        RuleFor(x => x.UserType).NotEmpty().WithMessage(ValidationMessages.UserType);
    }
}
