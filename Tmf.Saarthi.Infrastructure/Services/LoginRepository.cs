using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Login;
using Tmf.Saarthi.Infrastructure.Models.Response.Login;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class LoginRepository : ILoginRepository
{
    private readonly IHttpService _httpService;
    private readonly LoginOptions _loginOptions;
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public LoginRepository(IHttpService httpService, IOptions<LoginOptions> loginOptions, ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _httpService = httpService;
        _loginOptions = loginOptions.Value;
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequestModel)
    {
        //var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        //Dictionary<string, string> headers = new Dictionary<string, string>();
        //headers.Add("BpNo", "1");
        //headers.Add("UserType", "User");
        //headers.Add("OtpType", "dd");

        //JsonDocument result = await _httpService.PostAsync<LoginRequestModel>(_loginOptions.BaseUrl + _loginOptions.LoginWithUserId, loginRequestModel, headers);

        //return JsonSerializer.Deserialize<LoginResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();

        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("Username", loginRequestModel.Username),
            new SqlParameter("Password", loginRequestModel.Password)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getCustomerByUserNamePassword", parameters);

        LoginResponseModel loginResponseModel = new LoginResponseModel();

        if (dt.Rows.Count > 0)
        {
            loginResponseModel.BPNumber = (long)dt.Rows[0]["BPNumber"];
            loginResponseModel.BPType = (string)dt.Rows[0]["BPType"];
            loginResponseModel.Title = dt.Rows[0]["Title"] == DBNull.Value ? "" : (string)dt.Rows[0]["Title"];
            loginResponseModel.FirstName = dt.Rows[0]["FirstName"] == DBNull.Value ? "" : (string)dt.Rows[0]["FirstName"];
            loginResponseModel.MiddleName = dt.Rows[0]["MiddleName"] == DBNull.Value ? "" : (string)dt.Rows[0]["MiddleName"];
            loginResponseModel.LastName = dt.Rows[0]["LastName"] == DBNull.Value ? "" : (string)dt.Rows[0]["LastName"];
            loginResponseModel.MobileNo = dt.Rows[0]["MobileNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["MobileNo"];
            loginResponseModel.EmailID = dt.Rows[0]["EmailID"] == DBNull.Value ? "" : (string)dt.Rows[0]["EmailID"];
            loginResponseModel.Status = (bool)dt.Rows[0]["Status"];
            loginResponseModel.GStnNo = dt.Rows[0]["GStnNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["GStnNo"];
            loginResponseModel.Gender = dt.Rows[0]["Gender"] == DBNull.Value ? "" : (string)dt.Rows[0]["Gender"];
            loginResponseModel.NoOfVehicleOwned = dt.Rows[0]["NoOfVehicleOwned"] == DBNull.Value ? "" : (string)dt.Rows[0]["NoOfVehicleOwned"];
            loginResponseModel.Dob = (DateTime)dt.Rows[0]["Dob"];
            loginResponseModel.PanNo = dt.Rows[0]["PanNo"] != DBNull.Value ? (string)dt.Rows[0]["PanNo"] : string.Empty;
            loginResponseModel.CustomerType = dt.Rows[0]["CustomerType"] != DBNull.Value ? (string)dt.Rows[0]["CustomerType"] : string.Empty;
        }

        return loginResponseModel;
    }

    public async Task<EmployeeMasterResponseModel> EmployeeLoginAsync(LoginRequestModel loginRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("Username", loginRequestModel.Username),
            new SqlParameter("Password", loginRequestModel.Password)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetEmployeeByUseridAndPassword", parameters);

        EmployeeMasterResponseModel employeeMasterResponse = new EmployeeMasterResponseModel();

        if (dt.Rows.Count > 0)
        {
            employeeMasterResponse.BPNumber = (long)dt.Rows[0]["BPNumber"];
            employeeMasterResponse.UserName = (string)dt.Rows[0]["UserName"];
            employeeMasterResponse.Password = (string)dt.Rows[0]["Password"];
            employeeMasterResponse.FirstName = (string)dt.Rows[0]["FirstName"];
            employeeMasterResponse.MiddleName = dt.Rows[0]["MiddleName"] == DBNull.Value ? "" : (string)dt.Rows[0]["MiddleName"];
            employeeMasterResponse.LastName = (string)dt.Rows[0]["LastName"];
            employeeMasterResponse.MobileNo = (string)dt.Rows[0]["MobileNo"];
            employeeMasterResponse.EmailID = (string)dt.Rows[0]["EmailID"];
            employeeMasterResponse.DefaultRole = (string)dt.Rows[0]["DefaultRole"];
            employeeMasterResponse.IsActive = (bool)dt.Rows[0]["IsActive"];
            employeeMasterResponse.RoleId = (long)dt.Rows[0]["RoleId"];
        }

        return employeeMasterResponse;
    }
}
