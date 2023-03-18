using Tmf.Saarthi.Core.Constants;
using Tmf.Saarthi.Core.RequestModels.Customer;
using Tmf.Saarthi.Core.ResponseModels.Customer;
using Tmf.Saarthi.Core.ResponseModels.Natch;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Customer;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class CustomerManager : ICustomerManager
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerPreApprovedRepository _customerPreApprovedRepository;
    private readonly ICustomerAddressRepository _customerAddressRepository;
    public CustomerManager(ICustomerRepository customerRepository, ICustomerPreApprovedRepository customerPreApprovedRepository, ICustomerAddressRepository customerAddressRepository)
    {
        _customerRepository = customerRepository;
        _customerPreApprovedRepository = customerPreApprovedRepository;
        _customerAddressRepository = customerAddressRepository;
    }

    public async Task<CustomerResponse> AddCustomer(string mobileNo)
    {
        CustomerResponse customerResponse = new CustomerResponse();
        CustomerResponseModel customerResponseModel = new CustomerResponseModel();
        customerResponseModel = await _customerRepository.GetCustomerByMobileNo(mobileNo);
        if (customerResponseModel?.BPNumber == 0)
        {
            CustomerPreApprovedResponseModel customerPreApprovedResponseModel = await _customerPreApprovedRepository.GetCustomerByMobileNo(mobileNo);
            CustomerRequestModel customerRequestModel = new CustomerRequestModel();
            customerRequestModel.BPNumber = customerPreApprovedResponseModel.BPNumber;
            customerRequestModel.BPType = "User";
            customerRequestModel.PreApprovedID = customerPreApprovedResponseModel.PreApprovedID;
            customerRequestModel.AddressID = 1;

            customerRequestModel.FirstName = customerPreApprovedResponseModel.FirstName;
            customerRequestModel.MiddleName = customerPreApprovedResponseModel.MiddleName;
            customerRequestModel.LastName = customerPreApprovedResponseModel.LastName;
            customerRequestModel.Gender = "M";
            customerRequestModel.EmailID = customerPreApprovedResponseModel.EmailID;
            customerRequestModel.MobileNo = customerPreApprovedResponseModel.Mobile;
            customerRequestModel.Dob = DateTime.Now;
            customerRequestModel.LastUpdateDate = DateTime.Now;
            customerRequestModel.Status = true;

            customerResponseModel = await _customerRepository.AddCustomer(customerRequestModel);

        }

        customerResponse.BPNumber = customerResponseModel!.BPNumber;
        customerResponse.FirstName = customerResponseModel.FirstName;
        customerResponse.MiddleName = customerResponseModel.MiddleName;
        customerResponse.LastName = customerResponseModel.LastName;
        customerResponse.Gender = customerResponseModel.Gender;
        customerResponse.BPType = customerResponseModel.BPType;
        customerResponse.EmailID = customerResponseModel.EmailID;
        customerResponse.MobileNo = customerResponseModel.MobileNo;
        customerResponse.Title = customerResponseModel.Title;
        customerResponse.Status = customerResponseModel.Status;
        return customerResponse;
    }

    public async Task<CustomerResponse> GetCustomerByBPNumber(long BPNumber)
    {
        CustomerResponse customerResponse = new CustomerResponse();
        CustomerResponseModel customerResponseModel = new CustomerResponseModel();
        customerResponseModel = await _customerRepository.GetCustomerByBPNumber(BPNumber);

        customerResponse.BPNumber = customerResponseModel.BPNumber;
        customerResponse.FirstName = customerResponseModel.FirstName;
        customerResponse.MiddleName = customerResponseModel.MiddleName;
        customerResponse.LastName = customerResponseModel.LastName;
        customerResponse.Gender = customerResponseModel.Gender;
        customerResponse.BPType = customerResponseModel.BPType;
        customerResponse.EmailID = customerResponseModel.EmailID;
        customerResponse.MobileNo = customerResponseModel.MobileNo;
        customerResponse.Title = customerResponseModel.Title;
        customerResponse.Status = customerResponseModel.Status;
        customerResponse.PanNo = customerResponseModel.PanNo;
        customerResponse.CustomerType = customerResponseModel.CustomerType;
        return customerResponse;
    }

    public async Task<CustomerResponse> GetCustomerByMobileNo(string mobileNo)
    {
        CustomerResponse customerResponse = new CustomerResponse();
        CustomerResponseModel customerResponseModel = new CustomerResponseModel();
        customerResponseModel = await _customerRepository.GetCustomerByMobileNo(mobileNo);

        customerResponse.BPNumber = customerResponseModel.BPNumber;
        customerResponse.FirstName = customerResponseModel.FirstName;
        customerResponse.MiddleName = customerResponseModel.MiddleName;
        customerResponse.LastName = customerResponseModel.LastName;
        customerResponse.Gender = customerResponseModel.Gender;
        customerResponse.BPType = customerResponseModel.BPType;
        customerResponse.EmailID = customerResponseModel.EmailID;
        customerResponse.MobileNo = customerResponseModel.MobileNo;
        customerResponse.Title = customerResponseModel.Title;
        customerResponse.Status = customerResponseModel.Status;
        customerResponse.PanNo = customerResponseModel.PanNo;
        customerResponse.CustomerType = customerResponseModel.CustomerType;
        return customerResponse;
    }

    public async Task<CustomerAddressResponse> AddCustomerAddress(CustomerAddressRequest customerAddressRequest)
    {
        CustomerAddressRequestModel customerAddressRequestModel = new CustomerAddressRequestModel();
        customerAddressRequestModel.BPNumber = customerAddressRequest.BPNumber;
        customerAddressRequestModel.Type = customerAddressRequest.Type;
        customerAddressRequestModel.AddressLine1 = customerAddressRequest.AddressLine1;
        customerAddressRequestModel.AddressLine2 = customerAddressRequest.AddressLine2;
        customerAddressRequestModel.Landmark = customerAddressRequest.Landmark;
        customerAddressRequestModel.City = customerAddressRequest.City;
        customerAddressRequestModel.District = customerAddressRequest.District;
        customerAddressRequestModel.Region = customerAddressRequest.Region;
        customerAddressRequestModel.Country = customerAddressRequest.Country;
        customerAddressRequestModel.Pincode = customerAddressRequest.Pincode!.Value;
        customerAddressRequestModel.IsDefault = true;

        CustomerAddressResponseModel customerAddressResponseModel = await _customerAddressRepository.AddCustomerAddress(customerAddressRequestModel);

        CustomerAddressResponse customerAddressResponse = new CustomerAddressResponse();
        if (customerAddressResponseModel.AddressID > 0)
        {
            customerAddressResponse.AddressID = customerAddressResponseModel.AddressID;
            customerAddressResponse.BPNumber = customerAddressResponseModel.BPNumber;
            customerAddressResponse.Type = customerAddressResponseModel.Type;
            customerAddressResponse.AddressLine1 = customerAddressResponseModel.AddressLine1;
            customerAddressResponse.AddressLine2 = customerAddressResponseModel.AddressLine2;
            customerAddressResponse.Landmark = customerAddressResponseModel.Landmark;
            customerAddressResponse.City = customerAddressResponseModel.City;
            customerAddressResponse.District = customerAddressResponseModel.District;
            customerAddressResponse.Region = customerAddressResponseModel.Region;
            customerAddressResponse.Country = customerAddressResponseModel.Country;
            customerAddressResponse.Pincode = customerAddressResponseModel.Pincode;
        }

        return customerAddressResponse;
    }

    public async Task<CustomerAddressResponse> GetChangedAddress(long bpNumber)
    {
        CustomerAddressResponse customerAddressResponse = new CustomerAddressResponse();
        CustomerAddressResponseModel customerAddressResponseModel = await _customerAddressRepository.GetChangedAddress(bpNumber);
        customerAddressResponse.AddressID = customerAddressResponseModel.AddressID;
        customerAddressResponse.BPNumber = customerAddressResponseModel.BPNumber;
        customerAddressResponse.Type = customerAddressResponseModel.Type;
        customerAddressResponse.AddressLine1 = customerAddressResponseModel.AddressLine1;
        customerAddressResponse.AddressLine2 = customerAddressResponseModel.AddressLine2;
        customerAddressResponse.Landmark = customerAddressResponseModel.Landmark;
        customerAddressResponse.City = customerAddressResponseModel.City;
        customerAddressResponse.District = customerAddressResponseModel.District;
        customerAddressResponse.Region = customerAddressResponseModel.Region;
        customerAddressResponse.Country = customerAddressResponseModel.Country;
        customerAddressResponse.Pincode = customerAddressResponseModel.Pincode;

        return customerAddressResponse;
    }

    public async Task<CustomerDataResponse> GetCustomerData(long fleetId)
    {
        CustomerDataResponseModel adminFleetResponseModel = await _customerAddressRepository.GetCustomerData(fleetId);

        CustomerDataResponse customerDataResponse = new CustomerDataResponse();
        if (adminFleetResponseModel != null)
        {
            customerDataResponse.BPNumber = adminFleetResponseModel.BPNumber;
            customerDataResponse.FleetID = adminFleetResponseModel.FleetID;
            customerDataResponse.FirstName = adminFleetResponseModel.FirstName;
            customerDataResponse.MiddleName = adminFleetResponseModel.MiddleName;
            customerDataResponse.LastName = adminFleetResponseModel.LastName;
            customerDataResponse.Dob = adminFleetResponseModel.Dob;
            customerDataResponse.Gender = adminFleetResponseModel.Gender;
            customerDataResponse.PanNo = adminFleetResponseModel.PanNo;
            customerDataResponse.FanNo = adminFleetResponseModel.FanNo;
            customerDataResponse.MobileNo = adminFleetResponseModel.MobileNo;
        }
        return customerDataResponse;
    }

    public async Task<CustomerAddressResponse> GetCustomerAddresses(long bpNumber)
    {
        CustomerAddressResponse customerAddressResponse = new CustomerAddressResponse();
        CustomerAddressResponseModel customerAddressResponseModel = await _customerAddressRepository.GetCustomerAddresses(bpNumber);
        customerAddressResponse.AddressID = customerAddressResponseModel.AddressID;
        customerAddressResponse.BPNumber = customerAddressResponseModel.BPNumber;
        customerAddressResponse.Type = customerAddressResponseModel.Type;
        customerAddressResponse.AddressLine1 = customerAddressResponseModel.AddressLine1;
        customerAddressResponse.AddressLine2 = customerAddressResponseModel.AddressLine2;
        customerAddressResponse.Landmark = customerAddressResponseModel.Landmark;
        customerAddressResponse.City = customerAddressResponseModel.City;
        customerAddressResponse.District = customerAddressResponseModel.District;
        customerAddressResponse.Region = customerAddressResponseModel.Region;
        customerAddressResponse.Country = customerAddressResponseModel.Country;
        customerAddressResponse.Pincode = customerAddressResponseModel.Pincode;
        return customerAddressResponse;
    }


    public async Task<List<DropResponse>> GetDocumentType()
    {
        List<DropResponse> bankResponses = new List<DropResponse>();

        foreach (OCRDocuments enumValue in Enum.GetValues(typeof(OCRDocuments)))
        {
            DropResponse bankResponse = new DropResponse();
            bankResponse.Id = Convert.ToInt64(enumValue);
            bankResponse.DisplayName = Convert.ToString(enumValue);
            bankResponses.Add(bankResponse);
        }

        return bankResponses.OrderBy(x=> x.DisplayName).ToList();
    }
}

