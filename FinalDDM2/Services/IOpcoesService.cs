using FinalDDM2.Models.ValueObjects;

namespace FinalDDM2.Services;

public interface IOpcoesService
{
    public int GetOpcoesTemp();
    public void SetOpcoesTemp(int idOpcao = 0);
}