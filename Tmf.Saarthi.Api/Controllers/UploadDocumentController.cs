using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.ResponseModels.Document;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploadDocumentController : ControllerBase
{
    private readonly IUploadManager _uploadManager;
    private readonly IValidator<DocumentRequest> _documentRequestValidator;

    public UploadDocumentController(IUploadManager uploadManager, IValidator<DocumentRequest> documentRequestValidator)
    {
        _uploadManager = uploadManager;
        _documentRequestValidator = documentRequestValidator;
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
        if (documentRequestModel.DocumentUpload != null)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(documentRequestModel.DocumentUpload.FileName);
            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "FileUploaded");
            string folderPath = Path.Combine(uploadpath, documentRequestModel.FleetId.ToString());
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filepath = Path.Combine(folderPath, fileName);
            using (var filename = new FileStream(filepath, FileMode.Create))
            {
                documentRequestModel.DocumentUpload.CopyTo(filename);
            }
            documentRequestModel.DocumentUrl = filepath;
        }
        DocumentResponse documentResponse = await _uploadManager.AddDocument(documentRequestModel);
        return CreatedAtAction(nameof(Post), null, documentResponse);
    }
}
