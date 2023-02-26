using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Customer;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class CustomerRepository : ICustomerRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;
    private readonly ICustomerPreApprovedRepository _customerPreApprovedRepository;

    public CustomerRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, ICustomerPreApprovedRepository customerPreApprovedRepository)
    {
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
        _customerPreApprovedRepository = customerPreApprovedRepository;
    }

    public async Task<CustomerResponseModel> AddCustomer(CustomerRequestModel customerRequestModel)
    {
       

        List<SqlParameter> parameters = new List<SqlParameter>()
        {

            new SqlParameter("BPNumber", customerRequestModel.BPNumber),
            new SqlParameter("PreApprovedID", customerRequestModel.PreApprovedID),
            new SqlParameter("BPType", customerRequestModel.BPType),
            new SqlParameter("Title", customerRequestModel.Title),
            new SqlParameter("FirstName", customerRequestModel.FirstName),
            new SqlParameter("MiddleName", customerRequestModel.MiddleName),
            new SqlParameter("LastName", customerRequestModel.LastName),
            new SqlParameter("Telephone", customerRequestModel.Telephone),
            new SqlParameter("MobileNo", customerRequestModel.MobileNo),
            new SqlParameter("EmailID", customerRequestModel.EmailID),
            new SqlParameter("CKycNo", customerRequestModel.CKycNo),
            new SqlParameter("AadharNo", customerRequestModel.AadharNo),
            new SqlParameter("CustomerType", customerRequestModel.CustomerType),
            new SqlParameter("NoOfVehicleOwned", customerRequestModel.NoOfVehicleOwned),
            new SqlParameter("GStnNo", customerRequestModel.GStnNo),
            new SqlParameter("PanNo", customerRequestModel.PanNo),
            new SqlParameter("GroupKey", customerRequestModel.GroupKey),
            new SqlParameter("LastUpdateDate", customerRequestModel.LastUpdateDate),
            new SqlParameter("Dob", customerRequestModel.Dob),
            new SqlParameter("AddressID", customerRequestModel.AddressID),
            new SqlParameter("Status", customerRequestModel.Status),
            new SqlParameter("Gender", customerRequestModel.Gender)

};

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_insert_customer", parameters);

        CustomerResponseModel customerResponse = new CustomerResponseModel();
        if (dt.Rows.Count > 0)
        {
            customerResponse.BPNumber = customerRequestModel.BPNumber;
        }

        return customerResponse;
    }

    public async Task<CustomerResponseModel> GetCustomerByBPNumber(long BPNumber)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("BPNumber", BPNumber)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_get_customer_bpNumber", parameters);

        CustomerResponseModel customerResponse = new CustomerResponseModel();
        if (dt.Rows.Count > 0)
        {

            customerResponse.BPNumber = (long)dt.Rows[0]["BPNumber"];
            customerResponse.BPType = (string)dt.Rows[0]["BPType"];
            customerResponse.Title = dt.Rows[0]["Title"] == DBNull.Value ? "" : (string)dt.Rows[0]["Title"];
            customerResponse.FirstName = dt.Rows[0]["FirstName"] == DBNull.Value ? "" : (string)dt.Rows[0]["FirstName"];
            customerResponse.MiddleName = dt.Rows[0]["MiddleName"] == DBNull.Value ? "" : (string)dt.Rows[0]["MiddleName"];
            customerResponse.LastName = dt.Rows[0]["LastName"] == DBNull.Value ? "" : (string)dt.Rows[0]["LastName"];
            customerResponse.MobileNo = dt.Rows[0]["MobileNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["MobileNo"];
            customerResponse.EmailID = dt.Rows[0]["EmailID"] == DBNull.Value ? "" : (string)dt.Rows[0]["EmailID"];
            customerResponse.NoOfVehicleOwned = (string)dt.Rows[0]["NoOfVehicleOwned"];
            customerResponse.Status = (bool)dt.Rows[0]["Status"];
            customerResponse.GStnNo = dt.Rows[0]["GStnNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["GStnNo"];
            customerResponse.PanNo = dt.Rows[0]["PanNo"] != DBNull.Value ? (string)dt.Rows[0]["PanNo"] : string.Empty;
            customerResponse.CustomerType = dt.Rows[0]["CustomerType"] != DBNull.Value ? (string)dt.Rows[0]["CustomerType"] : string.Empty;
        }

        return customerResponse;
    }

    public async Task<CustomerResponseModel> GetCustomerByMobileNo(string mobileNo)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("mobileNo", mobileNo)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_get_customer_mobileNo", parameters);

        CustomerResponseModel customerResponse = new CustomerResponseModel();
        if (dt.Rows.Count > 0)
        {
                   
            customerResponse.BPNumber = (long)dt.Rows[0]["BPNumber"];
            customerResponse.BPType = (string)dt.Rows[0]["BPType"];
            customerResponse.Title = dt.Rows[0]["Title"] == DBNull.Value ? "" : (string)dt.Rows[0]["Title"];
            customerResponse.FirstName = dt.Rows[0]["FirstName"] == DBNull.Value ? "" : (string)dt.Rows[0]["FirstName"];
            customerResponse.MiddleName = dt.Rows[0]["MiddleName"] == DBNull.Value ? "" : (string)dt.Rows[0]["MiddleName"];
            customerResponse.LastName = dt.Rows[0]["LastName"] == DBNull.Value ? "" : (string)dt.Rows[0]["LastName"];
            customerResponse.MobileNo = dt.Rows[0]["MobileNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["MobileNo"];
            customerResponse.EmailID = dt.Rows[0]["EmailID"] == DBNull.Value ? "" : (string)dt.Rows[0]["EmailID"];
            customerResponse.Status =   (bool)dt.Rows[0]["Status"];
            customerResponse.GStnNo = dt.Rows[0]["GStnNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["GStnNo"];
            customerResponse.Gender = dt.Rows[0]["Gender"] == DBNull.Value ? "" : (string)dt.Rows[0]["Gender"];
            customerResponse.NoOfVehicleOwned = dt.Rows[0]["NoOfVehicleOwned"] == DBNull.Value ? "" : (string)dt.Rows[0]["NoOfVehicleOwned"];
            customerResponse.Dob = (DateTime)dt.Rows[0]["Dob"];
            customerResponse.PanNo = dt.Rows[0]["PanNo"] != DBNull.Value ? (string)dt.Rows[0]["PanNo"] : string.Empty;
            customerResponse.CustomerType = dt.Rows[0]["CustomerType"] != DBNull.Value ? (string)dt.Rows[0]["CustomerType"] : string.Empty;
        }

        return customerResponse;
    }

}
