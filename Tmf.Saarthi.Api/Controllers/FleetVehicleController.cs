using Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FleetVehicleController : ControllerBase
{
    private readonly IFleetVehicleManager _fleetVehicleManager;
    private readonly IValidator<AddFleetVehicleRequest> _addFleetVehicleValidator;
    private readonly IValidator<BulkAddFleetVehicleRequest> _bulkAddFleetVehicleValidator;
    private readonly IValidator<UpdateFleetVehicleRCRequest> _updateFleetVehicleRCValidator;
    public FleetVehicleController(IFleetVehicleManager fleetVehicleManager, IValidator<AddFleetVehicleRequest> addFleetVehicleValidator, IValidator<BulkAddFleetVehicleRequest> bulkAddFleetVehicleValidator, IValidator<UpdateFleetVehicleRCRequest> updateFleetVehicleRCValidator)
    {
        _fleetVehicleManager = fleetVehicleManager;
        _addFleetVehicleValidator = addFleetVehicleValidator;
        _updateFleetVehicleRCValidator = updateFleetVehicleRCValidator;
        _bulkAddFleetVehicleValidator= bulkAddFleetVehicleValidator;
    }

    [HttpGet("GetByFleetID/{FleetID}")]
    [ProducesDefaultResponseType(typeof(List<GetFleetVehicleByFleetIDResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<GetFleetVehicleByFleetIDResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFleetID([FromRoute] long FleetID)
    {
        List<GetFleetVehicleByFleetIDResponse> getFleetVehicleResponses = await _fleetVehicleManager.GetFleetVehicleByFleetID(FleetID);

        return Ok(getFleetVehicleResponses);
    }

   
    [HttpGet("{VehicleID}")]
    [ProducesDefaultResponseType(typeof(GetFleetVehicleResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GetFleetVehicleResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute] long VehicleID)
    {
        GetFleetVehicleResponse getFleetVehicleResponse = await _fleetVehicleManager.GetFleetVehicleById(VehicleID);

        return Ok(getFleetVehicleResponse);
    }


    [HttpPost]
    [ProducesDefaultResponseType(typeof(AddFleetVehicleResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AddFleetVehicleResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] AddFleetVehicleRequest addFleetVehicleRequest)
    {
        ValidationResult validationResult = await _addFleetVehicleValidator.ValidateAsync(addFleetVehicleRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        AddFleetVehicleResponse addFleetVehicleResponse = await _fleetVehicleManager.AddFleetVehicle(addFleetVehicleRequest);
        if (addFleetVehicleResponse.VehicleID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.InsertFailed, Error = ValidationMessages.DuplicateData });
        }

        return CreatedAtAction(nameof(Post), new { addFleetVehicleResponse.VehicleID }, addFleetVehicleResponse);
    }

    [HttpPost]
    [Route("upload/{fleetid}")]
    [ProducesDefaultResponseType(typeof(BulkAddFleetVehicleResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BulkAddFleetVehicleResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Upload([FromRoute] int fleetId, [FromBody] BulkAddFleetVehicleRequest bulkAddFleetVehicleRequest)
    {       
        ValidationResult validationResult = await _bulkAddFleetVehicleValidator.ValidateAsync(bulkAddFleetVehicleRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        BulkAddFleetVehicleResponse bulkAddFleetVehicleResponseList = await _fleetVehicleManager.BulkAddFleetVehicle(fleetId,bulkAddFleetVehicleRequest);
        if (bulkAddFleetVehicleResponseList != null && bulkAddFleetVehicleResponseList.Vehicles.Count == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.InsertFailed, Error = ValidationMessages.DuplicateData });
        }

        return CreatedAtAction(nameof(Upload), new { bulkAddFleetVehicleResponseList!.FleetID }, bulkAddFleetVehicleResponseList);
    }

    [HttpPatch("UpdateRCNo/{VehicleID}")]
    [ProducesDefaultResponseType(typeof(UpdateFleetVehicleRCResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateFleetVehicleRCResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Patch([FromRoute] long VehicleID, [FromBody] UpdateFleetVehicleRCRequest updateFleetVehicleRCRequest)
    {
        ValidationResult validationResult = await _updateFleetVehicleRCValidator.ValidateAsync(updateFleetVehicleRCRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        // write condition here that rcno alresdy exist.

        UpdateFleetVehicleRCResponse updateFleetVehicleRCResponse = await _fleetVehicleManager.UpdateFleetVehicleRC(VehicleID, updateFleetVehicleRCRequest);
        if (updateFleetVehicleRCResponse.VehicleID == 0)
        {
            string errMessage = ValidationMessages.IdNotFound;
            if (!string.IsNullOrEmpty(updateFleetVehicleRCResponse.Message)) 
            {
                errMessage = updateFleetVehicleRCResponse.Message;
            }
            return BadRequest(new ErrorResponse { Message = ValidationMessages.UpdateFailed, Error = errMessage });
        }

        return Ok(updateFleetVehicleRCResponse);
    }

   
    [HttpDelete("{VehicleID}")]
    [ProducesDefaultResponseType(typeof(DeleteFleetVehicleResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DeleteFleetVehicleResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] long VehicleID)
    {
        DeleteFleetVehicleResponse deleteFleetVehicleResponse = await _fleetVehicleManager.DeleteFleetVehicle(VehicleID);
        if (deleteFleetVehicleResponse.VehicleID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.DeleteFailed, Error = ValidationMessages.IdNotFound });
        }
        return Ok(deleteFleetVehicleResponse);
    }   

    [HttpPatch("DeactivateRC/{VehicleID}")]
    [ProducesDefaultResponseType(typeof(DeactivateFleetVehicleResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DeactivateFleetVehicleResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeactivateRC([FromRoute] long VehicleID)
    {
        DeactivateFleetVehicleResponse deactivateFleetVehicleResponse = await _fleetVehicleManager.DeactivateFleetVehicle(VehicleID);
        if (deactivateFleetVehicleResponse.VehicleID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.UpdateFailed, Error = ValidationMessages.IdNotFound });
        }

        return Ok(deactivateFleetVehicleResponse);
    }

    [HttpDelete("DeleteAllVehicle/{fleetId}")]
    [ProducesDefaultResponseType(typeof(DeleteAllFleetVehicleResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DeleteAllFleetVehicleResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAllVehicleByFleetId([FromRoute] long fleetId)
    {
        DeleteAllFleetVehicleResponse result = await _fleetVehicleManager.DeleteAllFleetVehicleByFleetId(fleetId);
        if (result.FleetId == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.DeleteFailed, Error = "" });
        }
        return Ok(result);
    }
}
