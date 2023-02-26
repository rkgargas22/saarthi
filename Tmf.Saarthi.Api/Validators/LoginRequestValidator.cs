using FluentValidation;
using Tmf.Saarthi.Core.Constants;
using Tmf.Saarthi.Core.RequestModels.Login;

namespace Tmf.Saarthi.Api.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage(ValidationMessages.UserName);
        RuleFor(x => x.Password).NotEmpty().WithMessage(ValidationMessages.Password);
    }
}
