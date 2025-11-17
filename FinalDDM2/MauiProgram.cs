using FinalDDM2.Database;
using FinalDDM2.Services;
using FinalDDM2.ViewModels;
using FinalDDM2.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinalDDM2;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        // Views
        builder.Services.AddSingleton<Login>();
        builder.Services.AddSingleton<Registro>();
        builder.Services.AddSingleton<Listagem>();
        
        // ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegistroViewModel>();
        builder.Services.AddTransient<ListagemViewModel>();
        
        // Services
        builder.Services.AddSingleton<IUsuarioService, UsuarioService>();
        builder.Services.AddSingleton<IClimaService, ClimaService>();
        builder.Services.AddSingleton<IApiService, ApiService>();
        
        // Database
        builder.Services.AddDbContext<FinalDbContext>(options => {
            options.UseSqlite($"Data Source={FinalDbContext.GetSqLiteConnection()}");
        });

        return builder.Build();
    }
}