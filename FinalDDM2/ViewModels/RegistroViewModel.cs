using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Models.ValueObjects;
using FinalDDM2.Services;

namespace FinalDDM2.ViewModels;

public partial class RegistroViewModel(IUsuarioService usuarioService) : ObservableObject
{
    private IUsuarioService _usuarioService { get; } = usuarioService;

    [ObservableProperty] private Usuario _usuario = new();
    [ObservableProperty] private UsuarioDataError _usuarioErrors = new();
    [ObservableProperty] private bool _showErrorMessage = false;
 
    [RelayCommand]
    private async Task FazerCadastro()
    {
        await ChecarDadosInseridos();
        if (UsuarioErrors.AllIsValid)
        {
            ShowErrorMessage = false;
            await _usuarioService.RegistrarUsuario(Usuario);
        }
        else
        {
            ShowErrorMessage = true;
        }
    }

    private Task ChecarDadosInseridos()
    {
        if (Usuario.Nome != string.Empty) UsuarioErrors.NomeIsValid = true;
        if (Usuario.Senha != string.Empty) UsuarioErrors.SenhaIsValid = true;
        if (Usuario.Email != string.Empty && Usuario.Email.Contains("@email.com")) UsuarioErrors.EmailIsValid = true;
        if (Usuario.DataNascimento != new DateTime(1900, 1, 1)) UsuarioErrors.DataNascIsValid = true;
        return Task.CompletedTask;
    }
}