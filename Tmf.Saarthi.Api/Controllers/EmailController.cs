using Tmf.Saarthi.Core.RequestModels.Email;
using Tmf.Saarthi.Core.ResponseModels.Email;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailManager _emailManager;
    private readonly IValidator<SendEmailRequest> _sendEmailValidator;
    private readonly IValidator<SendAgentEmailRequest> _sendAgentEmailValidator;
    public EmailController(IEmailManager emailManager, IValidator<SendEmailRequest> sendEmailValidator, IValidator<SendAgentEmailRequest> sendAgentEmailValidator)
    {
        _emailManager = emailManager;
        _sendEmailValidator = sendEmailValidator;
        _sendAgentEmailValidator = sendAgentEmailValidator;
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(SendEmailResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SendEmailResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest sendEmailRequest)
    {
        ValidationResult validationResult = await _sendEmailValidator.ValidateAsync(sendEmailRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }
        SendEmailResponse sendEmailResponse = await _emailManager.SendEmail(sendEmailRequest);

        return CreatedAtAction(nameof(SendEmail), null, sendEmailResponse);
    }

    [HttpPost("SendAgentEmail")]
    [ProducesDefaultResponseType(typeof(SendAgentEmailResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SendAgentEmailResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> SendAgentEmail([FromBody] SendAgentEmailRequest sendAgentEmailRequest)
    {
        ValidationResult validationResult = await _sendAgentEmailValidator.ValidateAsync(sendAgentEmailRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }
        SendAgentEmailResponse sendAgentEmailResponse = await _emailManager.SendAgentEmail(sendAgentEmailRequest);

        return CreatedAtAction(nameof(SendAgentEmail), null, sendAgentEmailResponse);
    }
}
