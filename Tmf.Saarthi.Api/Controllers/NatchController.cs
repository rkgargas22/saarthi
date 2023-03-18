using Tmf.Saarthi.Core.RequestModels.Natch;
using Tmf.Saarthi.Core.ResponseModels.Natch;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NatchController : ControllerBase
{
    private readonly INatchManager _nachManager;

    private readonly IValidator<NatchRequest> _nachRequestValidator;
    private readonly IValidator<UpdateNatchTimeSlotRequest> _updateNachTimeSlotRequestValidator;
    private readonly IValidator<UpdateNatchStatusRequest> _updateNachStatusRequestValidator;
    private readonly IValidator<NatchMandateRequest> _natchRequestValidator;
    public NatchController(INatchManager nachManager, IValidator<UpdateNatchStatusRequest> updateNachStatusRequestValidator, IValidator<NatchRequest> nachRequestValidator, IValidator<UpdateNatchTimeSlotRequest> updateNachTimeSlotRequestValidator, IValidator<NatchMandateRequest> natchRequestValidator)
    {
        _nachManager = nachManager;
        _nachRequestValidator = nachRequestValidator;
        _updateNachTimeSlotRequestValidator = updateNachTimeSlotRequestValidator;
        _updateNachStatusRequestValidator = updateNachStatusRequestValidator;
        _natchRequestValidator = natchRequestValidator;
    }

    [HttpPatch("UpdateNatch/{FleetID}")]
    [ProducesDefaultResponseType(typeof(UpdateNachResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateNachResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateNatch([FromRoute] long FleetID, [FromBody] NatchRequest nachRequest)
    {
        ValidationResult validationResult = await _nachRequestValidator.ValidateAsync(nachRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        UpdateNachResponse nachResponse = await _nachManager.UpdateNatch(FleetID,nachRequest);
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.UpdateFailed, Error = ValidationMessages.IdNotFound });
        }

        return CreatedAtAction(nameof(UpdateNatch), null, nachResponse);
    }
    
    [HttpGet("MNatch/{FleetId}")]
    [ProducesDefaultResponseType(typeof(List<NachResponseByFleetId>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<NachResponseByFleetId>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMNatchByFleetId([FromRoute] long FleetId)
    {
        NachResponseByFleetId nachResponse = await _nachManager.GetMNatchByFleetId(FleetId);
        if (nachResponse == null ||  nachResponse.StartDate == null || nachResponse.EndDate == null || nachResponse.Frequency == null || nachResponse.PurposeOfManadate == null)
        {
            AddNatchRequest nachRequest = new AddNatchRequest();
            //NatchResponse response = new NatchResponse();
            nachRequest.FleetID = FleetId;
            nachRequest.IsEnach = false;
            nachResponse = await _nachManager.AddNatch(nachRequest);
            return Ok(nachResponse);
        }
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = "DB Error" });
        }

        return Ok(nachResponse);
    }

    [HttpGet("ENatch/{FleetId}")]
    [ProducesDefaultResponseType(typeof(List<NachResponseByFleetId>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<NachResponseByFleetId>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetENatchByFleetId([FromRoute] long FleetId)
    {
        NachResponseByFleetId nachResponse = await _nachManager.GetENatchByFleetId(FleetId);
        if (nachResponse == null || nachResponse.StartDate == null || nachResponse.EndDate == null || nachResponse.Frequency == null || nachResponse.PurposeOfManadate == null)
        {
            AddNatchRequest nachRequest = new AddNatchRequest();
            //NatchResponse response = new NatchResponse();
            nachRequest.FleetID = FleetId;
            nachRequest.IsEnach = true;
            nachResponse = await _nachManager.AddNatch(nachRequest);
            return Ok(nachResponse);
        }
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = "DB Error" });
        }

        return Ok(nachResponse);
    }

    [HttpGet("Bank")]
    [ProducesDefaultResponseType(typeof(List<DropResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DropResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBank()
    {
        List<DropResponse> bankResponse = await _nachManager.GetBank();

        return Ok(bankResponse);
    }

    [HttpGet("State")]
    [ProducesDefaultResponseType(typeof(List<DropResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DropResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetState(int bankId)
    {
        List<DropResponse> stateResponse = await _nachManager.GetState(bankId);

        return Ok(stateResponse);
    }


    [HttpGet("City")]
    [ProducesDefaultResponseType(typeof(List<DropResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DropResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCity(int stateId)
    {
        List<DropResponse> stateResponse = await _nachManager.GetCity(stateId);

        return Ok(stateResponse);
    }

    [HttpGet("Branch")]
    [ProducesDefaultResponseType(typeof(List<DropResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DropResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBranch([FromQuery]int BankId, int StateId, int CityId)
    {
        List<DropResponse> branchResponse = await _nachManager.GetBranch(BankId, StateId, CityId);

        return Ok(branchResponse);
    }

    [HttpGet("IFSCCode")]
    [ProducesDefaultResponseType(typeof(NachResponseIFSC))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NachResponseIFSC), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIFSCCode(int BankId, int StateId, int CityId, int BranchId)
    {
        NachResponseIFSC nachIFSCResponse = await _nachManager.GetIFSCCode(BankId, StateId, CityId, BranchId);
        if (string.IsNullOrEmpty(nachIFSCResponse.IFSCCode))
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = "DB Error Or No Data Found" });
        }

        return Ok(nachIFSCResponse);
    }

    [HttpPatch("UpdateNatchStatus/{FleetID}")]
    [ProducesDefaultResponseType(typeof(UpdateNachResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateNachResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateNatchStatus([FromRoute] long FleetID, [FromBody] UpdateNatchStatusRequest updateNachStatusRequest)
    {
        ValidationResult validationResult = await _updateNachStatusRequestValidator.ValidateAsync(updateNachStatusRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        UpdateNachResponse nachResponse = await _nachManager.UpdateNatchStatus(FleetID, updateNachStatusRequest);
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.InsertFailed, Error = ValidationMessages.DuplicateData });
        }

        return Ok(nachResponse);
    }

    [HttpPatch("UpdateTimeSlotStatus/{FleetID}")]
    [ProducesDefaultResponseType(typeof(UpdateNachResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateNachResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTimeSlotStatus([FromRoute] long FleetID, [FromBody] UpdateNatchTimeSlotRequest updateNachTimeSlotRequest)
    {
        ValidationResult validationResult = await _updateNachTimeSlotRequestValidator.ValidateAsync(updateNachTimeSlotRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        UpdateNachResponse nachResponse = await _nachManager.UpdateTimeSlotStatus(FleetID, updateNachTimeSlotRequest);
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.InsertFailed, Error = ValidationMessages.DuplicateData });
        }

        return Ok(nachResponse);
    }

    [HttpGet("TimeSlotAndStatusDate")]
    [ProducesDefaultResponseType(typeof(List<NachResponseByFleetId>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<NachResponseByFleetId>), StatusCodes.Status200OK)]
    public async Task<IActionResult> TimeSlotAndStatusDate(long FleetId, bool IsEnach)
    {
        NatchStatusAndTimeslotResponse nachStatusAndTimeslotResponse = await _nachManager.GetTimeSlotAndStatusDate(FleetId , IsEnach);
        return Ok(nachStatusAndTimeslotResponse);
    }

    [HttpPatch("UpdateNatchMandate/{FleetID}")]
    [ProducesDefaultResponseType(typeof(UpdateNachResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateNachResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateNatchMandate([FromRoute] long FleetID, [FromBody] NatchMandateRequest natchMandateRequest)
    {
        ValidationResult validationResult = await _natchRequestValidator.ValidateAsync(natchMandateRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        UpdateNachResponse nachResponse = await _nachManager.UpdateNatchMandate(FleetID, natchMandateRequest);
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.UpdateFailed, Error = ValidationMessages.IdNotFound });
        }

        return Ok(nachResponse);
    }
}
