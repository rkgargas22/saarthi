using Tmf.Saarthi.Infrastructure.Models.Request.Login;
using Tmf.Saarthi.Infrastructure.Models.Response.Login;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface ILoginRepository
{
    Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequestModel);
    Task<EmployeeMasterResponseModel> EmployeeLoginAsync(LoginRequestModel loginRequestModel);
}
