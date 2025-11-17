using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Services;
using FinalDDM2.Views;

namespace FinalDDM2;

public partial class AppShell : Shell
{
    private IUsuarioService _usuarioService { get; set; }
    
    public AppShell(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
        _usuarioService.Deslogar();
        
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(Login), typeof(Login));
        Routing.RegisterRoute(nameof(Registro), typeof(Registro));
        Routing.RegisterRoute(nameof(Listagem), typeof(Listagem));
        
        BindingContext = _usuarioService;
    }
}