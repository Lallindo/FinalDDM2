using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Services;
using Debug = System.Diagnostics.Debug;

namespace FinalDDM2.ViewModels;

public partial class ListagemViewModel(
    IUsuarioService usuarioService, 
    IClimaService climaService, 
    IApiService apiService, 
    ILoggedUserService loggedUserService) : ObservableObject
{
    private IUsuarioService UsuarioService { get; } = usuarioService;
    private IClimaService ClimaService { get; } = climaService;
    private IApiService ApiService { get; } = apiService;
    private ILoggedUserService LoggedUserService { get; } = loggedUserService;

    [ObservableProperty] private Usuario _usuarioLogado = new();
    [ObservableProperty] private string _cidadeBusca = string.Empty;

    [RelayCommand]
    private async Task CallApi()
    {
        var climaObj = await ClimaService.JsonToClima(await ApiService.GetClimaIn(CidadeBusca));
        await ClimaService.AddClima(climaObj);
        UsuarioLogado.Buscas.Add(climaObj);
    }

    [RelayCommand]
    private async Task Deslogar()
    {
        await UsuarioService.Deslogar();
    }
    
    public async Task CarregarDadosUsuario()
    {
        UsuarioLogado = await LoggedUserService.GetUsuarioLogado();
    }
}