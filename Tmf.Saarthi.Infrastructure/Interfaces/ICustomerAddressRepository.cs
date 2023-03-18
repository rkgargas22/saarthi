using Tmf.Saarthi.Infrastructure.Models.Request.Customer;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface ICustomerAddressRepository
{
    Task<CustomerAddressResponseModel> GetCustomerAddresses(long bpNumber);
    Task<CustomerAddressResponseModel> AddCustomerAddress(CustomerAddressRequestModel customerAddressRequestModel);
    Task<CustomerDataResponseModel> GetCustomerData(long fleetId);
    Task<CustomerAddressResponseModel> GetChangedAddress(long bpNumber);
}
