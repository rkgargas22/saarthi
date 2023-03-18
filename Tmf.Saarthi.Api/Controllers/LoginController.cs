using System.Dynamic;
using Tmf.Saarthi.Core.RequestModels.Login;
using Tmf.Saarthi.Core.ResponseModels.Login;
using Tmf.Saarthi.Core.ResponseModels.Security;


namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{

    private readonly ILogger _logger;
    private readonly ILoginManager _loginManager;
    private readonly IValidator<LoginRequest> _loginRequestValidator;
    private readonly ITokenManager _tokenManager;
    public LoginController(ILogger<LoginController> logger, ILoginManager loginManager, IValidator<LoginRequest> loginRequestValidator, ITokenManager tokenManager)
    {
        _logger = logger;
        _loginManager = loginManager;
        _loginRequestValidator = loginRequestValidator;
        _tokenManager = tokenManager;
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(LoginResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
    {
        ValidationResult result = await _loginRequestValidator.ValidateAsync(loginRequest);

        if (!result.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
        }
        LoginResponse loginResponse = await _loginManager.LoginAsync(loginRequest);

        if (loginResponse != null && loginResponse.customerResponse.BPNumber == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.ValidationError, Error = ValidationMessages.IdNotFound });
        }
        TokenClaims claimPram = new TokenClaims()
        {
            BPNumber = loginResponse.customerResponse.BPNumber.ToString(),
            MobileNo = loginResponse.customerResponse.MobileNo,
            EmailID = loginResponse.customerResponse.EmailID,
            Role = "Customer"
        };
        var tokenToReturn = _tokenManager.AccesToken(claimPram);


        dynamic User = new ExpandoObject();

        User.loginResponse = loginResponse;
        User.Token = tokenToReturn;

        return CreatedAtAction(nameof(Post), new { loginResponse.customerResponse.BPNumber }, User);

    }

    [HttpPost]
    [Route("employee")]
    [ProducesDefaultResponseType(typeof(EmployeeLoginResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(EmployeeLoginResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> EmployeeLogin([FromBody] LoginRequest loginRequest)
    {
        ValidationResult result = await _loginRequestValidator.ValidateAsync(loginRequest);

        if (!result.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
        }
        EmployeeLoginResponse employeeLoginResponse = await _loginManager.EmployeeLoginAsync(loginRequest);

        if (employeeLoginResponse != null && employeeLoginResponse.BPNumber == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.ValidationError, Error = ValidationMessages.IdNotFound });
        }
        TokenClaims claimPram = new TokenClaims()
        {
            BPNumber = employeeLoginResponse.BPNumber.ToString(),
            MobileNo = employeeLoginResponse.MobileNo,
            EmailID = employeeLoginResponse.EmailID,
            Role = employeeLoginResponse.Role
        };
        var tokenToReturn = _tokenManager.AccesToken(claimPram);


        dynamic User = new ExpandoObject();

        User.employeeLoginResponse = employeeLoginResponse; 
        User.Token= tokenToReturn;

        return CreatedAtAction(nameof(EmployeeLogin), new { employeeLoginResponse.BPNumber }, User);

    }

   

}
