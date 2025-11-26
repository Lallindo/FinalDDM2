using CommunityToolkit.Maui.Extensions;
using FinalDDM2.Views.Components;

namespace FinalDDM2.Services;

public class PopupService : IPopupService
{
    public async Task<object?> AbrirConfiguracoesModal(int idTempOpcao)
    {
        var mainPage = Application.Current?.MainPage;

        if (mainPage == null) return null;
        
        var popup = new ConfigModal(idTempOpcao);
        return (int)(await mainPage.ShowPopupAsync<int>(popup)).Result;
    }
}