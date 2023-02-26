using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class CustomerPreApprovedRepository : ICustomerPreApprovedRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public CustomerPreApprovedRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<CustomerPreApprovedResponseModel> GetCustomerByMobileNo(string mobileNo)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("mobileNo", mobileNo)           
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_get_customerPreApproved_mobileNo", parameters);

        CustomerResponseModel customerResponse = new CustomerResponseModel();
        CustomerPreApprovedResponseModel customerPreApprovedResponseModel = new CustomerPreApprovedResponseModel();
        if (dt.Rows.Count > 0)
        {
            customerPreApprovedResponseModel.PreApprovedID = (long)dt.Rows[0]["PreApprovedID"];
            customerPreApprovedResponseModel.BPNumber = (long)dt.Rows[0]["BPNumber"];
            customerPreApprovedResponseModel.FirstName = dt.Rows[0]["FirstName"] == DBNull.Value ? "" : (string)dt.Rows[0]["FirstName"]; 
            customerPreApprovedResponseModel.MiddleName = dt.Rows[0]["MiddleName"] == DBNull.Value ? "" : (string)dt.Rows[0]["MiddleName"];
            customerPreApprovedResponseModel.LastName = dt.Rows[0]["LastName"] == DBNull.Value ? "" : (string)dt.Rows[0]["LastName"];
            customerPreApprovedResponseModel.Mobile = (string)dt.Rows[0]["Mobile"];
            customerPreApprovedResponseModel.Address = (string)dt.Rows[0]["Address"];
            customerPreApprovedResponseModel.EmailID = (string)dt.Rows[0]["EmailID"];
            customerPreApprovedResponseModel.SharedLink = dt.Rows[0]["ShareLink"] == DBNull.Value ? "" : (string)dt.Rows[0]["ShareLink"];
            customerPreApprovedResponseModel.Days = dt.Rows[0]["Days"] == DBNull.Value ? "" : (string)dt.Rows[0]["Days"];
            customerPreApprovedResponseModel.CreatedDate = (DateTime)dt.Rows[0]["CreatedDate"];
            customerPreApprovedResponseModel.UpdatedDate = dt.Rows[0]["UpdatedDate"] == DBNull.Value ? null : (DateTime)dt.Rows[0]["UpdatedDate"];

        }

        return customerPreApprovedResponseModel;
    }

}
