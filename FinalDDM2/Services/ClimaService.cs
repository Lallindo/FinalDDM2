using FinalDDM2.Database;
using FinalDDM2.Models;
using Newtonsoft.Json.Linq;

namespace FinalDDM2.Services;

public class ClimaService(FinalDbContext dbContext, ILoggedUserService loggedUserService) : IClimaService
{
    private FinalDbContext _dbContext { get; } = dbContext;
    private ILoggedUserService _loggedUserService { get; } = loggedUserService; 
    
    public async Task AddClima(Clima clima)
    {
        await _dbContext.Climas.AddAsync(clima);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddClima(JObject clima)
    {
        var climaObj = await JsonToClima(clima);
        await AddClima(climaObj);
    }

    public Task<Clima> JsonToClima(JObject clima)
    {
        return Task.Run(async () =>
        {
            return new Clima()
            {
                Id = 0,
                Usuario = await _loggedUserService.GetUsuarioLogado(),
                TempCelsius = (double)clima["main"]["temp"],
                SensacaoTermCelsius = (double)clima["main"]["feels_like"],
                Cidade = (string)clima["name"],
                DataBusca = DateTime.Now,
                Longitude = (double)clima["coord"]["lon"],
                Latitude = (double)clima["coord"]["lat"],
                CondMetereologicas = (string)clima["weather"][0]["description"]
            };
        });
    }
}