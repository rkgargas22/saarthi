using Tmf.Saarthi.Infrastructure.Models.Request.CustomerConsent;
using Tmf.Saarthi.Infrastructure.Models.Response.CustomerConsent;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface ICustomerConsentRepository
{
    Task<CustomerConsentResponseModel> GenerateCustomerConsent(CustomerConsentRequestModel customerConsentRequest);
    Task<CustomerConsentDocumentByFleetResponseModel> GetCustomerConsentLetterByFleetId(long FleetId, string Documenttype);
}
