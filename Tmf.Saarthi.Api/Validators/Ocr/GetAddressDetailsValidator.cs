using Tmf.Saarthi.Core.RequestModels.Ocr;

namespace Tmf.Saarthi.Api.Validators.Ocr;

public class GetAddressDetailsValidator : AbstractValidator<AddressDetailsRequest>
{
    public GetAddressDetailsValidator()
    {
        RuleFor(x => x.FleetID).NotEmpty().WithMessage(ValidationMessages.FleetId);
        RuleFor(x => x.BpNo).NotEmpty().WithMessage(ValidationMessages.BpNumber);
        RuleFor(x => x.DocumentType).NotEmpty().WithMessage(ValidationMessages.DocumentType);
        RuleFor(x => x.FrontPage).NotEmpty().WithMessage(ValidationMessages.FrontPage);
        RuleFor(x => x.BackPage).NotEmpty().WithMessage(ValidationMessages.BackPage);
    }
}
