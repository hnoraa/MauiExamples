using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Web;

namespace CarListApp.Maui.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class CarDetailsViewModel: BaseViewModel, IQueryAttributable
    {
        private readonly CarApiService _carApiService;
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        [ObservableProperty]
        Car car;

        [ObservableProperty]
        int id;

        public CarDetailsViewModel(CarApiService carApiService)
        {
            _carApiService = carApiService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(HttpUtility.UrlDecode(query[nameof(Id)].ToString()));
            //Car = App.carDatabaseService.GetCar(id);
            //Car = await _carApiService.GetCarAsync(Id);
            //Car = await _carApiService.GetCarAsync(Id).Result();    // forces syncronicity... May not work
        }

        public async Task GetCarDetailsAsync()
        {
            if (accessType == NetworkAccess.Internet)
            {
                Car = await _carApiService.GetCarAsync(Id);
            }
            else
            {
                Car = App.carDatabaseService.GetCar(id);
            }
        }
    }
}
