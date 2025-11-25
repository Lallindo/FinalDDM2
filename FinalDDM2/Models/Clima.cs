namespace FinalDDM2.Models;

public class Clima
{
    public int Id { get; set; } = 0;
    public int UsuarioId { get; set; } = 0;
    public Usuario Usuario { get; set; } = new();
    public DateTime DataBusca { get; set; } = DateTime.Now;
    public string Cidade { get; set; } = string.Empty;
    public double TempCelsius { get; set; } = 0;
    public double TempFahrenheit { get => (TempCelsius * 1.8) + 32; }
    public double TempKelvin { get => TempCelsius + 273.15; }
    public double SensacaoTermCelsius { get; set; } = 0;
    public double SensacaoTermFahrenheit { get => (SensacaoTermCelsius * 1.8) + 32; }
    public double SensacaoTermKelvin { get => SensacaoTermCelsius + 273.15; }
    public string CondMetereologicas { get; set; } =  string.Empty;
    public double Longitude { get; set; } = 0;
    public double Latitude { get; set; } = 0;
}