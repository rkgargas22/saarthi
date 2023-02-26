namespace Tmf.Saarthi.Api.Validators.FleetVehicle;

public class UpdateFleetVehicleRCValidator : AbstractValidator<UpdateFleetVehicleRCRequest>
{
    public UpdateFleetVehicleRCValidator()
    {
        RuleFor(x => x.RCNo).NotEmpty().WithMessage(ValidationMessages.RCNo);
    }
}
