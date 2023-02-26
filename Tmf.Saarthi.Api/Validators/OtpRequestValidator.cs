using FluentValidation;
using Tmf.Saarthi.Core.Constants;
using Tmf.Saarthi.Core.RequestModels.Otp;

namespace Tmf.Saarthi.Api.Validators
{
    public class OtpRequestValidator : AbstractValidator<OtpRequest>
    {
        public OtpRequestValidator()
        {
            RuleFor(x => x.MobileNo).NotEmpty().Length(10).Matches("^[6-9]\\d{9}$").WithMessage(ValidationMessages.MobileNo);
            RuleFor(x => x.Type).NotEmpty().WithMessage(ValidationMessages.Type);
        }
    }
}
