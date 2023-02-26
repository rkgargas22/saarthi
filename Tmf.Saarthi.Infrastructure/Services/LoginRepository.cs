using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Login;
using Tmf.Saarthi.Infrastructure.Models.Response.Fleet;
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
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");
        headers.Add("OtpType", "dd");

        JsonDocument result = await _httpService.PostAsync<LoginRequestModel>(_loginOptions.BaseUrl + _loginOptions.LoginWithUserId, loginRequestModel, headers);

        return JsonSerializer.Deserialize<LoginResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
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
        }

        return employeeMasterResponse;
    }
}
