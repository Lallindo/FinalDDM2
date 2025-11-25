using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Services;
using FinalDDM2.Views.Components;
using Microsoft.Maui.Controls.Shapes;
using IPopupService = FinalDDM2.Services.IPopupService;


namespace FinalDDM2.ViewModels;

public partial class ListagemViewModel(
    IUsuarioService usuarioService, 
    IClimaService climaService, 
    IApiService apiService, 
    ILoggedUserService loggedUserService,
    IOpcoesService opcoesService,
    IPopupService popupService,
    IDialogService dialogService) : ObservableObject
{
    private IUsuarioService UsuarioService { get; } = usuarioService;
    private IClimaService ClimaService { get; } = climaService;
    private IApiService ApiService { get; } = apiService;
    private ILoggedUserService LoggedUserService { get; } = loggedUserService;
    private IOpcoesService OpcoesService { get; } = opcoesService;
    private IPopupService PopupService { get; } = popupService;
    private IDialogService DialogService { get; } = dialogService;

    [ObservableProperty] private int _idOpcoesTemp = 0;
    [ObservableProperty] private Usuario? _usuarioLogado = new();
    [ObservableProperty] private string _cidadeBusca = string.Empty;
    
    [RelayCommand]
    private async Task CallApi()
    {
        var climaObj = await ClimaService.JsonToClima(await ApiService.GetClimaIn(CidadeBusca));
        await ClimaService.AddClima(climaObj);
    }

    [RelayCommand]
    private async Task AbrirConfiguracoes()
    {
        await PopupService.AbrirConfiguracoesModal(IdOpcoesTemp);
    }

    [RelayCommand]
    private async Task Deslogar()
    {
        await UsuarioService.Deslogar();
    }

    public Task GetIdOpcao()
    {
        IdOpcoesTemp = OpcoesService.GetOpcoesTemp();
        return Task.CompletedTask;
    }
    
    public async Task CarregarDadosUsuario()
    {
        UsuarioLogado = await LoggedUserService.GetUsuarioLogado();
    }

    public async Task ChecarUsuarioLogado()
    {
        if (UsuarioLogado == null)
        {
            var task = DialogService.DisplayAlert("Erro", "Bla", "blala");
            if (task.IsCompleted)
            {
                await Shell.Current.GoToAsync("///Login");   
            }
        }
    }
}