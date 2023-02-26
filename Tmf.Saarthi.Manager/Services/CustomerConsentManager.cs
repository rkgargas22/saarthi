using Tmf.Saarthi.Core.ResponseModels.CustomerConsent;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.CustomerConsent;
using Tmf.Saarthi.Infrastructure.Models.Response.CustomerConsent;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class CustomerConsentManager : ICustomerConsentManager
{
    private readonly ICustomerConsentRepository _customerConsentRepository;
    private readonly IFleetManager _fleetManager;
    public CustomerConsentManager(ICustomerConsentRepository customerConsentRepository, IFleetManager fleetManager)
    {
        _customerConsentRepository = customerConsentRepository;
        _fleetManager = fleetManager;
    }
    public async Task<CustomerConsentResponse> GenerateCustomerConsent()
    {
        //LetterMasterDataResponse letterMasterDataResponse = await _fleetManager.LetterMasterData(FleetID);

        CustomerConsentResponse customerConsentResponse = new CustomerConsentResponse();

        CustomerConsentRequestModel customerConsentRequestModel = new CustomerConsentRequestModel();
        customerConsentRequestModel.Borrower = "A";
        customerConsentRequestModel.CoBorrower = "B";

        CustomerConsentResponseModel customerConsentResponseModel = await _customerConsentRepository.GenerateCustomerConsent(customerConsentRequestModel);

        customerConsentResponse.Letter = customerConsentResponseModel.Letter;

        //if (letterMasterDataResponse != null && !string.IsNullOrEmpty(letterMasterDataResponse.BorrowerName))
        //{
        //    CustomerConsentRequestModel customerConsentRequestModel = new CustomerConsentRequestModel();
        //    customerConsentRequestModel.Borrower = letterMasterDataResponse.BorrowerName;
        //    customerConsentRequestModel.CoBorrower = letterMasterDataResponse.CoBorrowerName;

        //    CustomerConsentResponseModel customerConsentResponseModel = await _customerConsentRepository.GenerateCustomerConsent(customerConsentRequestModel);

        //    customerConsentResponse.Letter = customerConsentResponseModel.Letter;
        //}

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
}
