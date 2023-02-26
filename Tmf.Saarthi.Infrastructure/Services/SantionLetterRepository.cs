using Microsoft.Extensions.Options;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.SanctionLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.SanctionLetter;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class SantionLetterRepository : ISantionLetterRepository
{
    private readonly IHttpService _httpService;
    private readonly LetterOptions _letterOptions;

    public SantionLetterRepository(IHttpService httpService, IOptions<LetterOptions> letterOptions)
    {
        _httpService = httpService;
        _letterOptions = letterOptions.Value;
    }
    public async Task<SanctionLetterResponseModel> GenerateSanctionLetter(SanctionLetterRequestModel sanctionLetterRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.PostAsync(_letterOptions.BaseUrl + _letterOptions.SanctionLetter, sanctionLetterRequestModel, headers);

        return JsonSerializer.Deserialize<SanctionLetterResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }
}
