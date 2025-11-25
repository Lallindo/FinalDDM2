using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalDDM2.ViewModels;

namespace FinalDDM2.Views;

public partial class Listagem : ContentPage
{
    private ListagemViewModel ViewModel { get; set; }
    
    public Listagem(ListagemViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        Task.Run(ViewModel.CarregarDadosUsuario);
        Task.Run(ViewModel.GetIdOpcao);
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!Task.Run(ViewModel.ExisteUsuarioLogado).Result)
        {
            DisplayActionSheet(
                "Erro ao abrir a página de listagem", 
                null,
                null, 
                null, 
                "Login");
        };
    }
}