using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class CarDetailsPage : ContentPage
{
	private readonly CarDetailsViewModel _carDetailsViewModel;

	public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = carDetailsViewModel;
        _carDetailsViewModel = carDetailsViewModel;
    }

    // when the page is navigated to..
    //protected override void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    base.OnNavigatedTo(args);
    //}

    // when the page is appearing it means it's already been navigated to
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _carDetailsViewModel.GetCarDetailsAsync();
    }
}