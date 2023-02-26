using Tmf.Saarthi.Core.RequestModels.Credit;
using Tmf.Saarthi.Core.ResponseModels.Credit;

namespace Tmf.Saarthi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ICreditManager _creditManager;
        public CreditController(ICreditManager creditManager)
        {
            _creditManager = creditManager;
        }

        [HttpGet("CreditDashboard")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<CreditDashboardResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCreditDashboard()
        {
            List<CreditDashboardResponse> adminFleetResponse = await _creditManager.GetCreditDashboard();
            return Ok(adminFleetResponse);
        }

        [HttpGet("FiDetails/{FleetId}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<FiDetailResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFiDetail([FromRoute] long FleetId)
        {
            List<FiDetailResponse> fiDetailResponse = await _creditManager.GetFiDetail(FleetId);
            return Ok(fiDetailResponse);
        }

        [HttpPatch]
        [Route("UpdateFiDetails/{FleetID}")]
        [ProducesDefaultResponseType(typeof(FiDetailResponse))]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FiDetailResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateFiDetail([FromRoute] long FleetID, [FromBody] UpdateFiDetailRequest adminFleetDeviationRequest)
        {
            UpdateFiDetailResponse updateFiDetailResponse = await _creditManager.UpdateFiDetail(FleetID, adminFleetDeviationRequest);

            return Ok(updateFiDetailResponse);
        }
    }
}
