using FinalDDM2.Models;

namespace FinalDDM2.Services;

public class LoggedUserService : ILoggedUserService
{
    private Usuario? UsuarioLogado { get; set; } = null;

    public Task<Usuario?> GetUsuarioLogado()
    {
        return Task.FromResult(UsuarioLogado);
    }
    
    public Task SetUsuarioLogado(Usuario usuario)
    {
        return Task.Run(() => UsuarioLogado = usuario);
    }

    public Task UnsetUsuarioLogado()
    {
        return Task.Run(() => UsuarioLogado = null);
    }
}