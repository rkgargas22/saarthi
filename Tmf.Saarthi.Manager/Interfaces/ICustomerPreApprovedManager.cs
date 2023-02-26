using Tmf.Saarthi.Core.ResponseModels.Customer;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface ICustomerPreApprovedManager
{
    Task<CustomerResponse?> GetCustomerByMobileNo(string mobileNo);
}
