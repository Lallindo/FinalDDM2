using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalDDM2.Models;
using FinalDDM2.Services;

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
    private readonly IUsuarioService _usuarioService = usuarioService;
    private readonly IClimaService _climaService = climaService;
    private readonly IApiService _apiService = apiService;
    private readonly ILoggedUserService _loggedUserService = loggedUserService;
    private readonly IOpcoesService _opcoesService = opcoesService;
    private readonly IPopupService _popupService = popupService;
    private readonly IDialogService _dialogService = dialogService;

    private List<Clima> _todasAsBuscas = new();

    [ObservableProperty] private int _idOpcoesTemp;
    [ObservableProperty] private Usuario? _usuarioLogado;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(CallApiCommand))]
    private string _cidadeBusca = string.Empty;

    [ObservableProperty] private ObservableCollection<Clima> _buscas = new();

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(FiltrarCommand))]
    private DateTime? _startDate;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(FiltrarCommand))]
    private DateTime? _endDate;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(ToggleFiltrosButtonText))]
    private bool _filtrosVisiveis = false;

    public string ToggleFiltrosButtonText => FiltrosVisiveis ? "Esconder Filtros" : "Mostrar Filtros";

    private bool CanCallApi() => !string.IsNullOrWhiteSpace(CidadeBusca);

    [RelayCommand(CanExecute = nameof(CanCallApi))]
    private async Task CallApi()
    {
        var climaObj = await _climaService.JsonToClima(await _apiService.GetClimaIn(CidadeBusca));
        await _climaService.AddClima(climaObj);
        await CarregarDadosUsuario(); // Refresh data
    }

    [RelayCommand]
    private async Task AbrirConfiguracoes()
    {
        var resultado = await _popupService.AbrirConfiguracoesModal(IdOpcoesTemp);
        if (resultado is int novoId)
        {
            _opcoesService.SetOpcoesTemp(novoId);
            IdOpcoesTemp = novoId;
        }
    }

    [RelayCommand]
    private async Task Deslogar()
    {
        await _usuarioService.Deslogar();
        await Shell.Current.GoToAsync("///Login");
    }

    [RelayCommand]
    private void ToggleFiltros()
    {
        FiltrosVisiveis = !FiltrosVisiveis;
    }

    private bool CanFiltrar() => StartDate.HasValue || EndDate.HasValue;

    [RelayCommand(CanExecute = nameof(CanFiltrar))]
    private void Filtrar()
    {
        var buscasFiltradas = _todasAsBuscas;

        if (StartDate.HasValue)
        {
            buscasFiltradas = buscasFiltradas.Where(b => b.DataBusca.Date >= StartDate.Value.Date).ToList();
        }

        if (EndDate.HasValue)
        {
            buscasFiltradas = buscasFiltradas.Where(b => b.DataBusca.Date <= EndDate.Value.Date).ToList();
        }

        Buscas = new ObservableCollection<Clima>(buscasFiltradas);
    }

    [RelayCommand]
    private void ClearStartDate()
    {
        StartDate = null;
        Filtrar();
    }

    [RelayCommand]
    private void ClearEndDate()
    {
        EndDate = null;
        Filtrar();
    }

    public void GetIdOpcao()
    {
        IdOpcoesTemp = _opcoesService.GetOpcoesTemp();
    }

    public async Task CarregarDadosUsuario()
    {
        UsuarioLogado = await _loggedUserService.GetUsuarioLogado();
        if (UsuarioLogado?.Buscas != null)
        {
            _todasAsBuscas = UsuarioLogado.Buscas.OrderByDescending(b => b.DataBusca).ToList();
            Buscas = new ObservableCollection<Clima>(_todasAsBuscas);
        }
    }

    public async Task ChecarUsuarioLogado()
    {
        if (UsuarioLogado == null)
        {
            await _dialogService.DisplayAlert("Erro", "Usuário não encontrado. Por favor, faça o login novamente.",
                "OK");
            await Shell.Current.GoToAsync("///Login");
        }
    }
}