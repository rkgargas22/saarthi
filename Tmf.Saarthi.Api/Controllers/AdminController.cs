using Tmf.Saarthi.Core.RequestModels.Admin;
using Tmf.Saarthi.Core.ResponseModels.Admin;
using Tmf.Saarthi.Core.ResponseModels.Fleet;

namespace Tmf.Saarthi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminManager _adminManager;

        public AdminController(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        [HttpGet("AdminDashbaord")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<AdminDashbaordResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdminDashbaord()
        {
            List<AdminDashbaordResponse> adminDashboardResponse = await _adminManager.GetAdminDashbaord();
            return Ok(adminDashboardResponse);
        }

        [HttpGet("AdminFleet/{FleetId}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<AdminFleetResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdminFleet(long FleetId)
        {
            List<AdminFleetResponse> adminFleetResponse = await _adminManager.GetAdminFleet(FleetId);
            return Ok(adminFleetResponse);
        }

        [HttpGet("AdminFleetDeviation/{FleetId}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AdminFleetDeviationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdminFleetDeviation([FromRoute] long FleetId)
        {
            AdminFleetDeviationResponse adminFleetResponse = await _adminManager.GetAdminFleetDeviation(FleetId);
            return Ok(adminFleetResponse);
        }

        [HttpPatch]
        [Route("UpdateAdminFleetDeviation/{FleetID}")]
        [ProducesDefaultResponseType(typeof(GetFleetResponse))]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GetFleetResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAdminFleetDeviation([FromRoute] long FleetID, [FromBody] AdminFleetDeviationRequest adminFleetDeviationRequest)
        {
            AdminFleetDeviationUpdateResponse adminFleetResponse = await _adminManager.UpdateAdminFleetDeviation(FleetID, adminFleetDeviationRequest);

            return Ok(adminFleetResponse);
        }

        [HttpGet("AdminCaseOverViewData/{fleetId}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<AdminCaseOverViewResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdminCaseOverViewData([FromRoute] long fleetId)
        {
            List<AdminCaseOverViewResponse> adminCaseOverViewResponses = await _adminManager.GetAdminCaseOverViewData(fleetId);
            return Ok(adminCaseOverViewResponses);
        }


        [HttpPatch]
        [Route("ApproveAdminFleetDeviation")]
        [ProducesDefaultResponseType(typeof(GetFleetResponse))]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AdminFleetDeviationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> ApproveAdminFleetDeviation([FromBody] ApproveAdminFleetDeviationRequest approveAdminFleetDeviationRequest)
        {
            ApproveAdminFleetDeviationResponse approveAdminFleetDeviationResponse = await _adminManager.ApproveAdminFleetDeviation(approveAdminFleetDeviationRequest);

            return Ok(approveAdminFleetDeviationResponse);
        }


       

       
    }
}
