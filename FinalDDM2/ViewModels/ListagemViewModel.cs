using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Services;

namespace FinalDDM2.ViewModels;

public partial class ListagemViewModel(IClimaService climaService, IApiService apiService) : ObservableObject
{
    private IClimaService _climaService { get; } = climaService;
    private IApiService _apiService { get; } = apiService;

    [RelayCommand]
    private async Task CallApi()
    {
        await _climaService.AddClima(await _apiService.GetClimaIn("Jau"));
    }

}