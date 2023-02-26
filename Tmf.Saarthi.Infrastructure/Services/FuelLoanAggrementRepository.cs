using Microsoft.Extensions.Options;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.FuelLoanAggrement;
using Tmf.Saarthi.Infrastructure.Models.Response.FuelLoanAggrement;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class FuelLoanAggrementRepository : IFuelLoanAggrementRepository
{
    private readonly IHttpService _httpService;
    private readonly LetterOptions _letterOptions;

    public FuelLoanAggrementRepository(IHttpService httpService, IOptions<LetterOptions> letterOptions)
    {
        _httpService = httpService;
        _letterOptions = letterOptions.Value;
    }
    public async Task<FuelLoanAggrementResponseModel> GenerateFuelLoanAgreement(FuelLoanAggrementRequestModel fuelLoanAggrementRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.PostAsync(_letterOptions.BaseUrl + _letterOptions.FuelLoanAgreement, fuelLoanAggrementRequestModel, headers);

        return JsonSerializer.Deserialize<FuelLoanAggrementResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }
}
