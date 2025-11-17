using FinalDDM2.Models;
using Newtonsoft.Json.Linq;

namespace FinalDDM2.Services;

public interface IClimaService
{
    Task AddClima(Clima clima);
    Task AddClima(JObject clima);
}