using Tmf.Saarthi.Core.RequestModels.Fleet;

namespace Tmf.Saarthi.Api.Validators.Fleet;

public class AddressChangeValidator : AbstractValidator<AddressChangeRequest>
{
    public AddressChangeValidator() 
    {
        RuleFor(x => x.IsAddressChange).NotNull().WithMessage(ValidationMessages.AddressChangeFlag);
    }
}
