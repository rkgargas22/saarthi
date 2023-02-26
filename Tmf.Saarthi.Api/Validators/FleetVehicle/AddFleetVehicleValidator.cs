namespace Tmf.Saarthi.Api.Validators.FleetVehicle;

public class AddFleetVehicleValidator : AbstractValidator<AddFleetVehicleRequest>
{
    public AddFleetVehicleValidator()
    {
        RuleFor(x => x.RCNo).NotEmpty().WithMessage(ValidationMessages.RCNo);
        RuleFor(x => x.FleetID).NotEmpty().WithMessage(ValidationMessages.FleetId);
    }
}
