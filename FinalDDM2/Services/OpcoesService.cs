using FinalDDM2.Models.ValueObjects;

namespace FinalDDM2.Services;

public class OpcoesService : IOpcoesService
{
    private int _idOpcoesTemp = 0;
    
    public int GetOpcoesTemp()
    {
        return _idOpcoesTemp;
    }

    public void SetOpcoesTemp(int idOpcao = 0)
    {
        _idOpcoesTemp = idOpcao;
    }
}