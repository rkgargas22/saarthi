using Microsoft.AspNetCore.Authorization;
using Tmf.Saarthi.Core.RequestModels.Customer;
using Tmf.Saarthi.Core.ResponseModels.Customer;
using Tmf.Saarthi.Core.ResponseModels.Natch;

namespace Tmf.Saarthi.Api.Controllers;

 
[Route("api/[controller]")]
[ApiController]
public class CustomerAddressController : ControllerBase
{
    private readonly IValidator<CustomerAddressRequest> _customerAddressRequestValidator;
    private readonly ILogger _logger;
    private readonly ICustomerManager _customerManager;

    public CustomerAddressController(ILogger<CustomerAddressController> logger, ICustomerManager customerManager, IValidator<CustomerAddressRequest> customerAddressRequestValidator)
    {
        _logger = logger;
        _customerManager = customerManager;
        _customerAddressRequestValidator = customerAddressRequestValidator;
    }


    [HttpPost]
    [ProducesDefaultResponseType(typeof(CustomerAddressResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomerAddressResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] CustomerAddressRequest customerAddressRequest)
    {
        ValidationResult result = await _customerAddressRequestValidator.ValidateAsync(customerAddressRequest);

        if (!result.IsValid)
        {
            return BadRequest(new ErrorResponse { Message = ValidationMessages.GeneralValidationErrorMessage, Error = result.Errors.Select(m => m.ErrorMessage) });
        }
        _logger.LogInformation("Login start");

        CustomerAddressResponse customerAddressResponse = await _customerManager.AddCustomerAddress(customerAddressRequest);
        _logger.LogInformation("Login end");

        return CreatedAtAction(nameof(Post), new { customerAddressResponse.AddressID }, customerAddressResponse);
    }

    [HttpGet("{bpNumber}")]
    [ProducesDefaultResponseType(typeof(CustomerAddressResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomerAddressResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute] long bpNumber)
    {
        CustomerAddressResponse customerAddressResponse = await _customerManager.GetCustomerAddresses(bpNumber);

        return Ok(customerAddressResponse);
    }

    [HttpGet("OldAddress/{bpNumber}")]
    [ProducesDefaultResponseType(typeof(CustomerAddressResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomerAddressResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChangedAddress([FromRoute] long bpNumber)
    {
        CustomerAddressResponse customerAddressResponse = await _customerManager.GetChangedAddress(bpNumber);

        return Ok(customerAddressResponse);
    }

    [HttpGet("CustomerData/{FleetId}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomerDataResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerData(long FleetId)
    {
        CustomerDataResponse adminFleetResponse = await _customerManager.GetCustomerData(FleetId);
        return Ok(adminFleetResponse);
    }

    [HttpGet("DocumentType")]
    [ProducesDefaultResponseType(typeof(List<DropResponse>))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<DropResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDocumentType()
    {
        List<DropResponse> dropResponse = await _customerManager.GetDocumentType();

        return Ok(dropResponse);
    }
}
