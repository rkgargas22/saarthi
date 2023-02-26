using Tmf.Saarthi.Core.ResponseModels.Customer;
using Tmf.Saarthi.Core.ResponseModels.Otp;
using Tmf.Saarthi.Manager.Services;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OtpController : ControllerBase
{
    private readonly IValidator<OtpRequest> _otpValidator;
    private readonly ILogger _logger;
    private readonly IOtpManager _otpManager;
    private readonly ICustomerPreApprovedManager _customerPreApprovedManager;
    public OtpController(ILogger<OtpController> logger, IOtpManager otpManager, IValidator<OtpRequest> otpValidator, ICustomerPreApprovedManager customerPreApprovedManager)
    {
        _logger = logger;
        _otpManager = otpManager;
        _otpValidator = otpValidator;
        _customerPreApprovedManager = customerPreApprovedManager;
    }

  
    [HttpPost]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OtpResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] OtpRequest otpRequest)
    {
        ValidationResult result = await _otpValidator.ValidateAsync(otpRequest);

        if (!result.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
        }
        _logger.LogInformation("Login start");

        #region Check Customer exist

        CustomerResponse? customerResponse = await _customerPreApprovedManager.GetCustomerByMobileNo(otpRequest.MobileNo!);
        if (customerResponse == null)
        {
            return BadRequest(new ErrorResponse { Message = "Customer not exist in our system.", Error = otpRequest.MobileNo! });
        }
        #endregion

        OtpResponse otpResponse = await _otpManager.SendOtpAsync(otpRequest);
        _logger.LogInformation("Login end");

        return CreatedAtAction(nameof(Post), new { otpResponse.RequestId }, otpResponse);
    }
}
