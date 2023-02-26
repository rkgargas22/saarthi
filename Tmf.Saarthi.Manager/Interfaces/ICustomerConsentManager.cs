using Tmf.Saarthi.Core.ResponseModels.CustomerConsent;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface ICustomerConsentManager
{
    Task<CustomerConsentResponse> GenerateCustomerConsent();
    Task<CustomerConsentDocumentByFleetResponse> GetCustomerConsentLetterByFleetId(long FleetId, string Documenttype);
}
