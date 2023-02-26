using Tmf.Saarthi.Core.ResponseModels.Otp;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VerifyOtpController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IOtpManager _otpManager;
    private readonly ICustomerManager _customerManager;
    private readonly IValidator<VerifyOtpRequest> _verifyOtpValidator;
    public VerifyOtpController(ILogger<VerifyOtpController> logger, IOtpManager otpManager, ICustomerManager customerManager, IValidator<VerifyOtpRequest> verifyOtpValidator)
    {
        _logger = logger;
        _otpManager = otpManager;
        _verifyOtpValidator = verifyOtpValidator;
        _customerManager = customerManager;
    }


    [HttpPost]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VerifyOtpResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] VerifyOtpRequest verifyOtpRequest)
    {
        _logger.LogInformation("Verify otp start");
        ValidationResult result = await _verifyOtpValidator.ValidateAsync(verifyOtpRequest);

        if (!result.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
        }

        VerifyOtpResponse verifyOtpResponse = await _otpManager.VerifyOtpAsync(verifyOtpRequest);

       
        _logger.LogInformation("verify otp end");

        return CreatedAtAction(nameof(Post), new { verifyOtpResponse.customerResponse.BPNumber }, verifyOtpResponse);
    }
}
