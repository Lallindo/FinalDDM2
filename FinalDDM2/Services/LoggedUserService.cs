using FinalDDM2.Models;

namespace FinalDDM2.Services;

public class LoggedUserService : ILoggedUserService
{
    private Usuario? _usuarioLogado { get; set; } = null;

    public Task<Usuario?> GetUsuarioLogado()
    {
        return Task.FromResult(_usuarioLogado);
    }
    
    public Task SetUsuarioLogado(Usuario usuario)
    {
        return Task.Run(() => _usuarioLogado = usuario);
    }

    public Task UnsetUsuarioLogado()
    {
        return Task.Run(() => _usuarioLogado = null);
    }
}