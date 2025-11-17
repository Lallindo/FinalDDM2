using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace FinalDDM2.Services;

public class ApiService : IApiService
{
    private string _chave = "6135072afe7f6cec1537d5cb08a5a1a2";

    private string _baseUrl = $"https://api.openweathermap.org/data/2.5/weather?units=metric&lang=pt_br";

    private string FullUrl => $"{_baseUrl}&appid={_chave}";

    public async Task<JObject> GetClimaIn(string cidade)
    {
        var url = $"{FullUrl}&q={cidade}";

        using var client = new HttpClient();
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            var temp = JObject.Parse(responseString);
            return await Task.FromResult(temp);
        }
        else
        {
            return await Task.FromResult<JObject>(null);
        }
    }
}