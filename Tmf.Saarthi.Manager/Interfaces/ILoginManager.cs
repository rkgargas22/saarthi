using Tmf.Saarthi.Core.RequestModels.Login;
using Tmf.Saarthi.Core.ResponseModels.Login;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface ILoginManager
{
    Task<LoginResponse> LoginAsync(LoginRequest loginRequestModel);
    Task<EmployeeLoginResponse> EmployeeLoginAsync(LoginRequest loginRequest);
}
