using CBA_app.Models;
using CBA_app.ViewModels;

namespace CBA_app;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        this.BindingContext = new AppShellViewModel();

        
    }
}
