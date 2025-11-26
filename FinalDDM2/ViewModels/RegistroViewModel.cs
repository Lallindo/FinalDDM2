using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Services;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FinalDDM2.ViewModels;

public partial class RegistroViewModel(IUsuarioService usuarioService) : ObservableValidator
{
    private readonly IUsuarioService _usuarioService = usuarioService;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres")]
    private string _nome = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "O formato do email é inválido")]
    private string _email = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(5, ErrorMessage = "A senha precisa de pelo menos 5 dígitos")]
    private string _senha = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "A data de nascimento é obrigatória")]
    private DateTime _dataNascimento = DateTime.Now;

    public ObservableCollection<string> ErrosNome { get; } = new();
    public ObservableCollection<string> ErrosEmail { get; } = new();
    public ObservableCollection<string> ErrosSenha { get; } = new();
    public ObservableCollection<string> ErrosDataNascimento { get; } = new();

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
        if (!ValidateProperties()) return;

        await _usuarioService.RegistrarUsuario(Usuario);
        await Shell.Current.GoToAsync("///Login");
    }

    private bool ValidateProperties()
    {
        ValidateAllProperties();

        ErrosNome.Clear();
        ErrosEmail.Clear();
        ErrosSenha.Clear();
        ErrosDataNascimento.Clear();

        GetErrors(nameof(Nome)).Select(e => e.ErrorMessage).ToList<string?>().ForEach(ErrosNome.Add);
        GetErrors(nameof(Email)).Select(e => e.ErrorMessage).ToList<string?>().ForEach(ErrosEmail.Add);
        GetErrors(nameof(Senha)).Select(e => e.ErrorMessage).ToList<string?>().ForEach(ErrosSenha.Add);
        GetErrors(nameof(DataNascimento)).Select(e => e.ErrorMessage).ToList<string?>().ForEach(ErrosDataNascimento.Add);

        return !HasErrors;
    }
}