using Tmf.Saarthi.Core.RequestModels.Payment;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.Payment;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentManager _paymentManager;
    private readonly IValidator<SavePaymentStatusRequest> _savePaymentStatusValidator;
    public PaymentController(IValidator<SavePaymentStatusRequest> savePaymentStatusValidator, IPaymentManager paymentManager)
    {
        _savePaymentStatusValidator = savePaymentStatusValidator;
        _paymentManager = paymentManager;
    }

    [HttpGet]
    [Route("GeneratePaymentDetails/{FleetID}")]
    [ProducesDefaultResponseType(typeof(VerifyFleetResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VerifyFleetResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GeneratePaymentDetails([FromRoute] long FleetID)
    {
        VerifyFleetResponse verifyFleetResponse = await _paymentManager.GeneratePaymentDetails(FleetID);
        return Ok(verifyFleetResponse);
    }

    [HttpGet]
    [Route("GetPaymentUrl/{FleetID}")]
    [ProducesDefaultResponseType(typeof(GetPaymentUrlResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GetPaymentUrlResponse) ,StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentUrl([FromRoute] long FleetID)
    {
        GetPaymentUrlResponse getPaymentUrlResponse = await _paymentManager.GetPaymentUrl(FleetID);

        return Ok(getPaymentUrlResponse);
    }

    [HttpPost]
    [Route("SavePaymentStatus")]
    [ProducesDefaultResponseType(typeof(SavePaymentStatusResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SavePaymentStatusResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> SavePaymentStatus([FromBody] SavePaymentStatusRequest savePaymentStatusRequest)
    {
        ValidationResult validationResult = await _savePaymentStatusValidator.ValidateAsync(savePaymentStatusRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        SavePaymentStatusResponse savePaymentStatusResponse = await _paymentManager.SavePaymentStatus(savePaymentStatusRequest);

        return CreatedAtAction(nameof(SavePaymentStatus), savePaymentStatusResponse);
    }

    [HttpGet]
    [Route("GetPaymentStatus/{FleetID}")]
    [ProducesDefaultResponseType(typeof(GetPaymentStatusResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GetPaymentStatusResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentStatus([FromRoute] long FleetID)
    {
        GetPaymentStatusResponse getPaymentStatusResponse = await _paymentManager.GetPaymentStatus(FleetID, "S");

        return Ok(getPaymentStatusResponse);
    }

    [HttpGet]
    [Route("GetPaymentDetails/{FleetID}")]
    [ProducesDefaultResponseType(typeof(GetPaymentStatusResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GetPaymentStatusResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentDetails([FromRoute] long FleetID)
    {
        GetPaymentStatusResponse getPaymentStatusResponse = await _paymentManager.GetPaymentStatus(FleetID, "T");

        return Ok(getPaymentStatusResponse);
    }
}
