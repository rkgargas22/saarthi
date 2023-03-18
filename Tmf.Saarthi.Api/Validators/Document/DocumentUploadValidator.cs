using Tmf.Saarthi.Core.RequestModels.Document;

namespace Tmf.Saarthi.Api.Validators.Document;

public class DocumentUploadValidator : AbstractValidator<UploadDocumentsRequest>
{
    public DocumentUploadValidator()
    {
        RuleFor(x => x.FleetId).NotEmpty().WithMessage(ValidationMessages.FleetId);       
        RuleFor(x => x.DocTypeId).NotEmpty().WithMessage(ValidationMessages.DocumentType);
        RuleFor(x => x.StageId).NotEmpty().WithMessage(ValidationMessages.StageType);
        RuleFor(x => x.Extension).NotEmpty().WithMessage(ValidationMessages.ExtensionType); 
        RuleFor(x => x.DocumentName).NotEmpty().WithMessage(ValidationMessages.ExtensionType);
        RuleFor(x => x.DocumentData).NotEmpty().WithMessage(ValidationMessages.DocumentUpload);
    }
}
