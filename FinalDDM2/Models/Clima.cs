namespace FinalDDM2.Models;

public class Clima
{
    private double _tempCelsius = 0;
    private double _sensacaoTermCelsius = 0;
    public int Id { get; set; } = 0;
    public int UsuarioId { get; set; } = 0;
    public Usuario Usuario { get; set; } = new();
    public DateTime DataBusca { get; set; } = DateTime.Now;
    public string Cidade { get; set; } = string.Empty;
    public string TempCelsius { get => $"{_tempCelsius} Cº"; set => _tempCelsius = Convert.ToDouble(value); }
    public string TempFahrenheit { get => $"{(_tempCelsius * 1.8) + 32} Fº"; }
    public string SensacaoTermCelsius { get => $"{_tempCelsius} Cº"; set => _sensacaoTermCelsius = Convert.ToDouble(value); }
    public string SensacaoTermFahrenheit { get => $"{(_sensacaoTermCelsius * 1.8) + 32} Fº"; }
    public string CondMetereologicas { get; set; } =  string.Empty;
    public double Longitude { get; set; } = 0;
    public double Latitude { get; set; } = 0;
}