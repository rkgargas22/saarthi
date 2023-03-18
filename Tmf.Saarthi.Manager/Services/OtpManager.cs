using Microsoft.AspNetCore.Http;
using Tmf.Saarthi.Core.RequestModels.Otp;
using Tmf.Saarthi.Core.ResponseModels.Customer;
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
        OtpResponse otpResponse = new OtpResponse();

        OtpRequestModel otpRequestModel = new OtpRequestModel();
        otpRequestModel.MobileNo = otpRequest.MobileNo!;
        otpRequestModel.Type = otpRequest.Type!;
        otpRequestModel.Module = "Saarthi";

        CustomerResponse customerResponse = await _customerManager.AddCustomer(otpRequest.MobileNo!);

        OtpResponseModel otpResponseModel = await _otpRepository.SendOtpAsync(otpRequestModel);
        if(otpResponseModel != null && !string.IsNullOrEmpty(otpResponseModel.Data))
        {
            otpResponse.RequestId = otpResponseModel.Data;

            OtpLogRequest otpLogRequest = new OtpLogRequest();
            otpLogRequest.BPNumber = customerResponse.BPNumber;
            otpLogRequest.LoginTime = DateTime.Now;
            otpLogRequest.MobileNo = customerResponse.MobileNo;
            otpLogRequest.IpAddress = otpRequest.IpAddress;
            otpLogRequest.Latitude = otpRequest.Latitude;
            otpLogRequest.Longitude = otpRequest.Longitude;
            otpLogRequest.LoginDeviceType = otpRequest.LoginDeviceType;
            otpLogRequest.OtpRequestID = Convert.ToInt32(otpResponse.RequestId);
            otpLogRequest.LogType = otpRequest.Type;

            await OtpLog(otpLogRequest);
        }
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
            if(verifyOtpResponse.customerResponse != null)
            {
                OtpLogRequest otpLogRequest = new OtpLogRequest();
                otpLogRequest.BPNumber = verifyOtpResponse.customerResponse.BPNumber;
                otpLogRequest.LoginTime = DateTime.Now;
                otpLogRequest.MobileNo = verifyOtpResponse.customerResponse.MobileNo;
                otpLogRequest.IpAddress = verifyOtpRequest.IpAddress;
                otpLogRequest.Latitude = verifyOtpRequest.Latitude;
                otpLogRequest.Longitude = verifyOtpRequest.Longitude;
                otpLogRequest.LoginDeviceType = verifyOtpRequest.LoginDeviceType;
                otpLogRequest.OtpRequestID = Convert.ToInt32(verifyOtpRequest.RequestId);
                otpLogRequest.Otp = Convert.ToInt32(verifyOtpRequest.Otp);

                await OtpLog(otpLogRequest);
            }
        }
        return verifyOtpResponse;
    }

    public async Task<OtpLogResponse> OtpLog(OtpLogRequest otpLogRequest)
    {
        OtpLogResponse otpLogResponse = new OtpLogResponse();

        OtpLogRequestModel otpLogRequestModel = new OtpLogRequestModel();
        otpLogRequestModel.BPNumber = otpLogRequest.BPNumber;
        otpLogRequestModel.LoginTime = otpLogRequest.LoginTime;
        otpLogRequestModel.LogoutTime = otpLogRequest.LogoutTime;
        otpLogRequestModel.MobileNo = otpLogRequest.MobileNo;
        otpLogRequestModel.IpAddress = otpLogRequest.IpAddress;
        otpLogRequestModel.LoginDeviceType = otpLogRequest.LoginDeviceType;
        otpLogRequestModel.Latitude = otpLogRequest.Latitude;
        otpLogRequestModel.Longitude = otpLogRequest.Longitude;
        otpLogRequestModel.OtpRequestID = otpLogRequest.OtpRequestID;
        otpLogRequestModel.Otp = otpLogRequest.Otp;
        otpLogRequestModel.UserType = "Customer";
        otpLogRequestModel.CreatedBy = 101;
        otpLogRequestModel.CreatedDate = DateTime.Now;
        otpLogRequestModel.LogType = otpLogRequest.LogType;
        OtpLogResponseModel otpLogResponseModel = await _otpRepository.OtpLog(otpLogRequestModel);
        if(otpLogResponseModel != null && otpLogResponseModel.LogId != 0)
        {
            otpLogResponse.LogId = otpLogResponseModel.LogId;
        }

        return otpLogResponse;
    }

    public async Task<bool> GetOTPDetailByReqId(string reqId)
    {
        return await _otpRepository.GetOTPDetailByReqId(reqId);
    }
}
