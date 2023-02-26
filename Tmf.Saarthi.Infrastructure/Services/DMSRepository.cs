using Microsoft.Extensions.Options;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.DMS;
using Tmf.Saarthi.Infrastructure.Models.Response.DMS;

namespace Tmf.Saarthi.Infrastructure.Services;

public class DMSRepository : IDMSRepository
{
    private readonly IHttpService _httpService;
    private readonly DMSOptions _dMSOptions;

    public DMSRepository(IOptions<DMSOptions> dMSOptions, IHttpService httpService)
    {
        _dMSOptions = dMSOptions.Value;
        _httpService = httpService;
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
}
