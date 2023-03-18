using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.ResponseModels.Document;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentController : ControllerBase
{
    private readonly IUploadManager _uploadManager;
    private readonly IValidator<DocumentRequest> _documentRequestValidator;
    private readonly IValidator<UploadDocumentsRequest> _uploadDocumentsRequestValidator;

    public DocumentController(IUploadManager uploadManager, IValidator<DocumentRequest> documentRequestValidator, IValidator<UploadDocumentsRequest> uploadDocumentsRequestValidator)
    {
        _uploadManager = uploadManager;
        _documentRequestValidator = documentRequestValidator;
        _uploadDocumentsRequestValidator = uploadDocumentsRequestValidator;
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(DocumentResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromForm] DocumentRequest documentRequestModel)
    {
        ValidationResult validationResult = await _documentRequestValidator.ValidateAsync(documentRequestModel);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }
        DocumentResponse documentResponse = await _uploadManager.AddDocument(documentRequestModel);
        if(documentResponse != null && documentResponse.DocumentId != 0) 
        { 
            return BadRequest(new ErrorResponse { Message = ValidationMessages.InsertFailed, Error = ValidationMessages.SomeErrorOccured });
        }
        return CreatedAtAction(nameof(Post), null, documentResponse);
    }

    [HttpPost]
    [Route("UploadDocuments")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UploadDocumentsResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> UploadDocuments([FromBody] UploadDocumentsRequest uploadDocumentsRequest)
    {
        long createdBy = 1;
        ValidationResult validationResult = await _uploadDocumentsRequestValidator.ValidateAsync(uploadDocumentsRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }
        UploadDocumentsResponse uploadDocumentsResponse = await _uploadManager.UploadDocuments(uploadDocumentsRequest, createdBy);

        return Ok(uploadDocumentsResponse);
    }

    [HttpGet("DownloadDocument/{FleetId}/{StageId}/{DocTypeId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DownloadDocumentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadDocument([FromRoute] long FleetId, [FromRoute] int StageId, [FromRoute] int DocTypeId)
    {
        List<DownloadDocumentResponse> downloadDocumentResponse = await _uploadManager.DownloadDocument(FleetId, StageId, DocTypeId);
        return Ok(downloadDocumentResponse);
    }

}
