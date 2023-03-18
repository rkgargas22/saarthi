using Tmf.Saarthi.Core.ResponseModels.CPCFI;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CPCFIController : ControllerBase
{
    private readonly ICPCFIManager _cPCFIManager;

    public CPCFIController(ICPCFIManager cPCFIManager)
    {
        _cPCFIManager = cPCFIManager;
    }

    [HttpGet("Dashboard/{AgentId}")]
    [ProducesDefaultResponseType(typeof(List<CPCDashboardResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<CPCDashboardResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Dashboard([FromRoute] long AgentId)
    {
        List<CPCDashboardResponse> cPCDashboardResponses = await _cPCFIManager.CPCDashboard(AgentId);

        return Ok(cPCDashboardResponses);
    }

    [HttpGet("PoolDashboard")]
    [ProducesDefaultResponseType(typeof(List<CPCDashboardResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<CPCDashboardResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> PoolDashboard()
    {
        List<CPCDashboardResponse> cPCDashboardResponses = await _cPCFIManager.CPCPoolDashboard();

        return Ok(cPCDashboardResponses);
    }

    [HttpGet("TLDashboard/{AgentId}")]
    [ProducesDefaultResponseType(typeof(List<CPCDashboardResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<CPCDashboardResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> TLDashboard([FromRoute] long AgentId)
    {
        List<CPCDashboardResponse> cPCDashboardResponses = await _cPCFIManager.CPCTLDashboard(AgentId);

        return Ok(cPCDashboardResponses);
    }

    //[HttpGet("GetCPCDeviationList")]
    //[ProducesDefaultResponseType(typeof(List<CPCDeviationListResponse>))]
    //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(List<CPCDeviationListResponse>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetCPCDeviationList()
    //{
    //    List<CPCDeviationListResponse> cPCDeviationListResponses = await _cPCFIManager.GetCPCDeviationList();

    //    return Ok(cPCDeviationListResponses);
    //}

    //[HttpGet("GetCPCAddressDetail")]
    //[ProducesDefaultResponseType(typeof(List<CPCDeviationListResponse>))]
    //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(List<CPCDeviationListResponse>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetCPCAddressDetail()
    //{
    //    List<CPCDeviationListResponse> cPCDeviationListResponses = await _cPCFIManager.GetCPCDeviationList();

    //    return Ok(cPCDeviationListResponses);
    //}

}
