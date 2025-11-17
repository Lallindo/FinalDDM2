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
        builder.Services.AddTransient<Login>();
        builder.Services.AddTransient<Registro>();
        builder.Services.AddTransient<Listagem>();
        
        // ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegistroViewModel>();
        builder.Services.AddTransient<ListagemViewModel>();
        
        // Services
        builder.Services.AddTransient<IUsuarioService, UsuarioService>();
        builder.Services.AddTransient<IClimaService, ClimaService>();
        builder.Services.AddTransient<IApiService, ApiService>();
        builder.Services.AddSingleton<ILoggedUserService, LoggedUserService>();
        
        // Database
        builder.Services.AddDbContext<FinalDbContext>(options => {
            options.UseSqlite($"Data Source={FinalDbContext.GetSqLiteConnection()}");
        });

        return builder.Build();
    }
}