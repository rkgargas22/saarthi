using Tmf.Saarthi.Core.RequestModels.Login;
using Tmf.Saarthi.Core.ResponseModels.Login;


namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{

    private readonly ILogger _logger;
    private readonly ILoginManager _loginManager;
    private readonly IValidator<LoginRequest> _loginRequestValidator;
    public LoginController(ILogger<LoginController> logger, ILoginManager loginManager, IValidator<LoginRequest> loginRequestValidator)
    {
        _logger = logger;
        _loginManager = loginManager;
        _loginRequestValidator = loginRequestValidator;
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

        return CreatedAtAction(nameof(Post), new { loginResponse.BpNo }, loginResponse);

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
        if(employeeLoginResponse != null && employeeLoginResponse.BPNumber == 0) 
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.ValidationError, Error = ValidationMessages.IdNotFound });
        }
        
        return CreatedAtAction(nameof(EmployeeLogin), new { employeeLoginResponse.BPNumber }, employeeLoginResponse);

    }
}
