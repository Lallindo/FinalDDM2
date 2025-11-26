namespace FinalDDM2.Services;

public interface IPopupService
{
    Task<object?> AbrirConfiguracoesModal(int idTempOpcao);
}