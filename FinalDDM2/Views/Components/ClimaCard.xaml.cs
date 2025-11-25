using FinalDDM2.Models;

namespace FinalDDM2.Views.Components;

public partial class ClimaCard : ContentView
{
    public static readonly BindableProperty ClimaDataProperty = BindableProperty.Create(
        nameof(ClimaData),
        typeof(Clima),
        typeof(ClimaCard),
        propertyChanged: OnDadosChanged);

    public Clima ClimaData
    {
        get => (Clima)GetValue(ClimaDataProperty);
        set => SetValue(ClimaDataProperty, value);
    }

    public static readonly BindableProperty UnidadeIdProperty = BindableProperty.Create(
        nameof(UnidadeId),
        typeof(int),
        typeof(ClimaCard),
        defaultValue: 0, // Celsius
        propertyChanged: OnDadosChanged);

    public int UnidadeId
    {
        get => (int)GetValue(UnidadeIdProperty);
        set => SetValue(UnidadeIdProperty, value);
    }

    public ClimaCard()
    {
        InitializeComponent();
    }

    private static void OnDadosChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ClimaCard card)
        {
            card.AtualizarInterface();
        }
    }

    private void AtualizarInterface()
    {
        if (ClimaData == null) return;

        LblCidade.Text = ClimaData.Cidade;
        LblCondicao.Text = ClimaData.CondMetereologicas;
        LblData.Text = ClimaData.DataBusca.ToString("dd/MM/yyyy HH:mm");
        LblLatLon.Text = $"Lat: {ClimaData.Latitude:F4} | Lon: {ClimaData.Longitude:F4}";

        switch (UnidadeId)
        {
            case 1: // Fahrenheit
                LblTemperatura.Text = $"{ClimaData.TempFahrenheit:F1}°F";
                LblSensacao.Text = $"Sensação: {ClimaData.SensacaoTermFahrenheit:F1}°F";
                break;

            case 2: // Kelvin
                LblTemperatura.Text = $"{ClimaData.TempKelvin:F1}K";
                LblSensacao.Text = $"Sensação: {ClimaData.SensacaoTermKelvin:F1}K";
                break;

            default: // Celsius (0)
                LblTemperatura.Text = $"{ClimaData.TempCelsius:F1}°C";
                LblSensacao.Text = $"Sensação: {ClimaData.SensacaoTermCelsius:F1}°C";
                break;
        }
    }
}