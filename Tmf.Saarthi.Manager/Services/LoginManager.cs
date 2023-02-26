using Tmf.Saarthi.Core.Constants;
using Tmf.Saarthi.Core.RequestModels.Login;
using Tmf.Saarthi.Core.ResponseModels.Login;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Login;
using Tmf.Saarthi.Infrastructure.Models.Response.Login;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class LoginManager : ILoginManager
{
    private readonly ILoginRepository _loginRepository;
    public LoginManager(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
    {
        LoginResponse loginResponse = new LoginResponse();
        LoginRequestModel loginRequestModel = new LoginRequestModel();
        loginRequestModel.Username = loginRequest.Username!;
        loginRequestModel.Password = loginRequest.Password!;
        LoginResponseModel loginResponseModel = await _loginRepository.LoginAsync(loginRequestModel);
        loginResponse.BpNo = loginResponseModel.BpNo;
        loginResponse.MobileNo = loginResponseModel.MobileNo;
        loginResponse.Email = loginResponseModel.Email;
        loginResponse.UserId = loginResponseModel.UserName;

        return loginResponse;
    }
    public async Task<EmployeeLoginResponse> EmployeeLoginAsync(LoginRequest loginRequest)
    {
        EmployeeLoginResponse employeeLoginResponse = new EmployeeLoginResponse();
        LoginRequestModel loginRequestModel = new LoginRequestModel();
        loginRequestModel.Username = loginRequest.Username!;
        loginRequestModel.Password = loginRequest.Password!;
        // will call sso api here
        bool ssoResult = true;
        if (ssoResult)
        {
            EmployeeMasterResponseModel employeeMasterResponseModel = await _loginRepository.EmployeeLoginAsync(loginRequestModel);
            employeeLoginResponse.BPNumber = employeeMasterResponseModel.BPNumber;
            employeeLoginResponse.UserName = employeeMasterResponseModel.UserName;
            employeeLoginResponse.FirstName = employeeMasterResponseModel.FirstName;
            employeeLoginResponse.MiddleName = employeeMasterResponseModel.MiddleName;
            employeeLoginResponse.LastName = employeeMasterResponseModel.LastName;
            employeeLoginResponse.MobileNo = employeeMasterResponseModel.MobileNo;
            employeeLoginResponse.EmailID = employeeMasterResponseModel.EmailID;
            employeeLoginResponse.Role = employeeMasterResponseModel.DefaultRole;
        }


        return employeeLoginResponse;
    }
}
