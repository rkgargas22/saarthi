using Tmf.Saarthi.Core.RequestModels.Email;
using Tmf.Saarthi.Core.ResponseModels.Email;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailManager _emailManager;
    private readonly IValidator<SendEmailRequest> _sendEmailValidator;
    public EmailController(IEmailManager emailManager, IValidator<SendEmailRequest> sendEmailValidator)
    {
        _emailManager = emailManager;
        _sendEmailValidator = sendEmailValidator;
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
}
