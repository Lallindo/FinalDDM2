using FinalDDM2.Database;
using FinalDDM2.Models;
using Newtonsoft.Json.Linq;

namespace FinalDDM2.Services;

public class ClimaService(FinalDbContext dbContext, ILoggedUserService loggedUserService) : IClimaService
{
    private readonly FinalDbContext _dbContext = dbContext;
    private readonly ILoggedUserService _loggedUserService = loggedUserService;

    public async Task AddClima(Clima clima)
    {
        if (clima == null) return;
        await _dbContext.Climas.AddAsync(clima);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddClima(JObject? jObject)
    {
        if (jObject == null) return;

        var climaObj = await JsonToClima(jObject);
        await AddClima(climaObj);
    }

    public async Task<Clima?> JsonToClima(JObject? jObject)
    {
        if (jObject == null) return null;

        return new Clima
        {
            Usuario = await _loggedUserService.GetUsuarioLogado(),
            TempCelsius = (double)jObject["main"]["temp"],
            SensacaoTermCelsius = (double)jObject["main"]["feels_like"],
            Cidade = (string)jObject["name"],
            DataBusca = DateTime.Now,
            Longitude = (double)jObject["coord"]["lon"],
            Latitude = (double)jObject["coord"]["lat"],
            CondMetereologicas = (string)jObject["weather"][0]["description"]
        };
    }
}