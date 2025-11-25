using CommunityToolkit.Maui.Views; 

namespace FinalDDM2.Views.Components;

public partial class ConfigModal : Popup
{
    public ConfigModal()
    {
        InitializeComponent();
    }
    
    private async void Button_Clicked(object sender, EventArgs e)
    {
        // Força o fechamento a rodar no próximo "frame" da UI
        await Dispatcher.DispatchAsync(async () =>
        {
            try 
            {
                await this.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fechar: {ex.Message}");
                // Se cair aqui, o erro foi engolido e o app não fecha
            }
        });
    }
}