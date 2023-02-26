using Tmf.Saarthi.Core.RequestModels.Agent;

namespace Tmf.Saarthi.Api.Validators.Agent;

public class AssignFleetValidator : AbstractValidator<AssignFleetRequest>
{
    public AssignFleetValidator()
    {
        RuleFor(x => x.FleetIDs).NotEmpty().WithMessage(ValidationMessages.FleetId);
        RuleFor(x => x.AgentId).NotEmpty().WithMessage(ValidationMessages.AgentId);
        RuleFor(x => x.Role).NotEmpty().WithMessage(ValidationMessages.Role);
    }
}
