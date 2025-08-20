using CBA_app.Services;
using CBA_app.ViewModels;
using CBA_app.ViewModels.RegistroVisitantes;
using CBA_app.ViewModels.Startup;
using CBA_app.Views.GIO;
using CBA_app.Views.Login;
using CBA_app.Views.RegistroVisitantes.Listados;
using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;
using ZXing.Net.Maui.Controls;


namespace CBA_app;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()

            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("RobotoRegular-Regular.ttf", "RobotoRegularRegular");
                fonts.AddFont("RobotoRegular-Semibold.ttf", "RobotoRegularSemibold");
                fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                fonts.AddFont("Skriik-Electro-mechanical machine.ttf", "Skriik");
            })
            .UseBarcodeReader();


        //Views
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<VisitantesListadoPage>();
       
        //builder.Services.AddSingleton<GioCargar>();


        //View Models
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddSingleton<ListadoVisitantesViewModel>();
        //builder.Services.AddSingleton<GioViewModel>();


        return builder.Build();
    }
}
