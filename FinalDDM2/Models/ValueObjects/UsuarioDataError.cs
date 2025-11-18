namespace FinalDDM2.Models.ValueObjects;

public class UsuarioDataError
{
    public bool NomeIsValid { get; set; } = true;
    public bool SenhaIsValid { get; set; } = true;
    public bool EmailIsValid { get; set; } = true;
    public bool DataNascIsValid { get; set; } = true;
    public bool AllIsValid => NomeIsValid && SenhaIsValid && EmailIsValid && NomeIsValid;
}