using Microsoft.Maui.Platform;
using CBA_app.Handlers;
using CBA_app.Models;


namespace CBA_app;

public partial class App : Application
{
    public static UsuarioModel UserDetails;
   

    public App()
    {

        InitializeComponent();

        // Establecer la cultura y la información de formato regional
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-ES");
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-ES");


        //Border less entry
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
        {
            if (view is BorderlessEntry)
            {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif __IOS__
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            }
        });
        //Border less Editor
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(BorderlessEditor), (handler, view) =>
        {
            if (view is BorderlessEditor)
            {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#endif
            }
        });
#if ANDROID
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(UnderlineColor), (handler, view) =>
        {
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.White);
        });
#endif
        MainPage = new AppShell();


       
    }
   
   }

