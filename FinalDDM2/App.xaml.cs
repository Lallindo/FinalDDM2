using FinalDDM2.Services;

namespace FinalDDM2;

public partial class App : Application
{
    private IUsuarioService _usuarioService { get; set; }
    
    public App(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell(_usuarioService));
    }
}