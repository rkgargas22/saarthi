using Microsoft.Extensions.Options;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Otp;
using Tmf.Saarthi.Infrastructure.Models.Response.Otp;

namespace Tmf.Saarthi.Infrastructure.Services;

public class OtpRepository : IOtpRepository
{
    private readonly IHttpService _httpService;
    private readonly OtpServiceOptions _otpServiceOptions;
    public OtpRepository(IHttpService httpService, IOptions<OtpServiceOptions> otpServiceOptions)
    {
        _httpService = httpService;
        _otpServiceOptions = otpServiceOptions.Value;
    }

    public async Task<OtpResponseModel> SendOtpAsync(OtpRequestModel otpRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");
        headers.Add("OtpType", otpRequestModel.Type);

        JsonDocument result = await _httpService.PostAsync<OtpRequestModel>(_otpServiceOptions.BaseUrl + _otpServiceOptions.SendOtpEndpoint, otpRequestModel, headers);
       
        return JsonSerializer.Deserialize<OtpResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }

    public async Task<VerifyOtpResponseModel> VerifyOtpAsync(VerifyOtpRequestModel verifyOtpRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");
        headers.Add("OtpType", "verify");

        JsonDocument result = await _httpService.PostAsync<VerifyOtpRequestModel>(_otpServiceOptions.BaseUrl + _otpServiceOptions.VerifyOtpEndpoint, verifyOtpRequestModel, headers);

        return JsonSerializer.Deserialize<VerifyOtpResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }
}
