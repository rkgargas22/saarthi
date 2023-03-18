using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Constants;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.CPCFacility;
using Tmf.Saarthi.Infrastructure.Models.Request.Ocr;
using Tmf.Saarthi.Infrastructure.Models.Response.CPCFacility;
using Tmf.Saarthi.Infrastructure.Models.Response.Ocr;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class OcrRepository : IOcrRepository
{
    private readonly IHttpService _httpService;
    private readonly ISqlUtility _sqlUtility;
    private readonly OcrOptions _ocrOptions;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public OcrRepository(IHttpService httpService, IOptions<OcrOptions> ocrOptions, IOptions<ConnectionStringsOptions> connectionStringsOptions, ISqlUtility sqlUtility)
    {
        _httpService = httpService;
        _ocrOptions = ocrOptions.Value;
        _connectionStringsOptions = connectionStringsOptions.Value;
        _sqlUtility = sqlUtility;
    }

    public async Task<AadharExtractResponseModel> AadharExtract(AadharExtractRequestModel aadharExtractRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.PostAsync(_ocrOptions.BaseUrl + _ocrOptions.AadharExtract, aadharExtractRequestModel, headers);
        TaskDetailResponseModel taskDetailResponseModel = JsonSerializer.Deserialize<TaskDetailResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();

        if (taskDetailResponseModel != null && !string.IsNullOrEmpty(taskDetailResponseModel.RequestId))
        {
            TaskDetailRequestModel taskDetailRequestModel = new TaskDetailRequestModel();
            taskDetailRequestModel.RequestId = taskDetailResponseModel.RequestId;
            taskDetailRequestModel.RequestType = OcrRequestType.AADHAR;
            await Task.Delay(5000);
            var data = await TaskDetail<AadharExtractResponseModel>(taskDetailRequestModel);
            return data ?? new AadharExtractResponseModel();

        }
        return new AadharExtractResponseModel();
    }

    public async Task<DLExtractResponseModel> DLExtract(DLExtractRequestModel dlExtractRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.PostAsync(_ocrOptions.BaseUrl + _ocrOptions.DLExtract, dlExtractRequestModel, headers);
        TaskDetailResponseModel taskDetailResponseModel = JsonSerializer.Deserialize<TaskDetailResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();

        if (taskDetailResponseModel != null && !string.IsNullOrEmpty(taskDetailResponseModel.RequestId))
        {
            TaskDetailRequestModel taskDetailRequestModel = new TaskDetailRequestModel();
            taskDetailRequestModel.RequestId = taskDetailResponseModel.RequestId;
            taskDetailRequestModel.RequestType = OcrRequestType.DL;
            await Task.Delay(5000);
            var data = await TaskDetail<DLExtractResponseModel>(taskDetailRequestModel);
            return data ?? new DLExtractResponseModel();
        }
        return new DLExtractResponseModel();
    }

    public async Task<DocumentMaskingResponseModel> DocumentMasking(DocumentMaskingRequestModel documentMaskingRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.PostAsync(_ocrOptions.BaseUrl + _ocrOptions.DataMasking, documentMaskingRequestModel, headers);
        TaskDetailResponseModel taskDetailResponseModel = JsonSerializer.Deserialize<TaskDetailResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();

        if (taskDetailResponseModel != null && !string.IsNullOrEmpty(taskDetailResponseModel.RequestId))
        {
            TaskDetailRequestModel taskDetailRequestModel = new TaskDetailRequestModel();
            taskDetailRequestModel.RequestId = taskDetailResponseModel.RequestId;
            taskDetailRequestModel.RequestType = OcrRequestType.MASKING;
            await Task.Delay(5000);
            var data = await TaskDetail<DocumentMaskingResponseModel>(taskDetailRequestModel);
            return data ?? new DocumentMaskingResponseModel();
        }
        return new DocumentMaskingResponseModel();
    }

    public async Task<ValidateDocumentResponseModel> ValidateDocument(ValidateDocumentRequestModel validateDocumentRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.PostAsync(_ocrOptions.BaseUrl + _ocrOptions.ValidateDocument, validateDocumentRequestModel, headers);
        TaskDetailResponseModel taskDetailResponseModel = JsonSerializer.Deserialize<TaskDetailResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();

        if (taskDetailResponseModel != null && !string.IsNullOrEmpty(taskDetailResponseModel.RequestId))
        {
            TaskDetailRequestModel taskDetailRequestModel = new TaskDetailRequestModel();
            taskDetailRequestModel.RequestId = taskDetailResponseModel.RequestId;
            taskDetailRequestModel.RequestType = OcrRequestType.VALIDATION;

            await Task.Delay(5000);
            var data = await TaskDetail<ValidateDocumentResponseModel>(taskDetailRequestModel);
            return data ?? new ValidateDocumentResponseModel();
        }
        return new ValidateDocumentResponseModel();
    }

    public async Task<VoterIdExtractResponseModel> VoterIdExtract(VoterIdExtractRequestModel voterIdExtractRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.PostAsync(_ocrOptions.BaseUrl + _ocrOptions.VoterIdExtract, voterIdExtractRequestModel, headers);
        TaskDetailResponseModel taskDetailResponseModel = JsonSerializer.Deserialize<TaskDetailResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();

        if (taskDetailResponseModel != null && !string.IsNullOrEmpty(taskDetailResponseModel.RequestId))
        {
            TaskDetailRequestModel taskDetailRequestModel = new TaskDetailRequestModel();
            taskDetailRequestModel.RequestId = taskDetailResponseModel.RequestId;
            taskDetailRequestModel.RequestType = OcrRequestType.VOTERID;
            await Task.Delay(5000);
            var data = await TaskDetail<VoterIdExtractResponseModel>(taskDetailRequestModel);
            return data ?? new VoterIdExtractResponseModel();
        }
        return new VoterIdExtractResponseModel();
    }

    private async Task<dynamic> TaskDetail<TOut>(TaskDetailRequestModel taskDetailRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        string Url = _ocrOptions.BaseUrl + _ocrOptions.TaskDetails;
        Url = Url.Replace("{ReqId}", taskDetailRequestModel.RequestId);
        Url = Url.Replace("{ReqType}", taskDetailRequestModel.RequestType);

        JsonDocument result = await _httpService.GetAsync(Url, headers);

        var data = JsonSerializer.Deserialize<List<TOut>>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
        if (data.Count > 0)
        {
            return data[0];
        }
        else
        {
            return null;
        }
    }

    public async Task<OCRUploadDocumentResponseModel> UploadOcrDocuments(OCRUploadDocumentRequestModel oCRUploadDocumentRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
    {
        new SqlParameter("FleetID", oCRUploadDocumentRequestModel.FleetID),
        new SqlParameter("StagId", oCRUploadDocumentRequestModel.StagId),
        new SqlParameter("DocumentType", oCRUploadDocumentRequestModel.DocumentType),
        new SqlParameter("DocumentExtension", oCRUploadDocumentRequestModel.DocumentExtension),
        new SqlParameter("DocumentURL", oCRUploadDocumentRequestModel.DocumentURL_1),
    };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_InsertOcrVerifiedDocuments", parameters);

        if (oCRUploadDocumentRequestModel.DocumentURL_2 != null)
        {
            List<SqlParameter> parameters1 = new List<SqlParameter>()
    {
        new SqlParameter("FleetID", oCRUploadDocumentRequestModel.FleetID),
        new SqlParameter("StagId", oCRUploadDocumentRequestModel.StagId),
        new SqlParameter("DocumentType", oCRUploadDocumentRequestModel.DocumentType),
        new SqlParameter("DocumentExtension", oCRUploadDocumentRequestModel.DocumentExtension),
        new SqlParameter("DocumentURL", oCRUploadDocumentRequestModel.DocumentURL_2),
    };

            DataTable dt1 = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_InsertOcrVerifiedDocuments", parameters1);

        }
        OCRUploadDocumentResponseModel oCRUploadDocumentResponseModel = new OCRUploadDocumentResponseModel();
        if (dt.Rows.Count > 0)
        {
            oCRUploadDocumentResponseModel.FleetID = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return oCRUploadDocumentResponseModel;
    }


    public async Task<OCRUploadAddressResponseModel> UploadOcrAddress(OCRUploadAddressRequestModel oCRUploadAddressRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
    {
        new SqlParameter("BPNumber", oCRUploadAddressRequestModel.BPNo),
        new SqlParameter("Type", "Default"),
        new SqlParameter("AddressLine1", oCRUploadAddressRequestModel.Address),
        new SqlParameter("AddressLine2", oCRUploadAddressRequestModel.StreetAddress),
        new SqlParameter("Landmark", ""),
        new SqlParameter("City", ""),
        new SqlParameter("District", oCRUploadAddressRequestModel.District),
        new SqlParameter("Region", oCRUploadAddressRequestModel.State),
        new SqlParameter("Country","India"),
        new SqlParameter("Pincode",Convert.ToInt32(oCRUploadAddressRequestModel.Pincode)),
        new SqlParameter("IsDefault",true),

    };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_InsertCustomerAddress", parameters);

        OCRUploadAddressResponseModel oCRUploadAddressResponseModel = new OCRUploadAddressResponseModel();
        if (dt.Rows.Count > 0)
        {
            oCRUploadAddressResponseModel.AddressID = Convert.ToInt64(dt.Rows[0]["AddressID"]);
        }

        return oCRUploadAddressResponseModel;
    }
}
