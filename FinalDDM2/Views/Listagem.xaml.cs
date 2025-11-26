using FinalDDM2.ViewModels;

namespace FinalDDM2.Views;

public partial class Listagem : ContentPage
{
    private readonly ListagemViewModel _viewModel;

    public Listagem(ListagemViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        await _viewModel.CarregarDadosUsuario();
        _viewModel.GetIdOpcao();
        await _viewModel.ChecarUsuarioLogado();
    }
}