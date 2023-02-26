using Tmf.Saarthi.Infrastructure.Models.Response.Customer;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface ICustomerPreApprovedRepository
{    Task<CustomerPreApprovedResponseModel> GetCustomerByMobileNo(string mobileNo);
}
