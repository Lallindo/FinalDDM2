using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Services;
using Debug = System.Diagnostics.Debug;

namespace FinalDDM2.ViewModels;

public partial class RegistroViewModel(IUsuarioService usuarioService) : ObservableValidator
{
    private IUsuarioService UsuarioService { get; } = usuarioService;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Mínimo de 3 letras")]
    private string _nome;

    public ObservableCollection<string>? ErrosNome { get; set; } = null;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [PasswordPropertyText]
    [MinLength(5, ErrorMessage = "Senha precisa de pelo menos 5 dígitos")]
    private string _senha;

    public ObservableCollection<string>? ErrosSenha { get; set; } = null;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "Digite seu email")]
    [EmailAddress(ErrorMessage = "Email é inválido")]
    private string _email;

    public ObservableCollection<string>? ErrosEmail { get; set; } = null;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(typeof(DateTime), "1926-01-01", "2100-01-01", 
        ErrorMessage = "Data inválida ou não informada")]
    private DateTime _dataNascimento;

    public ObservableCollection<string>? ErrosDataNascimento { get; set; } = null;

    private Usuario Usuario => new Usuario()
    {
        Id = 0,
        Nome = Nome,
        Email = Email,
        Senha = Senha,
        DataNascimento = DataNascimento
    };

    [RelayCommand]
    private async Task FazerCadastro()
    {
        await SetErrors();

        if (!HasErrors)
        {
            await UsuarioService.RegistrarUsuario(Usuario);
            await Shell.Current.GoToAsync("///Login");
        }
        else
        {
            foreach (var erro in GetErrors())
            {
                Debug.WriteLine(erro.ErrorMessage);
            }
        }
    }

    private Task SetErrors()
    {
        ValidateAllProperties();

        var tempErrosNome = GetErrors(nameof(Nome));
        if (tempErrosNome.Count() != 0) SetErrorOf(nameof(ErrosNome), tempErrosNome);
        
        var tempErrosEmail = GetErrors(nameof(Email));
        if (tempErrosEmail.Count() != 0) SetErrorOf(nameof(ErrosEmail), tempErrosEmail);
        
        var tempErrosSenha = GetErrors(nameof(Senha));
        if (tempErrosSenha.Count() != 0) SetErrorOf(nameof(ErrosSenha), tempErrosSenha);
        
        var tempErrosDataNascimento = GetErrors(nameof(Nome));
        if (tempErrosDataNascimento.Count() != 0) SetErrorOf(nameof(ErrosDataNascimento), tempErrosDataNascimento);
        
        return Task.CompletedTask;
    }

    private Task SetErrorOf(string nomePropriedade, IEnumerable<ValidationResult> errors)
    {
        ObservableCollection<string>? prop = (ObservableCollection<string>?)GetType().GetProperty(nomePropriedade)?.GetValue(this);
        if (prop == null) return Task.CompletedTask;
        prop.Clear();
        foreach (var erro in errors)
        {
            prop.Add(
                erro.ErrorMessage != null 
                    ? erro.ErrorMessage 
                    : "Erro não especificado");
        }
        return Task.CompletedTask;
    }
}