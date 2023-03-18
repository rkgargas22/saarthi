using Tmf.Saarthi.Core.RequestModels.CPCFacility;
using Tmf.Saarthi.Core.ResponseModels.CPCFacility;
using Tmf.Saarthi.Core.ResponseModels.CPCFI;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.Natch;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CPCFacilityController : ControllerBase
{
    private readonly ICPCFacilityManager _cPCFacilityManager;

    public CPCFacilityController(ICPCFacilityManager cPCFacilityManager)
    {
        _cPCFacilityManager = cPCFacilityManager;
    }

    [HttpGet("DealDetails/{FleetId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DealDetailsResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDealDetails(long FleetId)
    {
        List<DealDetailsResponse> dealDetailsResponse = await _cPCFacilityManager.GetDealDetails(FleetId);
        return Ok(dealDetailsResponse);
    }

    [HttpGet("ApprovedFleet/{FleetId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<ApprovedFleetResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetApprovedFleet(long FleetId)
    {
        List<ApprovedFleetResponse> approvedFleetResponse = await _cPCFacilityManager.GetApprovedFleet(FleetId);
        return Ok(approvedFleetResponse);
    }


    [HttpGet("NachDetails/{FleetId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<NachDetailsResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNachDetails(long FleetId)
    {
        List<NachDetailsResponse> nachDetailsResponse = await _cPCFacilityManager.GetNachDetails(FleetId);
        return Ok(nachDetailsResponse);
    }

    [HttpGet("DashboardFC/{AgentId}")]
    [ProducesDefaultResponseType(typeof(List<CPCDashboardResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<CPCDashboardResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DashboardFC([FromRoute] long AgentId)
    {
        List<CPCDashboardResponse> cPCDashboardResponses = await _cPCFacilityManager.CPCDashboard(AgentId);

        return Ok(cPCDashboardResponses);
    }

    [HttpGet("PoolDashboardFC")]
    [ProducesDefaultResponseType(typeof(List<CPCDashboardResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<CPCDashboardResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> PoolDashboardFC()
    {
        List<CPCDashboardResponse> cPCDashboardResponses = await _cPCFacilityManager.CPCPoolDashboard();

        return Ok(cPCDashboardResponses);
    }

    [HttpGet("TLDashboardFC/{AgentId}")]
    [ProducesDefaultResponseType(typeof(List<CPCDashboardResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<CPCDashboardResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> TLDashboardFC([FromRoute] long AgentId)
    {
        List<CPCDashboardResponse> cPCDashboardResponses = await _cPCFacilityManager.CPCTLDashboard(AgentId);

        return Ok(cPCDashboardResponses);
    }


    /*  CPC Inward starts from here  */

    [HttpGet("InwardFIDetail/{FleetId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<InwardFIDetailResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInwardFIDetail(long FleetId)
    {
        InwardFIDetailResponse dealDetailsResponse = await _cPCFacilityManager.GetInwardFIDetail(FleetId);
        return Ok(dealDetailsResponse);
    }

    [HttpGet("InwardFIDeviationList")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DropResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInwardFIDeviationList()
    {
        List<DropResponse> dropResponse = await _cPCFacilityManager.GetInwardFIDeviationList();
        return Ok(dropResponse);
    }

    [HttpPatch]
    [Route("UpdateCPCFleetDeviation")]
    [ProducesDefaultResponseType(typeof(GetFleetResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GetFleetResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCPCFleetDeviation([FromBody] UpdateCPCFleetDeviationRequest updateCPCFleetDeviationRequest)
    {
        UpdateCPCFleetDeviationResponse updateCPCFleetDeviationResponse = await _cPCFacilityManager.UpdateCPCFleetDeviation(updateCPCFleetDeviationRequest);

        return Ok(updateCPCFleetDeviationResponse);
    }
}
