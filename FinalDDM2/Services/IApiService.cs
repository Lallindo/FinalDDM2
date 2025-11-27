using Newtonsoft.Json.Linq;

namespace FinalDDM2.Services;

public interface IApiService
{
    Task<JObject?> GetClimaIn(string cidade);
}