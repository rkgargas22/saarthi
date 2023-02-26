using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.CustomerConsent;
using Tmf.Saarthi.Infrastructure.Models.Response.CustomerConsent;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class CustomerConsentRepository : ICustomerConsentRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly IHttpService _httpService;
    private readonly ConnectionStringsOptions _connectionStringsOptions;
    private readonly LetterOptions _customerConsentOptions;

    public CustomerConsentRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, IHttpService httpService, IOptions<LetterOptions> customerConsentOptions)
    {
        _sqlUtility = sqlUtility;
        _httpService = httpService;
        _connectionStringsOptions = connectionStringsOptions.Value;
        _customerConsentOptions = customerConsentOptions.Value;
    }
    public async Task<CustomerConsentResponseModel> GenerateCustomerConsent(CustomerConsentRequestModel customerConsentRequest)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.PostAsync(_customerConsentOptions.BaseUrl + _customerConsentOptions.CustomerConsent , customerConsentRequest, headers);

        return JsonSerializer.Deserialize<CustomerConsentResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }
    public async Task<CustomerConsentDocumentByFleetResponseModel> GetCustomerConsentLetterByFleetId(long FleetId,string Documenttype)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", FleetId),
            new SqlParameter("Documenttype", Documenttype)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetDocumentByFleetID", parameters);

        CustomerConsentDocumentByFleetResponseModel customerConsentFleetResponse = new CustomerConsentDocumentByFleetResponseModel();
        if (dt.Rows.Count > 0)
        {
                customerConsentFleetResponse.FleetId = (int)dt.Rows[0]["FleetID"];
                customerConsentFleetResponse.DocumentUrl = dt.Rows[0]["DocumentUrl"] == DBNull.Value ? "" : (string)dt.Rows[0]["DocumentUrl"];
                customerConsentFleetResponse.CreatedBy = dt.Rows[0]["CreatedBy"] == DBNull.Value ? "" : (string)dt.Rows[0]["CreatedBy"];
                customerConsentFleetResponse.IsActive = (bool)dt.Rows[0]["IsActive"];
                customerConsentFleetResponse.Documenttype = dt.Rows[0]["LastName"] == DBNull.Value ? "" : (string)dt.Rows[0]["Documenttype"];
        }

        return customerConsentFleetResponse;
    }
}
