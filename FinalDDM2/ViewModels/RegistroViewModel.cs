using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Models.ValueObjects;
using FinalDDM2.Services;

namespace FinalDDM2.ViewModels;

public partial class RegistroViewModel(IUsuarioService usuarioService) : ObservableValidator
{
    private IUsuarioService UsuarioService { get; } = usuarioService;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Mínimo de 3 letras")]
    private string _nome;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [PasswordPropertyText]
    [MinLength(5, ErrorMessage = "Senha precisa de pelo menos 5 dígitos")]
    private string _senha;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [EmailAddress]
    private string _email;
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Range(typeof(DateTime), "1900-01-01", "2100-01-01", 
        ErrorMessage = "Data inválida ou não informada")]
    private DateTime _dataNascimento;

    private Usuario _usuario => new Usuario()
    {
        Id = 0,
        Nome = Nome,
        Email = Email,
        Senha = Senha,
        DataNascimento = DataNascimento
    };

    [RelayCommand]
    private async Task CadastrarUsuario()
    {
        ValidateAllProperties();

        if (!HasErrors)
        {
            await UsuarioService.RegistrarUsuario(_usuario);   
        }
    }
}