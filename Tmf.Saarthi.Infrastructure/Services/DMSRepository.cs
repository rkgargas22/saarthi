using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.RequestModels.DMS;
using Tmf.Saarthi.Core.RequestModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request;
using Tmf.Saarthi.Infrastructure.Models.Request.DMS;
using Tmf.Saarthi.Infrastructure.Models.Request.Document;
using Tmf.Saarthi.Infrastructure.Models.Request.Fleet;
using Tmf.Saarthi.Infrastructure.Models.Request.Ocr;
using Tmf.Saarthi.Infrastructure.Models.Response.DMS;
using Tmf.Saarthi.Infrastructure.Models.Response.Document;
using Tmf.Saarthi.Infrastructure.Models.Response.Fleet;
using Tmf.Saarthi.Infrastructure.Models.Response.FleetVehicle;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class DMSRepository : IDMSRepository
{
    private readonly IFleetRepository _fleetRepository;
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;
    private readonly IHttpService _httpService;
    private readonly DMSOptions _dMSOptions;

    public DMSRepository(IOptions<DMSOptions> dMSOptions, IHttpService httpService, ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, IFleetRepository fleetRepository)
    {
        _dMSOptions = dMSOptions.Value;
        _httpService = httpService;
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
        _fleetRepository = fleetRepository;
    }

    public async Task<GenerateFanNoResponseModel> GenerateFanNo(GenerateFanNoRequestModel generateFanNoRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");
        JsonDocument result = await _httpService.PostAsync<GenerateFanNoRequestModel>(_dMSOptions.BaseUrl + _dMSOptions.GenerateFanNo, generateFanNoRequestModel, headers);

        return JsonSerializer.Deserialize<GenerateFanNoResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }

    public async Task<GenerateTokenResponseModel> GenerateToken(GenerateTokenRequestModel generateTokenRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.GetAsync(_dMSOptions.BaseUrl + _dMSOptions.GenerateToken + generateTokenRequestModel.DomainId, headers);

        return JsonSerializer.Deserialize<GenerateTokenResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();

    }

    public async Task<UploadDocumentsDMSResponseModel> UploadDocument(UploadDocumentsDMSRequestModel uploadDocumentsDMSRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");
        JsonDocument result = await _httpService.PostAsync<UploadDocumentsDMSRequestModel>(_dMSOptions.BaseUrl + _dMSOptions.UploadDocument, uploadDocumentsDMSRequestModel, headers);

        return JsonSerializer.Deserialize<UploadDocumentsDMSResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }
    public async Task<UpdateFleetFanNoResponse> UpdateFleetFanNo(UpdateFleetFanNoRequest updateFleetFanNoRequest)
    {
        UpdateFleetFanNoRequestModel updateFleetFanNoRequestModel = new UpdateFleetFanNoRequestModel();
        updateFleetFanNoRequestModel.FleetID = updateFleetFanNoRequest.FleetID;
        updateFleetFanNoRequestModel.FanNo = updateFleetFanNoRequest.FanNo;
        updateFleetFanNoRequestModel.UpdatedBy = 41;
        updateFleetFanNoRequestModel.UpdatedDate = DateTime.Now;

        UpdateFleetFanNoResponseModel updateFleetFanNoResponseModel = await _fleetRepository.UpdateFleetFanNo(updateFleetFanNoRequestModel);

        UpdateFleetFanNoResponse updateFleetFanNoResponse = new UpdateFleetFanNoResponse();
        updateFleetFanNoResponse.FleetID = updateFleetFanNoResponseModel.FleetID;

        return updateFleetFanNoResponse;
    }
    public async Task<DMSDocumentAndFleetResponseModel> GetFleetAndDocDetailsByFleetId(DMSDocumentAndFleetRequestModel dMSDocumentAndFleetRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", dMSDocumentAndFleetRequestModel.FleetId),
            new SqlParameter("IsRCUUpload", dMSDocumentAndFleetRequestModel.IsRCUUpload)
        };

        DataSet ds = await _sqlUtility.ExecuteMultipleCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFleetAndDocDetails", parameters);

        DMSDocumentAndFleetResponseModel dMSDocumentAndFleetResponseModel = new DMSDocumentAndFleetResponseModel();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count >0)
            {
                dMSDocumentAndFleetResponseModel.DmsFleetDetailResponseModel = ConvertDataTableToObject<DmsFleetDetailResponseModel>(ds.Tables[0]);
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                dMSDocumentAndFleetResponseModel.DmsDocumentDetailsResponseModels = ConvertDataTableToList<DmsDocumentDetailsResponseModel>(ds.Tables[1]);
            }
        }

        return dMSDocumentAndFleetResponseModel;
    }
    private static T ConvertDataTableToObject<T>(DataTable dt)
    {
        var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
        var properties = typeof(T).GetProperties();

        DataRow row = dt.Rows[0];
        var objT = Activator.CreateInstance<T>();
        foreach (var pro in properties)
        {
            if (columnNames.Contains(pro.Name.ToLower()))
            {
                try
                {
                    if (row[pro.Name] == DBNull.Value)
                    {
                        if (pro.PropertyType == typeof(string))
                        {
                            pro.SetValue(objT, string.Empty);
                        }
                        else
                        {
                            pro.SetValue(objT, default);
                        }
                    }
                    else
                    {
                        pro.SetValue(objT, row[pro.Name]);
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
        return objT;
    }
    public static List<T> ConvertDataTableToList<T>(DataTable dt)
    {
        var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
        var properties = typeof(T).GetProperties();
        return dt.AsEnumerable().Select(row =>
        {
            var objT = Activator.CreateInstance<T>();
            foreach (var pro in properties)
            {
                if (columnNames.Contains(pro.Name.ToLower()))
                {
                    try
                    {
                        if (row[pro.Name] == DBNull.Value)
                        {
                            if (pro.PropertyType == typeof(string))
                            {
                                pro.SetValue(objT, string.Empty);
                            }
                            else
                            {
                                pro.SetValue(objT, default);
                            }
                        }
                        else
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return objT;
        }).ToList();
    }
    public async Task<UpdateDmsDocumentStatusResponseModel> UpdateDmsDocumentStatus(UpdateDmsDocumentStatusRequestModel updateDmsDocumentStatusRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("DocumentID", updateDmsDocumentStatusRequestModel.DocumentID),
            new SqlParameter("DMSUploadStatus", updateDmsDocumentStatusRequestModel.DMSUploadStatus),
            new SqlParameter("DMSComment", updateDmsDocumentStatusRequestModel.DMSComment),
         
            new SqlParameter("DMSDateTime", updateDmsDocumentStatusRequestModel.DMSDateTime)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateDocumentDmsData", parameters);

        UpdateDmsDocumentStatusResponseModel updateDmsDocumentStatusResponseModel = new UpdateDmsDocumentStatusResponseModel();
        if (dt.Rows.Count > 0)
        {
            updateDmsDocumentStatusResponseModel.DocumentID = Convert.ToInt64(dt.Rows[0]["DocumentID"]);
        }

        return updateDmsDocumentStatusResponseModel;
    }
    public async Task<UpdateRcuResponseModel> UpdateDocumentQueue(UpdateRcuRequestModel updateRcuRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", updateRcuRequestModel.FleetId),
            new SqlParameter("IsProcessed", updateRcuRequestModel.IsProcessed),
            new SqlParameter("Comment", updateRcuRequestModel.Comment),
            new SqlParameter("ProcessedDate", updateRcuRequestModel.ProcessedDate),
            new SqlParameter("IsRCUUpload", updateRcuRequestModel.IsRCUUpload),
            new SqlParameter("UpdatedDate", updateRcuRequestModel.UpdatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateRcu", parameters);

        UpdateRcuResponseModel updateRcuResponseModel = new UpdateRcuResponseModel();
        if (dt.Rows.Count > 0)
        {
            updateRcuResponseModel.FleetId = Convert.ToInt64(dt.Rows[0]["FleetId"]);
        }

        return updateRcuResponseModel;
    }

    public async Task<ViewDocumentResponseModel> ViewDocument(ViewDocumentRequestModel viewDocumentRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");
        string Url = _dMSOptions.BaseUrl + _dMSOptions.ViewDocument;
        Url = Url.Replace("{ReqType}", viewDocumentRequestModel.Type);
        Url = Url.Replace("{ReqDomainId}", viewDocumentRequestModel.DomainId);
        JsonDocument result = await _httpService.PostAsyncToView(Url, headers);

        return JsonSerializer.Deserialize<ViewDocumentResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }

    public async Task<string> GetFanNo(GetFanNoRequestModel getFanNoRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", getFanNoRequestModel.FleetId),           
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFanNoByFleetId", parameters);
        string FanNo = string.Empty;
        if (dt.Rows.Count > 0)
        {
            FanNo = Convert.ToString(dt.Rows[0]["FanNo"]);
        }       
        return FanNo;
    }
}

