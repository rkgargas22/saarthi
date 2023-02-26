using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Email;
using Tmf.Saarthi.Infrastructure.Models.Response.Email;
using Tmf.Saarthi.Infrastructure.Models.Response.Fleet;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class EmailRepository : IEmailRepository
{
    private readonly IHttpService _httpService;
    private readonly ISqlUtility _sqlUtility;
    private readonly EmailOptions _emailOptions;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public EmailRepository(IOptions<EmailOptions> emailOptions, IHttpService httpService, ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _emailOptions = emailOptions.Value;
        _httpService = httpService;
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<SendEmailResponseModel> SendEmail(SendEmailRequestModel sendEmailRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");
        JsonDocument result = await _httpService.PostAsync(_emailOptions.BaseUrl + _emailOptions.SendEmail, sendEmailRequestModel, headers);

        return JsonSerializer.Deserialize<SendEmailResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }

    public async Task<TemplateDataResponse> GetTemplateData(TemplateDataRequest templateDataRequest)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("Module", templateDataRequest.Module),
            new SqlParameter("SubModule", templateDataRequest.SubModule),
            new SqlParameter("TemplateType", templateDataRequest.TemplateType)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetTemplateMasterByModule", parameters);

        TemplateDataResponse templateDataResponse = new TemplateDataResponse();
        if (dt.Rows.Count > 0)
        {
            templateDataResponse.TemplateId = (long)dt.Rows[0]["TemplateId"];
            templateDataResponse.Subject = dt.Rows[0]["Subject"] != DBNull.Value ? (string)dt.Rows[0]["Subject"] : string.Empty;
            templateDataResponse.Url = dt.Rows[0]["Url"] != DBNull.Value ? (string)dt.Rows[0]["Url"] : string.Empty;
            templateDataResponse.Body = (string)dt.Rows[0]["Body"];
            templateDataResponse.TemplateType = (string)dt.Rows[0]["TemplateType"];
            templateDataResponse.Module = (string)dt.Rows[0]["Module"];
            templateDataResponse.SubModule = dt.Rows[0]["SubModule"] != DBNull.Value ? (string)dt.Rows[0]["SubModule"] : string.Empty;
            //templateDataResponse.IsActive = (bool)dt.Rows[0]["IsActive"];
            //templateDataResponse.CreatedBy = (long)dt.Rows[0]["CreatedBy"];
            //templateDataResponse.CreatedDate = (DateTime)dt.Rows[0]["CreatedDate"];
            //templateDataResponse.UpdatedBy = dt.Rows[0]["UpdatedBy"] != DBNull.Value ? (long)dt.Rows[0]["UpdatedBy"] : null;
            //templateDataResponse.UpdatedDate = dt.Rows[0]["UpdatedDate"] != DBNull.Value ? (DateTime)dt.Rows[0]["UpdatedDate"] : null;
        }

        return templateDataResponse;
    }
}
