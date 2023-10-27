using CarListApp.Maui.Models;
using CarListApp.Maui.Services;

namespace CarListApp.Maui;

public partial class App : Application
{
	public static CarDatabaseService carDatabaseService { get; private set; }

	public static UserInfo userInfo { get; set; }

	public App(CarDatabaseService mCarDatabaseService)
	{
		InitializeComponent();

		MainPage = new AppShell();
        carDatabaseService = mCarDatabaseService;
	}
}
