namespace Tmf.Saarthi.Api.Validators
{
    public class VerifyOtpRequestValidator : AbstractValidator<VerifyOtpRequest>
    {
        public VerifyOtpRequestValidator()
        {
            RuleFor(x => x.MobileNo).NotEmpty().Length(10).Matches("^[6-9]\\d{9}$").WithMessage(ValidationMessages.MobileNo);
            RuleFor(x => x.Otp).NotEmpty().Length(4).Matches("^[0-9]+$").WithMessage(ValidationMessages.OTP);
            RuleFor(x => x.RequestId).NotEmpty().Matches("^[0-9]+$").WithMessage(ValidationMessages.RequestId);
        }
    }
}
