using Tmf.Saarthi.Core.RequestModels.Ocr;
using Tmf.Saarthi.Core.ResponseModels.Ocr;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OcrController : ControllerBase
{
    private readonly IOcrManager _ocrManager;
    private readonly IValidator<AddressDetailsRequest> _getAddressDetailsValidator;

    public OcrController(IOcrManager ocrManager, IValidator<AddressDetailsRequest> getAddressDetailsValidator)
    {
        _ocrManager = ocrManager;
        _getAddressDetailsValidator = getAddressDetailsValidator;
    }

    [HttpPost("GetAddressDetail")]
    [ProducesDefaultResponseType(typeof(AddressDetailsResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AddressDetailsResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetAddressDetail([FromBody] AddressDetailsRequest getAddressDetailsRequest)
    {
        ValidationResult validationResult = await _getAddressDetailsValidator.ValidateAsync(getAddressDetailsRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        AddressDetailsResponse getAddressDetailsResponse = await _ocrManager.GetAddressDetails(getAddressDetailsRequest);
        if (string.IsNullOrEmpty(getAddressDetailsResponse.Address))
        {
            return BadRequest(new ErrorResponse { Message = "OCR failed.", Error = "Address Could not be verified." });
        }

        return CreatedAtAction(nameof(GetAddressDetail), getAddressDetailsResponse);
    }
}
