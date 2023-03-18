using Tmf.Saarthi.Core.RequestModels.Customer;
using Tmf.Saarthi.Core.ResponseModels.Customer;
using Tmf.Saarthi.Core.ResponseModels.Natch;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface ICustomerManager
{
    Task<CustomerResponse> AddCustomer(string mobileNo);
    Task<CustomerResponse> GetCustomerByMobileNo(string mobileNo);
    Task<CustomerResponse> GetCustomerByBPNumber(long BPNumber);
    Task<CustomerAddressResponse> GetCustomerAddresses(long bpNumber);
    Task<CustomerAddressResponse> AddCustomerAddress(CustomerAddressRequest customerAddressRequest);
    Task<CustomerDataResponse> GetCustomerData(long fleetId);
    Task<CustomerAddressResponse> GetChangedAddress(long bpNumber);
    Task<List<DropResponse>> GetDocumentType();
}
