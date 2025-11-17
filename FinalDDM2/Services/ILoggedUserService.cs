using FinalDDM2.Models;

namespace FinalDDM2.Services;

public interface ILoggedUserService
{
    Task<Usuario?> GetUsuarioLogado();
    Task SetUsuarioLogado(Usuario usuario);
    Task UnsetUsuarioLogado();
}