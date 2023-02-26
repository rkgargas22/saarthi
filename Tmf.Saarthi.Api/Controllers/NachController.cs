using Tmf.Saarthi.Core.RequestModels.Nach;
using Tmf.Saarthi.Core.ResponseModels.Nach;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NachController : ControllerBase
{
    private readonly INachManager _nachManager;

    private readonly IValidator<NachRequest> _nachRequestValidator;
    private readonly IValidator<UpdateNachTimeSlotRequest> _updateNachTimeSlotRequestValidator;
    private readonly IValidator<UpdateNachStatusRequest> _updateNachStatusRequestValidator;
    public NachController(INachManager nachManager, IValidator<UpdateNachStatusRequest> updateNachStatusRequestValidator, IValidator<NachRequest> nachRequestValidator, IValidator<UpdateNachTimeSlotRequest> updateNachTimeSlotRequestValidator)
    {
        _nachManager = nachManager;
        _nachRequestValidator = nachRequestValidator;
        _updateNachTimeSlotRequestValidator = updateNachTimeSlotRequestValidator;
        _updateNachStatusRequestValidator = updateNachStatusRequestValidator;
    }

    [Route("UpdateNach")]
    [HttpPost]
    [ProducesDefaultResponseType(typeof(UpdateNachResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateNachResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateNach([FromBody] NachRequest nachRequest)
    {
        ValidationResult validationResult = await _nachRequestValidator.ValidateAsync(nachRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        UpdateNachResponse nachResponse = await _nachManager.UpdateNach(nachRequest);
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.InsertFailed, Error = ValidationMessages.DuplicateData });
        }

        return CreatedAtAction(nameof(UpdateNach), null, nachResponse);
    }
    
    [HttpGet("MNach/{FleetId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<NachResponseByFleetId>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMNachByFleetId([FromRoute] long FleetId)
    {
        NachResponseByFleetId nachResponse = await _nachManager.GetMNachByFleetId(FleetId);
        if (nachResponse == null ||  nachResponse.StartDate == null || nachResponse.EndDate == null || nachResponse.Frequency == null || nachResponse.PurposeOfManadate == null)
        {
            AddNachRequest nachRequest = new AddNachRequest();
            NachResponse response = new NachResponse();
            nachRequest.FleetID = FleetId;
            nachRequest.IsEnach = false;
            response = await _nachManager.AddNach(nachRequest);
            return Ok(response);
        }
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = "DB Error" });
        }

        return Ok(nachResponse);
    }
    [HttpGet("ENach/{FleetId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<NachResponseByFleetId>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetENachByFleetId([FromRoute] long FleetId)
    {
        NachResponseByFleetId nachResponse = await _nachManager.GetENachByFleetId(FleetId);
        if (nachResponse == null || nachResponse.StartDate == null || nachResponse.EndDate == null || nachResponse.Frequency == null || nachResponse.PurposeOfManadate == null)
        {
            AddNachRequest nachRequest = new AddNachRequest();
            NachResponse response = new NachResponse();
            nachRequest.FleetID = FleetId;
            nachRequest.IsEnach = true;
            response = await _nachManager.AddNach(nachRequest);
            return Ok(response);
        }
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = "DB Error" });
        }

        return Ok(nachResponse);
    }



    [HttpGet("Bank")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DropResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBank()
    {
        List<DropResponse> bankResponse = await _nachManager.GetBank();

        return Ok(bankResponse);
    }

    [HttpGet("State")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DropResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetState(int bankId)
    {
        List<DropResponse> stateResponse = await _nachManager.GetState(bankId);

        return Ok(stateResponse);
    }


    [HttpGet("City")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DropResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCity(int stateId)
    {
        List<DropResponse> stateResponse = await _nachManager.GetCity(stateId);

        return Ok(stateResponse);
    }

    [HttpGet("Branch/{BankId}/{StateId}/{CityId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DropResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBranch(int BankId, int StateId, int CityId)
    {
        List<DropResponse> branchResponse = await _nachManager.GetBranch(BankId, StateId, CityId);

        return Ok(branchResponse);
    }

    [HttpGet("IFSCCode/{BankId}/{BranchId}/{StateId}/{CityId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NachResponseIFSC), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIFSCCode(int BankId, int StateId, int CityId, int BranchId)
    {
        NachResponseIFSC nachIFSCResponse = await _nachManager.GetIFSCCode(BankId, StateId, CityId, BranchId);
        if (string.IsNullOrEmpty(nachIFSCResponse.IFSCCode))
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = "DB Error" });
        }

        return Ok(nachIFSCResponse);
    }
    [Route("UpdateNachStatus")]
    [HttpPost]
    [ProducesDefaultResponseType(typeof(UpdateNachResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateNachResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateNachStatus([FromBody] UpdateNachStatusRequest updateNachStatusRequest)
    {
        ValidationResult validationResult = await _updateNachStatusRequestValidator.ValidateAsync(updateNachStatusRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        UpdateNachResponse nachResponse = await _nachManager.UpdateNachStatus(updateNachStatusRequest);
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.InsertFailed, Error = ValidationMessages.DuplicateData });
        }

        return CreatedAtAction(nameof(UpdateNach), null, nachResponse);
    }
    [Route("UpdateTimeSlotStatus")]
    [HttpPost]
    [ProducesDefaultResponseType(typeof(UpdateNachResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateNachResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateTimeSlotStatus([FromBody] UpdateNachTimeSlotRequest updateNachTimeSlotRequest)
    {
        ValidationResult validationResult = await _updateNachTimeSlotRequestValidator.ValidateAsync(updateNachTimeSlotRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        UpdateNachResponse nachResponse = await _nachManager.UpdateTimeSlotStatus(updateNachTimeSlotRequest);
        if (nachResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.InsertFailed, Error = ValidationMessages.DuplicateData });
        }

        return CreatedAtAction(nameof(UpdateNach), null, nachResponse);
    }
    [HttpGet("TimeSlotAndStatusDate")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<NachResponseByFleetId>), StatusCodes.Status200OK)]
    public async Task<IActionResult> TimeSlotAndStatusDate(long FleetId, bool IsEnach)
    {
        NachStatusAndTimeslotResponse nachStatusAndTimeslotResponse = await _nachManager.GetTimeSlotAndStatusDate(FleetId , IsEnach);
        return Ok(nachStatusAndTimeslotResponse);
    }
}
