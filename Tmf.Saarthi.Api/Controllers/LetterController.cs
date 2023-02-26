using Tmf.Saarthi.Core.ResponseModels.CustomerConsent;
using Tmf.Saarthi.Core.ResponseModels.FuelLoanAgreement;
using Tmf.Saarthi.Core.ResponseModels.ProvisionalLette;
using Tmf.Saarthi.Core.ResponseModels.SanctionLetter;

namespace Tmf.Saarthi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LetterController : ControllerBase
{
    private readonly ICustomerConsentManager _customerConsentManager;
    private readonly ISanctionLetterManager _sanctionLetterManager;  
    private readonly IFuelLoanAggrementManager _fuelLoanAggrementManager;
    private readonly IProvisionalLetterManager _provisionalLetterManager;
    public LetterController(ICustomerConsentManager customerConsentManager, 
        ISanctionLetterManager sanctionLetterManager,
        IFuelLoanAggrementManager fuelLoanAggrementManager,
        IProvisionalLetterManager provisionalLetterManager
        )
    {
        _customerConsentManager = customerConsentManager;
        _sanctionLetterManager = sanctionLetterManager;
        _fuelLoanAggrementManager = fuelLoanAggrementManager;
        _provisionalLetterManager = provisionalLetterManager;
    }

    [Route("CustomerConsent")]
    [HttpGet]
    [ProducesDefaultResponseType(typeof(CustomerConsentResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomerConsentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CustomerConsent()
    {
        CustomerConsentResponse customerConsentResponse = await _customerConsentManager.GenerateCustomerConsent();
        if (string.IsNullOrEmpty(customerConsentResponse.Letter))
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.DataNotAvailable, Error = ValidationMessages.IdNotFound });
        }

        return CreatedAtAction(nameof(CustomerConsent), null, customerConsentResponse);
    }

    [Route("Sanction/{FleetID}")]
    [HttpGet]
    [ProducesDefaultResponseType(typeof(SanctionLetterResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SanctionLetterResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Sanction([FromRoute] long FleetID)
    {
        SanctionLetterResponse sanctionLetterResponse = await _sanctionLetterManager.GenerateSanctionLetter(FleetID);
        if (string.IsNullOrEmpty(sanctionLetterResponse.Letter))
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.DataNotAvailable, Error = ValidationMessages.IdNotFound });
        }

        return Ok(sanctionLetterResponse);
    }

    [Route("LoanAgreement/{FleetID}")]
    [HttpGet]
    [ProducesDefaultResponseType(typeof(FuelLoanAgreementResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(FuelLoanAgreementResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> LoanAgreement([FromRoute] long FleetID)
    {
        FuelLoanAgreementResponse fuelLoanAgreementResponse = await _fuelLoanAggrementManager.GenerateFuelLoanAgreement(FleetID);
        if (string.IsNullOrEmpty(fuelLoanAgreementResponse.Letter))
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.DataNotAvailable, Error = ValidationMessages.IdNotFound });
        }

        return Ok(fuelLoanAgreementResponse);
    }

    [HttpGet("{FleetId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<CustomerConsentDocumentByFleetResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute] long FleetId, string Documenttype)
    {
        CustomerConsentDocumentByFleetResponse customerConsentDocumentByFleet = await _customerConsentManager.GetCustomerConsentLetterByFleetId(FleetId, Documenttype);
        if (customerConsentDocumentByFleet.FleetId == 0)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = "DB Error" });
        }

        return Ok(customerConsentDocumentByFleet);
    }

    [Route("ProvisionalLetter/{FleetID}")]
    [HttpGet]
    [ProducesDefaultResponseType(typeof(ProvisionalLetteResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProvisionalLetteResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ProvisionalLetter([FromRoute] long FleetID)
    {
        ProvisionalLetteResponse provisionalLetteResponse = await _provisionalLetterManager.GenerateprovisionalLetter(FleetID);
        if (string.IsNullOrEmpty(provisionalLetteResponse.Letter))
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.DataNotAvailable, Error = ValidationMessages.IdNotFound });
        }

        return Ok(provisionalLetteResponse);
    }
}
