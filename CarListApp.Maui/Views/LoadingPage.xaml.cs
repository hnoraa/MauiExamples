using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}