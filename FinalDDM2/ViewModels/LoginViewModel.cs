using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Services;

namespace FinalDDM2.ViewModels;

public partial class LoginViewModel(IUsuarioService usuarioService) : ObservableObject
{
    private IUsuarioService UsuarioService { get; } = usuarioService;
    
    [ObservableProperty] private Usuario _usuario = new();
    
    [RelayCommand]
    private async Task TentarLogin()
    {
        await UsuarioService.TentarLogin(Usuario);
        if (await SecureStorage.GetAsync("IdUsuario") != null)
        {
            await Shell.Current.GoToAsync("///Listagem");
        }
    }

    [RelayCommand]
    private async Task IrParaRegistro()
    {
        await Shell.Current.GoToAsync("Registro");
    }
}