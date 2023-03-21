using Tmf.Saarthi.Core.RequestModels.DMS;

namespace Tmf.Saarthi.Api.Validators.DMS
{
    public class UploadDocumentsDMSRequestValidator : AbstractValidator<UploadDocumentsDMSRequest>
    {

        public UploadDocumentsDMSRequestValidator()
        {
            RuleFor(x => x.FleetId).NotEmpty().WithMessage(ValidationMessages.FleetId);
           // RuleFor(x => x.IsRCUUpload).NotEmpty().WithMessage(ValidationMessages.IsRCUUpload);           
        }
    }
}
