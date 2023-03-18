using Tmf.Saarthi.Core.RequestModels.Document;

namespace Tmf.Saarthi.Api.Validators.Document
{
    public class DocumentRequestValidator : AbstractValidator<DocumentRequest>
    {
        public DocumentRequestValidator()
        {
            RuleFor(x => x.FleetId).NotEmpty().WithMessage(ValidationMessages.FleetId);
            //RuleFor(x => x.CreatedBy).NotEmpty().WithMessage(ValidationMessages.CreatedBy);
            // RuleFor(x => x.Documenttype).NotEmpty().WithMessage(ValidationMessages.Documenttype);
            //RuleFor(x => x.DocumentUpload).NotEmpty().WithMessage(ValidationMessages.DocumentUpload);
        }
    }
}
