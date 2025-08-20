using CBA_app.ViewModels.Startup;

namespace CBA_app.Views.Login;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel viewModel)
	{
        InitializeComponent();
        if (viewModel != null)
            this.BindingContext = viewModel;

    }
}