using Microsoft.AspNetCore.Mvc;
using Tmf.Saarthi.Core.RequestModels.Agent;
using Tmf.Saarthi.Core.ResponseModels.Agent;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgentController : ControllerBase
{
    private readonly IAgentManager _agentManager;
    private readonly IValidator<AgentSalesDeviationRequest> _agentSalesDeviationRequestValidator;
    private readonly IValidator<AssignFleetRequest> _assignFleetValidator;
    private readonly IValidator<AgentListDataRequest> _agentListDataValidator;
    private readonly IValidator<SendOtpToCustomerRequest> _sendOtpToCustomerValidator;
    public AgentController(IAgentManager agentManager, IValidator<AgentSalesDeviationRequest> agentSalesDeviationRequestValidator, IValidator<AssignFleetRequest> assignFleetValidator, IValidator<AgentListDataRequest> agentListDataValidator, IValidator<SendOtpToCustomerRequest> sendOtpToCustomerValidator)
    {
        _agentManager = agentManager;
        _agentSalesDeviationRequestValidator = agentSalesDeviationRequestValidator;
        _assignFleetValidator = assignFleetValidator;
        _agentListDataValidator = agentListDataValidator;
        _sendOtpToCustomerValidator = sendOtpToCustomerValidator;
    }

    [HttpGet("dashBoardData/{agentId}")]
    [ProducesDefaultResponseType(typeof(List<AgentDashBoardResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<AgentDashBoardResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DashBoardData([FromRoute] long agentId)
    {
        List<AgentDashBoardResponse> agentDashBoardResponse = await _agentManager.GetAgentDashBoardData(agentId);
        return Ok(agentDashBoardResponse);
    }

    [HttpGet("poolDashBoardData")]
    [ProducesDefaultResponseType(typeof(List<AgentDashBoardResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<AgentDashBoardResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> PoolDashBoardData()
    {
        List<AgentDashBoardResponse> agentDashBoardResponse = await _agentManager.GetAgentDashBoardData(null);
        return Ok(agentDashBoardResponse);
    }

    [HttpGet("caseOverViewData/{fleetId}")]
    [ProducesDefaultResponseType(typeof(List<AgentCaseOverViewResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<AgentCaseOverViewResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CaseOverViewData([FromRoute] long fleetId)
    {
        List<AgentCaseOverViewResponse> agentCaseOverViewResponses = await _agentManager.GetAgentCaseOverViewData(fleetId);
        return Ok(agentCaseOverViewResponses);
    }

    [HttpGet("customerDataByMobileNo/{mobileNo}")]
    [ProducesDefaultResponseType(typeof(AgentCustomerResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AgentCustomerResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CustomerDataByMobileNo([FromRoute] string mobileNo)
    {
        AgentCustomerResponse agentCustomerResponse = await _agentManager.GetAgentCustomerData(mobileNo);
        if (agentCustomerResponse != null && agentCustomerResponse.BpNo == 0)
        {
            return BadRequest(new ErrorResponse { Message = "Customer not exist in our system.", Error = mobileNo! });
        }

        return Ok(agentCustomerResponse);
    }

    [HttpGet("historyData/{fleetID}")]
    [ProducesDefaultResponseType(typeof(List<AgentHistoryResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<AgentHistoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> HistoryData([FromRoute] long fleetID)
    {
        List<AgentHistoryResponse> agentHistoryResponses = await _agentManager.GetAgentHistoryData(fleetID);
        return Ok(agentHistoryResponses);
    }

    [HttpGet("rejectedFleetData/{fleetId}")]
    [ProducesDefaultResponseType(typeof(List<AgentRejectedFleetResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<AgentRejectedFleetResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RejectedFleetData([FromRoute] long fleetId)
    {
        List<AgentRejectedFleetResponse> agentRejectedFleetResponses = await _agentManager.GetAgentRejectedFleetData(fleetId);
        return Ok(agentRejectedFleetResponses);
    }

    [HttpGet("approvedFleetData/{fleetId}")]
    [ProducesDefaultResponseType(typeof(List<AgentApprovedFleetResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<AgentApprovedFleetResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ApprovedFleetData([FromRoute] long fleetId)
    {
        List<AgentApprovedFleetResponse> agentApprovedFleetResponses = await _agentManager.GetAgentApprovedFleetData(fleetId);
        return Ok(agentApprovedFleetResponses);
    }

    [HttpGet("salesDeviation/{fleetId}")]
    [ProducesDefaultResponseType(typeof(AgentSalesDeviationResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AgentSalesDeviationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SalesDeviation([FromRoute] long fleetId)
    {
        AgentSalesDeviationResponse agentSalesDeviationResponses = await _agentManager.GetAgentSalesDeviation(fleetId);
        return Ok(agentSalesDeviationResponses);
    }

    [HttpPatch("updateSalesDeviation/{FleetID}")]
    [ProducesDefaultResponseType(typeof(AgentSalesDeviationUpdateResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AgentSalesDeviationUpdateResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateSalesDeviation([FromRoute] long FleetID, [FromBody] AgentSalesDeviationRequest agentSalesDeviationRequest)
    {
        ValidationResult validationResult = await _agentSalesDeviationRequestValidator.ValidateAsync(agentSalesDeviationRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        AgentSalesDeviationUpdateResponse agentSalesDeviationUpdateResponse = await _agentManager.UpdateAgentSalesDeviationData(FleetID, agentSalesDeviationRequest);
        if (agentSalesDeviationUpdateResponse.FleetId == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.UpdateFailed, Error = ValidationMessages.ValidationError });
        }

        return Ok(agentSalesDeviationUpdateResponse);
    }

    [HttpGet("fIData/{fleetId}")]
    [ProducesDefaultResponseType(typeof(AgentFIResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AgentFIResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> FIData([FromRoute] long fleetId)
    {
        AgentFIResponse agentFIResponses = await _agentManager.GetAgentFIData(fleetId);
        return Ok(agentFIResponses);
    }

    [HttpPatch("AssignFleet")]
    [ProducesDefaultResponseType(typeof(AssignFleetResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AssignFleetResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> AssignFleet(AssignFleetRequest assignFleetRequest)
    {
        ValidationResult validationResult = await _assignFleetValidator.ValidateAsync(assignFleetRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }
        AssignFleetResponse assignFleetResponse = await _agentManager.AssignFleet(assignFleetRequest);
        if (assignFleetResponse != null && string.IsNullOrEmpty(assignFleetResponse.Message))
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.UpdateFailed, Error = ValidationMessages.IdNotFound });
        }

        return Ok(assignFleetResponse);
    }

    [HttpGet("GetCustomerData/{FleetID}")]
    [ProducesDefaultResponseType(typeof(AgentCustomerResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AgentCustomerResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerDetails([FromRoute] long FleetID)
    {
        AgentCustomerResponse agentCustomerResponse = await _agentManager.GetAgentCustomerDataByFleetID(FleetID);
        if (agentCustomerResponse != null && agentCustomerResponse.BpNo == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.DataNotAvailable, Error = ValidationMessages.IdNotFound });
        }

        return Ok(agentCustomerResponse);
    }


    [HttpGet("GetAgentListData")]
    [ProducesDefaultResponseType(typeof(List<AgentListDataResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<AgentListDataResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAgentListData([FromQuery] AgentListDataRequest agentListDataRequest)
    {
        ValidationResult validationResult = await _agentListDataValidator.ValidateAsync(agentListDataRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }
        List<AgentListDataResponse> agentListDataResponses = await _agentManager.GetAgentLists(agentListDataRequest);
        if (agentListDataResponses != null && agentListDataResponses.Count() == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.DataNotAvailable, Error = "Error Occurred" });
        }

        return Ok(agentListDataResponses);
    }

    [HttpPost("SendOtpToCustomer")]
    [ProducesDefaultResponseType(typeof(SendOtpToCustomerResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SendOtpToCustomerResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> SendOtpToCustomer([FromBody] SendOtpToCustomerRequest sendOtpToCustomerRequest)
    {
        ValidationResult validationResult = await _sendOtpToCustomerValidator.ValidateAsync(sendOtpToCustomerRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }
        SendOtpToCustomerResponse sendOtpToCustomerResponse = await _agentManager.SendOtpToCustomer(sendOtpToCustomerRequest);

        return CreatedAtAction(nameof(SendOtpToCustomer), null, sendOtpToCustomerResponse);
    }

    //[HttpGet("GetOtpKeyData")]
    //[ProducesDefaultResponseType(typeof(SendOtpToCustomerResponse))]
    //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(SendOtpToCustomerResponse), StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetOtpKeyData([FromQuery] string key)
    //{
    //    if (string.IsNullOrEmpty(key))
    //    {
    //        return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = "" });
    //    }

    //}

}
