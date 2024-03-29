﻿using Tmf.Saarthi.Core.RequestModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.Fleet;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FleetController : ControllerBase
{
    private readonly IFleetManager _fleetManager;
    private readonly IValidator<ProvisionApprovalRequest> _provisionApprovalValidator;
    private readonly IValidator<SanctionApprovalRequest> _sanctionApprovalValidator;
    private readonly IValidator<EAgreementApprovalRequest> _eAgreementApprovalValidator;
    private readonly IValidator<CommentRequest> _commentValidator;
    private readonly IValidator<AdditionalInformationRequest> _additionalInformationValidator;
    private readonly IValidator<AddressChangeRequest> _addressChangeValidator;
    public FleetController(IFleetManager fleetManager, IValidator<ProvisionApprovalRequest> provisionApprovalValidator, IValidator<SanctionApprovalRequest> sanctionApprovalValidator, IValidator<EAgreementApprovalRequest> eAgreementApprovalValidator, IValidator<CommentRequest> commentValidator, IValidator<AdditionalInformationRequest> additionalInformationValidator, IValidator<AddressChangeRequest> addressChangeValidator)
    {
        _fleetManager = fleetManager;
        _provisionApprovalValidator = provisionApprovalValidator;
        _sanctionApprovalValidator = sanctionApprovalValidator;
        _eAgreementApprovalValidator = eAgreementApprovalValidator;
        _commentValidator = commentValidator;
        _additionalInformationValidator = additionalInformationValidator;
        _addressChangeValidator = addressChangeValidator;
    }

    // GET api/<FleetController>/5
    [HttpGet("{BPNumber}")]
    [ProducesDefaultResponseType(typeof(GetFleetResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GetFleetResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute] long BPNumber)
    {
        GetFleetResponse getFleetResponse = await _fleetManager.GetByBPNumber(BPNumber);
        if(getFleetResponse != null && getFleetResponse.FleetID == 0)
        {
            getFleetResponse = await _fleetManager.Add(BPNumber);
            if(getFleetResponse.FleetID == 0)
            {
                return BadRequest(new ErrorResponse { Message = "Invalid BPNumber", Error = "DB Error" });
            }
            return Ok(getFleetResponse);
        }

        return Ok(getFleetResponse);
    }

    [HttpGet("verify/{id}")]
    [ProducesDefaultResponseType(typeof(VerifyFleetResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VerifyFleetResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Verify([FromRoute] long id)
    {
        VerifyFleetResponse verifyFleetResponse = await _fleetManager.Verify(id);
        if(verifyFleetResponse != null && verifyFleetResponse.FleetID == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = ValidationMessages.IdNotFound });
        }
        return Ok(verifyFleetResponse);
    }

    [HttpPatch]
    [Route("ApproveProvision/{FleetID}")]
    [ProducesDefaultResponseType(typeof(ProvisionApprovalResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProvisionApprovalResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ApproveProvision([FromRoute] long FleetID , [FromBody] ProvisionApprovalRequest provisionApprovalRequest)
    {
        ValidationResult validationResult = await _provisionApprovalValidator.ValidateAsync(provisionApprovalRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        ProvisionApprovalResponse provisionApprovalResponse = await _fleetManager.ProvisionApproval(FleetID ,provisionApprovalRequest);

        return Ok(provisionApprovalResponse);
    }

    [HttpPatch]
    [Route("ApproveSanction/{FleetID}")]
    [ProducesDefaultResponseType(typeof(SanctionApprovalResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SanctionApprovalResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ApproveSanction([FromRoute] long FleetID, [FromBody] SanctionApprovalRequest sanctionApprovalRequest)
    {
        ValidationResult validationResult = await _sanctionApprovalValidator.ValidateAsync(sanctionApprovalRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        SanctionApprovalResponse sanctionApprovalResponse = await _fleetManager.SanctionApproval(FleetID, sanctionApprovalRequest);

        return Ok(sanctionApprovalResponse);
    }

    [HttpPatch]
    [Route("ApproveEAgreement/{FleetID}")]
    [ProducesDefaultResponseType(typeof(EAgreementApprovalResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(EAgreementApprovalResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ApproveEAgreement([FromRoute] long FleetID, [FromBody] EAgreementApprovalRequest eAgreementApprovalRequest)
    {
        ValidationResult validationResult = await _eAgreementApprovalValidator.ValidateAsync(eAgreementApprovalRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        EAgreementApprovalResponse eAgreementApprovalResponse = await _fleetManager.EAgreementApproval(FleetID, eAgreementApprovalRequest);
        
        return Ok(eAgreementApprovalResponse);
    }

    [HttpPost]
    [Route("AddComment")]
    [ProducesDefaultResponseType(typeof(CommentResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommentResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddComment([FromBody] CommentRequest addCommentRequest)
    {
        ValidationResult validationResult = await _commentValidator.ValidateAsync(addCommentRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        CommentResponse commentResponse = await _fleetManager.AddComment(addCommentRequest);
        if (commentResponse != null && commentResponse.CommentId == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.InsertFailed, Error = commentResponse.Message });
        }

        return CreatedAtAction(nameof(AddComment), null, commentResponse);
    }

    [HttpPost]
    [Route("AddAdditionalInfo")]
    [ProducesDefaultResponseType(typeof(AdditionalInformationResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AdditionalInformationResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAdditionalInfo([FromBody] AdditionalInformationRequest addAdditionalInformationRequest)
    {
        ValidationResult validationResult = await _additionalInformationValidator.ValidateAsync(addAdditionalInformationRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        AdditionalInformationResponse additionalInformationResponse = await _fleetManager.AddAdditionalInformation(addAdditionalInformationRequest);
        if(additionalInformationResponse != null && additionalInformationResponse.AdditionalInfoId == 0) 
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.InsertFailed, Error = additionalInformationResponse.Message });
        }

        return CreatedAtAction(nameof(AddAdditionalInfo), null,additionalInformationResponse);
    }


    [HttpPatch]
    [Route("AddressChange/{FleetID}")]
    [ProducesDefaultResponseType(typeof(AddressChangeResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AddressChangeResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddressChange([FromRoute] long FleetID, [FromBody] AddressChangeRequest addressChangeRequest)
    {
        ValidationResult validationResult = await _addressChangeValidator.ValidateAsync(addressChangeRequest);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = validationResult.Errors.Select(m => m.ErrorMessage) });
        }

        AddressChangeResponse addressChangeResponse = await _fleetManager.AddressChange(FleetID, addressChangeRequest);

        return Ok(addressChangeResponse);
    }

    [HttpGet]
    [Route("GetDepartmentType")]
    [ProducesDefaultResponseType(typeof(List<GetDepartmentListResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<GetDepartmentListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDepartmentType([FromQuery] GetDepartmentListRequest getDepartmentListRequest)
    {
        List<GetDepartmentListResponse> getDepartmentListResponses = await _fleetManager.GetDepartmentList(getDepartmentListRequest);

        return Ok(getDepartmentListResponses);
    }

    [HttpGet]
    [Route("GetAdditionalInfoData")]
    [ProducesDefaultResponseType(typeof(List<GetAdditionalInfoResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<GetAdditionalInfoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdditionalInfoData([FromQuery] GetAdditionalInfoRequest getAdditionalInfoData)
    {
        List<GetAdditionalInfoResponse> getAdditionalInfoResponses = await _fleetManager.GetAdditionalInfo(getAdditionalInfoData);

        return Ok(getAdditionalInfoResponses);
    }

}
