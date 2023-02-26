using Tmf.Saarthi.Core.RequestModels.Fleet;

namespace Tmf.Saarthi.Api.Validators.Fleet;

public class CommentValidator : AbstractValidator<CommentRequest>
{
    public CommentValidator() 
    {
        RuleFor(x => x.Comment).NotEmpty().WithMessage(ValidationMessages.Comment);
    }
}
