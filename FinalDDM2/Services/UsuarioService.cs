using System.Diagnostics;
using FinalDDM2.Database;
using FinalDDM2.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalDDM2.Services;

public class UsuarioService(FinalDbContext dbContext, ILoggedUserService loggedUserService) : IUsuarioService
{
    private FinalDbContext DbContext { get; } = dbContext;
    private ILoggedUserService LoggedUserService { get; } = loggedUserService;
    
    public async Task RegistrarUsuario(Usuario usuario)
    {
        DbContext.Usuarios.Add(usuario);
        await DbContext.SaveChangesAsync();
    }
    
    public async Task TentarLogin(string email, string senha)
    {
        Usuario? usuario = await DbContext.Usuarios
            .Where(u => u.Email == email && u.Senha == senha)
            .Include(u => u.Buscas)
            .FirstOrDefaultAsync();

        if (usuario is null) return;
        await LoggedUserService.SetUsuarioLogado(usuario);
    }
    
    public async Task TentarLogin(Usuario usuario)
    {
        await TentarLogin(usuario.Email, usuario.Senha);
    }

    public async Task Deslogar()
    {
        await LoggedUserService.UnsetUsuarioLogado();
        
        Debug.WriteLine(await SecureStorage.GetAsync("IdUsuario"));
    }
    
    public async Task<List<Clima>> ListarClimas(Usuario usuario)
    {
        return await ListarClimas(usuario.Id);
    }
    
    public async Task<List<Clima>> ListarClimas(int usuarioId)
    {
        return await DbContext.Climas
            .Where(c => c.UsuarioId == usuarioId)
            .ToListAsync();
    }
    
    public async Task<List<Clima>> ListarClimasPorData(DateTime dataInicial, DateTime dataFinal)
    {
        throw new NotImplementedException();
    }
}