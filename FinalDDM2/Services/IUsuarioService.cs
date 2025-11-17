using FinalDDM2.Models;

namespace FinalDDM2.Services;

public interface IUsuarioService
{
    Task RegistrarUsuario(Usuario usuario);
    Task TentarLogin(string email, string senha);
    Task TentarLogin(Usuario usuario);
    Task Deslogar();
    Task<List<Clima>> ListarClimas(Usuario usuario);
    Task<List<Clima>> ListarClimas(int usuarioId);
    Task<List<Clima>> ListarClimasPorData(DateTime dataInicial, DateTime dataFinal);
}