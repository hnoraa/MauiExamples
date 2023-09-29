using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}