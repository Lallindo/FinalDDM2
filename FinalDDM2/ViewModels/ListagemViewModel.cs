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
    private IUsuarioService _usuarioService { get; } = usuarioService;
    private IClimaService _climaService { get; } = climaService;
    private IApiService _apiService { get; } = apiService;
    private ILoggedUserService _loggedUserService { get; } = loggedUserService;

    [ObservableProperty] private Usuario _usuarioLogado = new();
    public ObservableCollection<Clima> Buscas { get; set; } = [];
    [ObservableProperty] private string _cidadeBusca = string.Empty;

    [RelayCommand]
    private async Task CallApi()
    {
        var climaObj = await _climaService.JsonToClima(await _apiService.GetClimaIn(CidadeBusca));
        await _climaService.AddClima(climaObj);
        await CarregarBuscasUsuarioLogado();
    }

    [RelayCommand]
    private async Task Deslogar()
    {
        await _usuarioService.Deslogar();
    }
    
    public async Task CarregarDadosUsuario()
    {
        UsuarioLogado = await _loggedUserService.GetUsuarioLogado();
    }

    public async Task CarregarBuscasUsuarioLogado()
    {
        await RecarregarBuscas(await _usuarioService.ListarClimas(UsuarioLogado));
    }

    public async Task RecarregarBuscas(List<Clima> climas)
    {
        Buscas.Clear();
        await Task.Run(() =>
        {
            foreach (Clima clima in climas)
            {
                Buscas.Add(clima);
            }
        });
    }
}