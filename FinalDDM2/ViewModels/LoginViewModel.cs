using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Services;

namespace FinalDDM2.ViewModels;

public partial class LoginViewModel(IUsuarioService usuarioService, IDialogService dialogService) : ObservableObject
{
    private readonly IUsuarioService _usuarioService = usuarioService;
    private readonly IDialogService _dialogService = dialogService;
    
    [ObservableProperty] private Usuario _usuario = new();
    
    [RelayCommand]
    private async Task TentarLogin()
    {
        await _usuarioService.TentarLogin(Usuario);
        if (await SecureStorage.GetAsync("IdUsuario") != null)
        {
            await Shell.Current.GoToAsync("///Listagem");
        }
        else
        {
            await _dialogService.DisplayAlert("Erro", "Email ou senha incorretos.", "OK");
        }
    }

    [RelayCommand]
    private async Task IrParaRegistro()
    {
        await Shell.Current.GoToAsync("Registro");
    }
}