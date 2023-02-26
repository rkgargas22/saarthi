namespace Tmf.Saarthi.Api.Validators.FleetVehicle;

public class BulkAddFleetVehicleRequestValidator : AbstractValidator<BulkAddFleetVehicleRequest>
{
    public BulkAddFleetVehicleRequestValidator()
    {
        RuleForEach(x => x.RCNoList).NotNull().WithMessage("RcNo {CollectionIndex} is required.");
    }
}
