using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalDDM2.ViewModels;

namespace FinalDDM2.Views;

public partial class Login : ContentPage
{
    public Login(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}