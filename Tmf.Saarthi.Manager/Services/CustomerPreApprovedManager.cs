using Tmf.Saarthi.Core.ResponseModels.Customer;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class CustomerPreApprovedManager : ICustomerPreApprovedManager
{
    private readonly ICustomerPreApprovedRepository _customerPreApprovedRepository;
    public CustomerPreApprovedManager(ICustomerPreApprovedRepository customerPreApprovedRepository)
    {
        _customerPreApprovedRepository = customerPreApprovedRepository;
    }
    public async Task<CustomerResponse?> GetCustomerByMobileNo(string mobileNo)
    {
       CustomerPreApprovedResponseModel customerPreApprovedResponseModel = await _customerPreApprovedRepository.GetCustomerByMobileNo(mobileNo);
        if (customerPreApprovedResponseModel.BPNumber > 0)
        {
            CustomerResponse customerResponse = new CustomerResponse();
            customerResponse.MobileNo = customerPreApprovedResponseModel.Mobile;
            customerResponse.BPNumber = customerPreApprovedResponseModel.BPNumber;
            customerResponse.BPType = "User";
            customerResponse.FirstName = customerPreApprovedResponseModel.FirstName;
            customerResponse.MiddleName = customerPreApprovedResponseModel.MiddleName;
            customerResponse.LastName = customerPreApprovedResponseModel.LastName;
            customerResponse.EmailID = customerPreApprovedResponseModel.EmailID;
            customerResponse.NoOfVehicleOwned = "20";
            customerResponse.Dob = DateTime.Now;
            customerResponse.Status = true;
            return customerResponse;
        }

        return null;
    }
}
