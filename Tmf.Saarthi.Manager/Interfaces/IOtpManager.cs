using Tmf.Saarthi.Core.RequestModels.Otp;
using Tmf.Saarthi.Core.ResponseModels.Otp;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IOtpManager
{
    Task<OtpResponse> SendOtpAsync(OtpRequest otpRequest);

    Task<VerifyOtpResponse> VerifyOtpAsync(VerifyOtpRequest verifyOtpRequest);

    Task<OtpLogResponse> OtpLog(OtpLogRequest otpLogRequest);

    Task<bool> GetOTPDetailByReqId(string reqId);
}
