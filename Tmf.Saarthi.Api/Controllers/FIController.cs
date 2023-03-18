using Tmf.Saarthi.Core.ResponseModels.FI;

namespace Tmf.Saarthi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FIController : ControllerBase
    {
        private readonly IFIManager _fIManager;

        public FIController(IFIManager fIManager)
        {
            _fIManager = fIManager;
        }

        [HttpGet]
        [Route("InitiateFI/{fleetId}/{queueId}")]
        [ProducesDefaultResponseType(typeof(InitiateFIResponse))]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InitiateFIResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<InitiateFIResponse>> InitiateFI(long fleetId, long queueId)
        {
            long createdBy = 1; // CreatedBy will get from response herder when authentication will implement.

            if (fleetId == 0)
            {
                return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = "Enter Valid Fleet Id" });
            }
            if (queueId == 0)
            {
                return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = "Enter Valid Queue Id" });
            }

            var result = await _fIManager.InitiateFI(fleetId, queueId, createdBy);

            return Ok(result);
        }
    }
}
