using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using FinalDDM2.Views.Components;

namespace FinalDDM2.Services;

public class PopupService : IPopupService
{
    public async Task AbrirConfiguracoesModal(int idTempOpcao)
    {
        var mainPage = Application.Current?.Windows[0].Page;

        if (mainPage != null)
        {
            var popup = new ConfigModal();
            await mainPage.ShowPopupAsync(popup);
        }
    }

    public async Task FecharConfiguracoesModal()
    {
        var mainPage = Application.Current?.Windows[0].Page;

        if (mainPage != null)
        {
            await mainPage.ClosePopupAsync();
        }
    }
}