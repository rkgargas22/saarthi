using Tmf.Saarthi.Core.RequestModels.Otp;
using Tmf.Saarthi.Core.ResponseModels.Otp;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Otp;
using Tmf.Saarthi.Infrastructure.Models.Response.Otp;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class OtpManager : IOtpManager
{
    private readonly IOtpRepository _otpRepository;
    private readonly ICustomerManager _customerManager;

    public OtpManager(IOtpRepository otpRepository, ICustomerManager customerManager)
    {
        _otpRepository = otpRepository;
        _customerManager = customerManager;
    }
    public async Task<OtpResponse> SendOtpAsync(OtpRequest otpRequest)
    {
        OtpRequestModel otpRequestModel = new OtpRequestModel();
        otpRequestModel.MobileNo = otpRequest.MobileNo!;
        otpRequestModel.Type = otpRequest.Type!;
        otpRequestModel.Module = "Saarthi";

        await _customerManager.AddCustomer(otpRequest.MobileNo!);

        OtpResponseModel otpResponseModel = await _otpRepository.SendOtpAsync(otpRequestModel);

        OtpResponse otpResponse = new OtpResponse();
        otpResponse.RequestId = otpResponseModel.Data;

        return otpResponse;
    }

    public async Task<VerifyOtpResponse> VerifyOtpAsync(VerifyOtpRequest verifyOtpRequest)
    {
        VerifyOtpRequestModel verifyOtpRequestModel = new VerifyOtpRequestModel();
        verifyOtpRequestModel.Id = verifyOtpRequest.RequestId;
        verifyOtpRequestModel.Otp = verifyOtpRequest.Otp;
        VerifyOtpResponseModel verifyOtpResponseModel = await _otpRepository.VerifyOtpAsync(verifyOtpRequestModel);

        VerifyOtpResponse verifyOtpResponse = new VerifyOtpResponse();
        if (verifyOtpResponseModel.StatusCode == 0)
        {
            verifyOtpResponse.customerResponse = await _customerManager.GetCustomerByMobileNo(verifyOtpRequest.MobileNo!);
        }
      

        return verifyOtpResponse;
    }
}
