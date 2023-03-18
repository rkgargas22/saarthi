using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Otp;
using Tmf.Saarthi.Infrastructure.Models.Response.Otp;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class OtpRepository : IOtpRepository
{
    private readonly IHttpService _httpService;
    private readonly ISqlUtility _sqlUtility;
    private readonly OtpServiceOptions _otpServiceOptions;
    private readonly ConnectionStringsOptions _connectionStringsOptions;
    public OtpRepository(IHttpService httpService, IOptions<OtpServiceOptions> otpServiceOptions, ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _httpService = httpService;
        _otpServiceOptions = otpServiceOptions.Value;
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
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

    public async Task<OtpLogResponseModel> OtpLog(OtpLogRequestModel otpLogRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("BPNumber", otpLogRequestModel.BPNumber),
            new SqlParameter("LoginTime", otpLogRequestModel.LoginTime),
            new SqlParameter("LogoutTime", otpLogRequestModel.LogoutTime),
            new SqlParameter("MobileNo", otpLogRequestModel.MobileNo),
            new SqlParameter("IpAddress", otpLogRequestModel.IpAddress),
            new SqlParameter("LoginDeviceType", otpLogRequestModel.LoginDeviceType),
            new SqlParameter("Latitude", otpLogRequestModel.Latitude),
            new SqlParameter("Longitude", otpLogRequestModel.Longitude),
            new SqlParameter("OtpRequestID", otpLogRequestModel.OtpRequestID),
            new SqlParameter("Otp", otpLogRequestModel.Otp),
            new SqlParameter("CreatedBy", otpLogRequestModel.CreatedBy),
            new SqlParameter("CreatedDate", otpLogRequestModel.CreatedDate),
            new SqlParameter("UserType", otpLogRequestModel.UserType),
            new SqlParameter("LogType", otpLogRequestModel.LogType)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_addOtpLog", parameters);

        OtpLogResponseModel otpLogResponseModel = new OtpLogResponseModel();
        if (dt.Rows.Count > 0)
        {
            otpLogResponseModel.LogId = Convert.ToInt64(dt.Rows[0]["LogId"]);
        }

        return otpLogResponseModel;
    }

    public async Task<bool> GetOTPDetailByReqId(string reqId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("reqId", reqId)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "uspcheckOtpTimeOut", parameters);

        if (dt.Rows.Count > 0)
        {
            return true;
        }

        return false;
    }
}
