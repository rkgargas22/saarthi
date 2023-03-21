using Tmf.Saarthi.Core.RequestModels.DMS;

namespace Tmf.Saarthi.Api.Validators.DMS
{
    public class ViewDocumentRequestValidator : AbstractValidator<ViewDocumentRequest>
    {

        public ViewDocumentRequestValidator()
        {
            RuleFor(x => x.FleetId).NotEmpty().WithMessage(ValidationMessages.FleetId);
         
        }
    }
}
