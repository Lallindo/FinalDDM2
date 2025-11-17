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
        BindingContext = viewModel;
    }
}