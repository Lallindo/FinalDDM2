using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Services;

namespace FinalDDM2.ViewModels;

public partial class ListagemViewModel(IUsuarioService usuarioService, IClimaService climaService, IApiService apiService) : ObservableObject
{
    private IUsuarioService _usuarioService { get; } = usuarioService;
    private IClimaService _climaService { get; } = climaService;
    private IApiService _apiService { get; } = apiService;
    
    [ObservableProperty] private string _nomeUsuario = SecureStorage.GetAsync("NomeUsuario").Result;

    [RelayCommand]
    private async Task CallApi()
    {
        await _climaService.AddClima(await _apiService.GetClimaIn("Jau"));
    }

    [RelayCommand]
    private async Task Deslogar()
    {
        await _usuarioService.Deslogar();
    }
}