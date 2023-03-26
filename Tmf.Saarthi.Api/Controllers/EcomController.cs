using Tmf.Saarthi.Core.RequestModels.Ecom;
using Tmf.Saarthi.Core.ResponseModels.Ecom;


namespace Tmf.Saarthi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcomController : ControllerBase
    {
        //private readonly IValidator<AdditionalInformation> _ecomManifestRequestValidator;
        //private readonly IValidator<PushShipmentStatusRequest> _pushShipmentStatusRequest;
        private readonly IEcomManager _ecomManager;
        public EcomController(IEcomManager ecomManager)
        {
            _ecomManager = ecomManager;
            //_ecomManifestRequestValidator = ecomManifestRequestValidator;
            //_pushShipmentStatusRequest = pushShipmentStatusRequest;
        }

        [HttpPost]
        [Route("GenerateManifest")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<GenerateManifestResponse>), StatusCodes.Status201Created)]
        public async Task<IActionResult> GenerateManifest(int FleetId, [FromBody] AdditionalInformation EcomManifestRequest)
        {
            //ValidationResult result = await _ecomManifestRequestValidator.ValidateAsync(EcomManifestRequest);

            //if (!result.IsValid)
            //{
            //    return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
            //}
            var data = await _ecomManager.GenerateManifest(FleetId, EcomManifestRequest);

            if (data != null && data.Count > 0 && data[0].AwbNumber == 0)
            {
                return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = data });
            }
            return CreatedAtAction(nameof(GenerateManifest), new { AwbNumber = data![0].AwbNumber }, data);
        }


        [HttpPost]
        [Route("RescheduleOrCancelAppointment")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RescheduleOrCancelAppointmentResponse>), StatusCodes.Status201Created)]
        public async Task<IActionResult> RescheduleOrCancelAppointment(int FleetId, [FromBody] AdditionalInformation EcomRescheduleOrCancelRequest)
        {
            //ValidationResult result = await _rescheduleOrCancelAppointmentRequestValidator.ValidateAsync(rescheduleOrCancelAppointment);

            //if (!result.IsValid)
            //{
            //    return BadRequest(new ErrorMessage { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
            //}
            var data = await _ecomManager.RescheduleOrCancelAppointment(FleetId, EcomRescheduleOrCancelRequest);
            if (data != null && data.Count > 0 && !data[0].Status)
            {
                return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = data });
            }
            return CreatedAtAction(nameof(RescheduleOrCancelAppointment), new { AwbNumber = data[0].Awb }, data);
        }


        [HttpPost]
        [Route("PushShipmentStatus")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PushShipmentTrackResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> PushShipmentStatus(PushShipmentStatusRequest pushShipmentStatusRequest)
        {
            //ValidationResult result = await _pushShipmentStatusRequest.ValidateAsync(pushShipmentStatusRequest);

            //if (!result.IsValid)
            //{
            //    return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
            //}
            var data = await _ecomManager.PushShipmentTrack(pushShipmentStatusRequest);
            if (data != null && !data.Status)
            {
                return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = data });
            }
            return CreatedAtAction(nameof(PushShipmentStatus), new { Status = data.Status }, data);
        }
    }
}
