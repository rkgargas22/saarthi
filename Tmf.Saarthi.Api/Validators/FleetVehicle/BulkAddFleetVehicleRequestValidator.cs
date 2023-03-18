namespace Tmf.Saarthi.Api.Validators.FleetVehicle;

public class BulkAddFleetVehicleRequestValidator : AbstractValidator<BulkAddFleetVehicleRequest>
{
    public BulkAddFleetVehicleRequestValidator()
    {
        RuleFor(x => x.RCNoList).Must(x => x.Count() <= 20).WithMessage("Vehicle count should not exceed 20 vehicles");
        RuleForEach(x => x.RCNoList).NotNull().WithMessage("RcNo {CollectionIndex} is required.");
    }
}
