using System.Dynamic;
using Tmf.Saarthi.Core.ResponseModels.Otp;
using Tmf.Saarthi.Core.ResponseModels.Security;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VerifyOtpController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IOtpManager _otpManager;
    private readonly ICustomerManager _customerManager;
    private readonly IValidator<VerifyOtpRequest> _verifyOtpValidator;
    
    private readonly ITokenManager _tokenManager;
    public VerifyOtpController(ILogger<VerifyOtpController> logger, IOtpManager otpManager, ICustomerManager customerManager, IValidator<VerifyOtpRequest> verifyOtpValidator
        ,   ITokenManager tokenManager)
    {
        _logger = logger;
        _otpManager = otpManager;
        _verifyOtpValidator = verifyOtpValidator;
        _customerManager = customerManager;
        _tokenManager = tokenManager;
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

        var validOTP = await _otpManager.GetOTPDetailByReqId(verifyOtpRequest.RequestId);

        if (!validOTP) 
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = ValidationMessages.OTPSessionExpired });
        }

        VerifyOtpResponse verifyOtpResponse = await _otpManager.VerifyOtpAsync(verifyOtpRequest);
        _logger.LogInformation("verify otp end");
        TokenClaims claimPram = new TokenClaims()
        {
            BPNumber = verifyOtpResponse.customerResponse.BPNumber.ToString(),
            MobileNo = verifyOtpResponse.customerResponse.MobileNo,
            EmailID = verifyOtpResponse.customerResponse.EmailID,
            Role = "Customer"
        };
        var tokenToReturn = _tokenManager.AccesToken(claimPram);
        dynamic User = new ExpandoObject();
        User.verifyOtpResponse = verifyOtpResponse;
        User.Token = tokenToReturn;
        return CreatedAtAction(nameof(Post), new { verifyOtpResponse.customerResponse.BPNumber }, User);
         
    }

    

}
