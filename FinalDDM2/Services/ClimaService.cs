using FinalDDM2.Database;
using FinalDDM2.Models;
using Newtonsoft.Json.Linq;

namespace FinalDDM2.Services;

public class ClimaService(FinalDbContext dbContext) : IClimaService
{
    private FinalDbContext _dbContext { get; } = dbContext;
    
    public async Task AddClima(Clima clima)
    {
        _dbContext.Climas.Add(clima);
    }

    public async Task AddClima(JObject clima)
    {
        Clima climaObj = new Clima()
        {
            Id = 0,
            UsuarioId = Convert.ToInt32(await SecureStorage.GetAsync("IdUsuario")),// TODO Mudar para usuário logado
            TempCelsius = (string)clima["main"]["temp"],
            SensacaoTermCelsius = (string)clima["main"]["feels_like"],
            Cidade = (string)clima["name"],
            DataBusca = DateTime.Now,
            Longitude = (double)clima["coord"]["lon"],
            Latitude = (double)clima["coord"]["lat"],
            CondMetereologicas = (string)clima["weather"][0]["description"]
        };
        await _dbContext.Climas.AddAsync(climaObj);
        await _dbContext.SaveChangesAsync();
    }
}