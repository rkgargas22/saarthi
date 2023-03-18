using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Admin;
using Tmf.Saarthi.Infrastructure.Models.Request.ProvisionalLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.Admin;
using Tmf.Saarthi.Infrastructure.Models.Response.ProvisionalLetter;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class ProvisionalLetterRepository : IProvisionalLetterRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;
    private readonly IHttpService _httpService;
    private readonly LetterOptions _letterOptions;

    public ProvisionalLetterRepository(IHttpService httpService, IOptions<LetterOptions> letterOptions, ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _httpService = httpService;
        _letterOptions = letterOptions.Value;
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }
    public async Task<ProvisionalLetteResponseModel> GenerateprovisionalLetter(ProvisionalLetterRequestModel provisionalLetterRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "BpNo", "1" },
            { "UserType", "User" }
        };

        JsonDocument result = await _httpService.PostAsync(_letterOptions.BaseUrl + _letterOptions.ProvisionalLetter, provisionalLetterRequestModel, headers);

        return JsonSerializer.Deserialize<ProvisionalLetteResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }

    public async Task<DisagreeProvisionalResponseModel> DisagreeProvisionalLetter(DisagreeProvisionalResquestModel disagreeProvisionalResquestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", disagreeProvisionalResquestModel.FleetId)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_DisagreeProvisionalLetter", parameters);

        DisagreeProvisionalResponseModel disagreeProvisionalResponseModel = new DisagreeProvisionalResponseModel();
        if (dt.Rows.Count > 0)
        {
            disagreeProvisionalResponseModel.FleetId = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return disagreeProvisionalResponseModel;
    }
}
