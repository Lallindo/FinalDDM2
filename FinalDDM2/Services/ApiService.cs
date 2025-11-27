using Newtonsoft.Json.Linq;

namespace FinalDDM2.Services;

public class ApiService(IDialogService dialogService) : IApiService
{
    private readonly IDialogService _dialogService = dialogService;
    private const string Chave = "6135072afe7f6cec1537d5cb08a5a1a2";
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather?units=metric&lang=pt_br";

    private string FullUrl => $"{BaseUrl}&appid={Chave}";

    public async Task<JObject?> GetClimaIn(string cidade)
    {
        var url = $"{FullUrl}&q={cidade}";

        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseString);
            }
            else
            {
                // Inform the user about the error
                await _dialogService.DisplayAlert("Erro de API",
                    $"Não foi possível encontrar o clima para \"{cidade}\". Verifique o nome da cidade e tente novamente.",
                    "OK");
                return null;
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle network errors
            await _dialogService.DisplayAlert("Erro de Rede", $"Ocorreu um erro de conexão: {ex.Message}", "OK");
            return null;
        }
    }
}