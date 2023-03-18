using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Customer;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class CustomerAddressRepository : ICustomerAddressRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public CustomerAddressRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<CustomerAddressResponseModel> GetCustomerAddresses(long bpNumber)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("BPNumber", bpNumber)
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetCustomerAddressesByBpNumber", parameters);

        CustomerAddressResponseModel customerAddressResponseModel = new CustomerAddressResponseModel();

        if (dt.Rows.Count > 0)
        {
            customerAddressResponseModel.AddressID = (int)dt.Rows[0]["AddressID"];
            customerAddressResponseModel.BPNumber = (long)dt.Rows[0]["BPNumber"];
            customerAddressResponseModel.Type = dt.Rows[0]["Type"] == DBNull.Value ? "" : (string)dt.Rows[0]["Type"];
            customerAddressResponseModel.AddressLine1 = dt.Rows[0]["AddressLine1"] == DBNull.Value ? "" : (string)dt.Rows[0]["AddressLine1"];
            customerAddressResponseModel.AddressLine2 = dt.Rows[0]["AddressLine2"] == DBNull.Value ? "" : (string)dt.Rows[0]["AddressLine2"];
            customerAddressResponseModel.Landmark = dt.Rows[0]["Landmark"] == DBNull.Value ? "" : (string)dt.Rows[0]["Landmark"];
            customerAddressResponseModel.City = dt.Rows[0]["City"] == DBNull.Value ? "" : (string)dt.Rows[0]["City"];
            customerAddressResponseModel.District = dt.Rows[0]["District"] == DBNull.Value ? "" : (string)dt.Rows[0]["District"];
            customerAddressResponseModel.Region = dt.Rows[0]["Region"] == DBNull.Value ? "" : (string)dt.Rows[0]["Region"];
            customerAddressResponseModel.Country = dt.Rows[0]["Country"] == DBNull.Value ? "" : (string)dt.Rows[0]["Country"];
            customerAddressResponseModel.Pincode = (int)dt.Rows[0]["Pincode"];
            customerAddressResponseModel.IsDefault = dt.Rows[0]["IsDefault"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsDefault"];
        }

        return customerAddressResponseModel;
    }

    public async Task<CustomerAddressResponseModel> GetChangedAddress(long bpNumber)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("BPNumber", bpNumber)
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetCustomerChangesAddressesByBpNumber", parameters);

        CustomerAddressResponseModel customerAddressResponseModel = new CustomerAddressResponseModel();

        if (dt.Rows.Count > 0)
        {
            customerAddressResponseModel.AddressID = (int)dt.Rows[0]["AddressID"];
            customerAddressResponseModel.BPNumber = (long)dt.Rows[0]["BPNumber"];
            customerAddressResponseModel.Type = dt.Rows[0]["Type"] == DBNull.Value ? "" : (string)dt.Rows[0]["Type"];
            customerAddressResponseModel.AddressLine1 = dt.Rows[0]["AddressLine1"] == DBNull.Value ? "" : (string)dt.Rows[0]["AddressLine1"];
            customerAddressResponseModel.AddressLine2 = dt.Rows[0]["AddressLine2"] == DBNull.Value ? "" : (string)dt.Rows[0]["AddressLine2"];
            customerAddressResponseModel.Landmark = dt.Rows[0]["Landmark"] == DBNull.Value ? "" : (string)dt.Rows[0]["Landmark"];
            customerAddressResponseModel.City = dt.Rows[0]["City"] == DBNull.Value ? "" : (string)dt.Rows[0]["City"];
            customerAddressResponseModel.District = dt.Rows[0]["District"] == DBNull.Value ? "" : (string)dt.Rows[0]["District"];
            customerAddressResponseModel.Region = dt.Rows[0]["Region"] == DBNull.Value ? "" : (string)dt.Rows[0]["Region"];
            customerAddressResponseModel.Country = dt.Rows[0]["Country"] == DBNull.Value ? "" : (string)dt.Rows[0]["Country"];
            customerAddressResponseModel.Pincode = (int)dt.Rows[0]["Pincode"];
            customerAddressResponseModel.IsDefault = dt.Rows[0]["IsDefault"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsDefault"];
        }

        return customerAddressResponseModel;

    }
    public async Task<CustomerAddressResponseModel> AddCustomerAddress(CustomerAddressRequestModel customerAddressRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("BPNumber", customerAddressRequestModel.BPNumber),
            new SqlParameter("Type", customerAddressRequestModel.Type),
            new SqlParameter("AddressLine1", customerAddressRequestModel.AddressLine1),
            new SqlParameter("AddressLine2", customerAddressRequestModel.AddressLine2),
            new SqlParameter("Landmark", customerAddressRequestModel.Landmark),
            new SqlParameter("City", customerAddressRequestModel.City),
            new SqlParameter("District", customerAddressRequestModel.District),
            new SqlParameter("Region", customerAddressRequestModel.Region),
            new SqlParameter("Country", customerAddressRequestModel.Country),
            new SqlParameter("Pincode", customerAddressRequestModel.Pincode),
            new SqlParameter("IsDefault", customerAddressRequestModel.IsDefault)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_InsertCustomerAddress", parameters);

        CustomerAddressResponseModel customerAddressResponseModel = new CustomerAddressResponseModel();
        if (dt.Rows.Count > 0)
        {
            customerAddressResponseModel.AddressID = (int)(dt.Rows[0]["AddressID"]);
            customerAddressResponseModel.BPNumber = customerAddressRequestModel.BPNumber;
            customerAddressResponseModel.Type = customerAddressRequestModel.Type;
            customerAddressResponseModel.AddressLine1 = customerAddressRequestModel.AddressLine1;
            customerAddressResponseModel.AddressLine2 = customerAddressRequestModel.AddressLine2;
            customerAddressResponseModel.Landmark = customerAddressRequestModel.Landmark;
            customerAddressResponseModel.City = customerAddressRequestModel.City;
            customerAddressResponseModel.District = customerAddressRequestModel.District;
            customerAddressResponseModel.Region = customerAddressRequestModel.Region;
            customerAddressResponseModel.Country = customerAddressRequestModel.Country;
            customerAddressResponseModel.Pincode = customerAddressRequestModel.Pincode;
            customerAddressResponseModel.IsDefault = customerAddressRequestModel.IsDefault;
        }
        return customerAddressResponseModel;
    }

    public async Task<CustomerDataResponseModel> GetCustomerData(long fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
            {
            new SqlParameter("FleetId", fleetId)
            };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getCustomerDetails", parameters);
        CustomerDataResponseModel customerDataResponseModel = new CustomerDataResponseModel();

        if (dt.Rows.Count > 0)
        {
            customerDataResponseModel.BPNumber = (Int64)dt.Rows[0]["BPNumber"];
            customerDataResponseModel.FleetID = (Int64)dt.Rows[0]["FleetID"];
            customerDataResponseModel.FirstName = dt.Rows[0]["FirstName"] == DBNull.Value ? "" : (string)dt.Rows[0]["FirstName"];
            customerDataResponseModel.MiddleName = dt.Rows[0]["MiddleName"] == DBNull.Value ? "" : (string)dt.Rows[0]["MiddleName"];
            customerDataResponseModel.LastName = dt.Rows[0]["LastName"] == DBNull.Value ? "" : (string)dt.Rows[0]["LastName"];
            customerDataResponseModel.Gender = dt.Rows[0]["Gender"] == DBNull.Value ? "" : (string)dt.Rows[0]["Gender"];
            customerDataResponseModel.Dob = dt.Rows[0]["Dob"] == DBNull.Value ? "" : (string)dt.Rows[0]["Dob"];
            customerDataResponseModel.PanNo = dt.Rows[0]["PanNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["PanNo"];
            customerDataResponseModel.FanNo = dt.Rows[0]["FanNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["FanNo"];
            customerDataResponseModel.MobileNo = dt.Rows[0]["MobileNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["MobileNo"];
        }

        return customerDataResponseModel;
    }

}
