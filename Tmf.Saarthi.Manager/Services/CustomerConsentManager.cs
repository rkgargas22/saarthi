using Microsoft.AspNetCore.Hosting;
using Tmf.Saarthi.Core.ResponseModels.CustomerConsent;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.CustomerConsent;
using Tmf.Saarthi.Infrastructure.Models.Response.CustomerConsent;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class CustomerConsentManager : ICustomerConsentManager
{
    private const string TemplatesFolderName = "Templates";
    private const string LetterHtmlFileName = "CustomerConsent.html";

    private readonly ICustomerConsentRepository _customerConsentRepository;
    private readonly IFleetManager _fleetManager;
    private readonly IHostingEnvironment _environment;

    public CustomerConsentManager(ICustomerConsentRepository customerConsentRepository, IFleetManager fleetManager, IHostingEnvironment environment)
    {
        _customerConsentRepository = customerConsentRepository;
        _fleetManager = fleetManager;
        _environment = environment;
    }
    public async Task<CustomerConsentResponse> GenerateCustomerConsent()
    {
        //LetterMasterDataResponse letterMasterDataResponse = await _fleetManager.LetterMasterData(FleetID);

        string templatePath = Path.Combine(_environment.WebRootPath, TemplatesFolderName, LetterHtmlFileName);
        byte[] templateBytes = File.ReadAllBytes(templatePath);
        string base64String = Convert.ToBase64String(templateBytes);
        Dictionary<string, string> mappingProperties = GetMappingProperties();

        CustomerConsentRequestModel customerConsentRequestModel = new()
        {
            MappingProperties = mappingProperties,
            Htmlbase64String = base64String
        };

        CustomerConsentResponseModel customerConsentResponseModel = await _customerConsentRepository.GenerateCustomerConsent(customerConsentRequestModel);

        CustomerConsentResponse customerConsentResponse = new()
        {
            Letter = customerConsentResponseModel.Letter
        };

        return customerConsentResponse;
    }

    public async Task<CustomerConsentDocumentByFleetResponse> GetCustomerConsentLetterByFleetId(long FleetId, string Documenttype)
    {
        CustomerConsentDocumentByFleetResponseModel customerConsentDocumentByFleetResponse = await _customerConsentRepository.GetCustomerConsentLetterByFleetId(FleetId, Documenttype);

        CustomerConsentDocumentByFleetResponse customerConsentDocumentByFleet = new CustomerConsentDocumentByFleetResponse();
        customerConsentDocumentByFleet.FleetId = customerConsentDocumentByFleetResponse.FleetId;
        customerConsentDocumentByFleet.DocumentUrl = customerConsentDocumentByFleetResponse.DocumentUrl;
        customerConsentDocumentByFleet.CreatedBy = customerConsentDocumentByFleetResponse.CreatedBy;
        customerConsentDocumentByFleet.IsActive = customerConsentDocumentByFleetResponse.IsActive;
        customerConsentDocumentByFleet.Documenttype = customerConsentDocumentByFleetResponse.Documenttype;

        return customerConsentDocumentByFleet;
    }

    private Dictionary<string, string> GetMappingProperties()
    {
        Dictionary<string, string> mappingProperties = new()
        {
            { "##Borrower", "A" },
            { "##CoBorrower", "B"},
        };

        return mappingProperties;
    }
}
