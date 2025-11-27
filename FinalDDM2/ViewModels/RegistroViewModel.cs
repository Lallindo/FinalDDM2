using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Services;

namespace FinalDDM2.ViewModels;

public partial class RegistroViewModel(IUsuarioService usuarioService) : ObservableObject
{
    private readonly IUsuarioService _usuarioService = usuarioService;

    [ObservableProperty] private string _nome = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _senha = string.Empty;
    [ObservableProperty] private DateTime _dataNascimento = DateTime.Now;

    [ObservableProperty] private string _erroNome = string.Empty;
    [ObservableProperty] private string _erroEmail = string.Empty;
    [ObservableProperty] private string _erroSenha = string.Empty;

    private Usuario Usuario => new()
    {
        Nome = Nome,
        Email = Email,
        Senha = Senha,
        DataNascimento = DataNascimento
    };
    

    [RelayCommand]
    private async Task FazerCadastro()
    {
        if (!Validate()) return;
        
        await _usuarioService.RegistrarUsuario(Usuario);
        await Shell.Current.GoToAsync("///Login");
    }

    private bool Validate()
    {
        // Clear previous errors
        ErroNome = string.Empty;
        ErroEmail = string.Empty;
        ErroSenha = string.Empty;

        // Validate Nome
        if (string.IsNullOrWhiteSpace(Nome))
        {
            ErroNome = "O nome é obrigatório.";
        }

        // Validate Email (prioritizing empty check)
        if (string.IsNullOrWhiteSpace(Email))
        {
            ErroEmail = "O email é obrigatório.";
        }
        else if (!Email.Contains('@'))
        {
            ErroEmail = "O formato do email é inválido (precisa de um '@').";
        }

        // Validate Senha
        if (string.IsNullOrWhiteSpace(Senha) || Senha.Length <= 5)
        {
            ErroSenha = "A senha precisa ter mais de 5 caracteres.";
        }

        // Return true if no error messages were set
        return string.IsNullOrEmpty(ErroNome) &&
               string.IsNullOrEmpty(ErroEmail) &&
               string.IsNullOrEmpty(ErroSenha);
    }
}