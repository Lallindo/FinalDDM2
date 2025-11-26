using CommunityToolkit.Maui.Views;
using System;

namespace FinalDDM2.Views.Components;

public partial class ConfigModal : Popup<int>
{
    public int IdTempOpcao { get; private set; }
    
    public ConfigModal(int idTempOpcao)
    {
        IdTempOpcao = idTempOpcao;
        InitializeComponent();

        switch (idTempOpcao)
        {
            case 1:
                FahrenRb.IsChecked = true;
                break;

            case 2:
                KelvinRb.IsChecked = true;
                break;
            
            default:
                CelsiusRb.IsChecked = true;
                break;
        }
    }
    
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(IdTempOpcao);
    }
    
    private void OnColorsRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value && sender is RadioButton radioButton)
        {
            IdTempOpcao = Convert.ToInt32(radioButton.Value);
        }
    }
}