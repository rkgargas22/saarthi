using FluentValidation;
using Tmf.Saarthi.Core.RequestModels.Fleet;

namespace Tmf.Saarthi.Api.Validators.Fleet;

public class AdditionalInformationValidator : AbstractValidator<AdditionalInformationRequest>
{
    public AdditionalInformationValidator() 
    {
        RuleFor(x => x.AdditionalInformation).NotEmpty().WithMessage(ValidationMessages.AdditionalInformation);
        RuleFor(x => x.DepartmentType).NotEmpty().WithMessage(ValidationMessages.DepartmentType);
    }
}
