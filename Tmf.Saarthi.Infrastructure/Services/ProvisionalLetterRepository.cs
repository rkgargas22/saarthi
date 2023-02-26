using Microsoft.Extensions.Options;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.ProvisionalLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.ProvisionalLetter;

namespace Tmf.Saarthi.Infrastructure.Services;

public class ProvisionalLetterRepository: IProvisionalLetterRepository
{
    private readonly IHttpService _httpService;
    private readonly LetterOptions _letterOptions;

    public ProvisionalLetterRepository(IHttpService httpService, IOptions<LetterOptions> letterOptions)
    {
        _httpService = httpService;
        _letterOptions = letterOptions.Value;
    }
    public async Task<ProvisionalLetteResponseModel> GenerateprovisionalLetter(ProvisionalLetterRequestModel provisionalLetterRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.PostAsync(_letterOptions.BaseUrl + _letterOptions.ProvisionalLetter, provisionalLetterRequestModel, headers);

        return JsonSerializer.Deserialize<ProvisionalLetteResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }
}
