using Tmf.Saarthi.Core.RequestModels.DMS;
using Tmf.Saarthi.Core.ResponseModels.DMS;

namespace Tmf.Saarthi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DMSController : ControllerBase
    {
        private readonly IDMSManager _dMSManager;
        private readonly IValidator<UploadDocumentsDMSRequest> _uploadDocumentsDMSRequestValidator;
        private readonly IValidator<ViewDocumentRequest> _viewDocumentRequestValidator;
        public DMSController(IDMSManager dMSManager, IValidator<UploadDocumentsDMSRequest> uploadDocumentsDMSRequest, IValidator<ViewDocumentRequest> viewDocumentRequestValidator)
        {
            _dMSManager = dMSManager;
            _uploadDocumentsDMSRequestValidator = uploadDocumentsDMSRequest;
            _viewDocumentRequestValidator = viewDocumentRequestValidator;
        }

        [HttpPost]      
        [Route("UploadDocuments")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UploadDocumentsDMSResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> UploadDocuments([FromBody] UploadDocumentsDMSRequest uploadDocumentsDMSRequest)
        {            
            ValidationResult validationResult = await _uploadDocumentsDMSRequestValidator.ValidateAsync(uploadDocumentsDMSRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
            }
            UploadDocumentsDMSResponse uploadDocumentsDMSResponse = await _dMSManager.UploadDocuments(uploadDocumentsDMSRequest);
            return Ok(uploadDocumentsDMSResponse);
        }

        [HttpPost]
        [Route("ViewDocument")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ViewDocumentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> ViewDocument([FromBody] ViewDocumentRequest viewDocumentRequest)
        {            
            ValidationResult validationResult = await _viewDocumentRequestValidator.ValidateAsync(viewDocumentRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
            }
            ViewDocumentResponse viewDocumentResponse = await _dMSManager.ViewDocument(viewDocumentRequest);
            return Ok(viewDocumentResponse);
        }
    }
}
