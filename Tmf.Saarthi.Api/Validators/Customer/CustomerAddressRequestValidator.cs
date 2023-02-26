using Tmf.Saarthi.Core.RequestModels.Customer;

namespace Tmf.Saarthi.Api.Validators.Customer
{
    public class CustomerAddressRequestValidator : AbstractValidator<CustomerAddressRequest>
    {
        public CustomerAddressRequestValidator()
        {
            RuleFor(x => x.BPNumber).NotEmpty().WithMessage(ValidationMessages.BpNumber);
            RuleFor(x => x.Type).NotEmpty().WithMessage(ValidationMessages.Type);
            RuleFor(x => x.AddressLine1).NotEmpty().WithMessage(ValidationMessages.AddressLine1);
            RuleFor(x => x.AddressLine2).NotEmpty().WithMessage(ValidationMessages.AddressLine2);
            RuleFor(x => x.Landmark).NotEmpty().WithMessage(ValidationMessages.Landmark);
            RuleFor(x => x.City).NotEmpty().WithMessage(ValidationMessages.City);
            RuleFor(x => x.District).NotEmpty().WithMessage(ValidationMessages.District);
            RuleFor(x => x.Region).NotEmpty().WithMessage(ValidationMessages.Region);
            RuleFor(x => x.Country).NotEmpty().WithMessage(ValidationMessages.Country);
            RuleFor(x => x.Pincode).NotEmpty().WithMessage(ValidationMessages.Pincode);
          
        }
    }
}
