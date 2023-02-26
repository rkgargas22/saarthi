using System.Text.Json;

namespace Tmf.Saarthi.Infrastructure.HttpService;

public interface IHttpService
{
    Task<JsonDocument> GetAsync(string uri, Dictionary<string, string> Headers);

    Task<JsonDocument> PostAsync<TIn>(string uri, TIn model, Dictionary<string, string> Headers);
}
