using Tmf.Saarthi.Infrastructure.Models.Request.Customer;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface ICustomerRepository
{
    Task<CustomerResponseModel> AddCustomer(CustomerRequestModel customerRequestModel);
    Task<CustomerResponseModel> GetCustomerByMobileNo(string mobileNo);
    Task<CustomerResponseModel> GetCustomerByBPNumber(long BPNumber);
}
