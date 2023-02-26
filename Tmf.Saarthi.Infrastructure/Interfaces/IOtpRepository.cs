using Tmf.Saarthi.Infrastructure.Models.Request.Otp;
using Tmf.Saarthi.Infrastructure.Models.Response.Otp;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IOtpRepository
{
    Task<OtpResponseModel> SendOtpAsync(OtpRequestModel otpRequestModel);
    Task<VerifyOtpResponseModel> VerifyOtpAsync(VerifyOtpRequestModel verifyOtpRequestModel);
}
