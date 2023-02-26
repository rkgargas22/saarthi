using Microsoft.Extensions.Options;
using System.Text.Json;
using Tmf.Saarthi.Core.Constants;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Ocr;
using Tmf.Saarthi.Infrastructure.Models.Response.Ocr;

namespace Tmf.Saarthi.Infrastructure.Services;

public class OcrRepository : IOcrRepository
{
    private readonly IHttpService _httpService;
    private readonly OcrOptions _ocrOptions;
    public OcrRepository(IHttpService httpService, IOptions<OcrOptions> ocrOptions)
    {
        _httpService = httpService;
        _ocrOptions = ocrOptions.Value;
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
}
