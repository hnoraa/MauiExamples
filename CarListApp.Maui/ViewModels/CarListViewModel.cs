using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CarListApp.Maui.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        private const string editButtonText = "Edit Car";

        private const string addButtonText = "Add Car";
        private readonly CarApiService _carApiService;

        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        public ObservableCollection<Car> Cars { get; private set; } = new ();

        public CarListViewModel(CarApiService carApiService)
        {
            Title = "Car List";
            AddEditButtonText = addButtonText;
            _carApiService = carApiService;
            GetCarListAsync().Wait();
        }

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private string _make;

        [ObservableProperty]
        private string _model;

        [ObservableProperty]
        private string _vin;

        [ObservableProperty]
        private string _addEditButtonText;

        [ObservableProperty]
        private int _carId;

        [RelayCommand]
        async Task GetCarListAsync()
        {
            if (IsLoading)
            {
                return;
            }

            try
            {
                IsLoading = true;

                if(Cars.Any())
                {
                    Cars.Clear();
                }

                // get the cars from the data source
                //var cars = App.carService.GetCars();
                //var cars = await _carApiService.GetCarsAsync();
                var cars = new List<Car>();

                if(accessType == NetworkAccess.Internet)
                {
                    cars = await _carApiService.GetCarsAsync();
                } 
                else
                {
                    cars = App.carDatabaseService.GetCars();
                }

                // populate observable collection
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Unable to get cars: {ex.Message}");
                await ShowAlert("Unable to find cars for list");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetCarDetails(int id)
        {
            if(id == 0) return;

            //await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object> { { nameof(Car), car} });

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}");
        }

        [RelayCommand]
        async Task SaveCar()
        {
            if(string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await ShowAlert("Please insert valid data");
                return; 
            }

            Car car = new Car()
            {
                Make = Make,
                Model = Model,
                Vin = Vin
            };

            string txt = "";
            bool result;

            if (CarId != 0)
            {
                car.Id = CarId;
                //result = App.carService.UpdateCar(car);
                //await _carApiService.UpdateCarAsync(car.Id, car);

                if (accessType == NetworkAccess.Internet)
                {
                    await _carApiService.UpdateCarAsync(car.Id, car);
                    txt = _carApiService.StatusMessage;
                    result = true;
                }
                else
                {
                    result = App.carDatabaseService.UpdateCar(car);
                    txt = App.carDatabaseService.StatusMessage;
                }

                CarId = 0;
            }
            else
            {
                //result = App.carService.AddCar(car);
                //await _carApiService.AddCarAsync(car);

                if (accessType == NetworkAccess.Internet)
                {
                    await _carApiService.AddCarAsync(car);
                    result = true;
                }
                else
                {
                    result = App.carDatabaseService.AddCar(car);
                }
            }

            if (result == false)
            {
                await ShowAlert("Please insert valid data");
            }
            else
            {
                await ShowAlert(txt);
                await GetCarListAsync();
                await ClearForm();
            }
        }

        [RelayCommand]
        async Task ToggleEdit(int id)
        {
            if (id == 0)
            {
                await ShowAlert("Please insert valid data");
                return;
            }

            //var result = App.carService.UpdateCar(car);
            CarId = id;
            //var result = App.carService.GetCar(id);
            //var result = await _carApiService.GetCarAsync(id);
            Car result = null;

            if (accessType == NetworkAccess.Internet)
            {
                result = await _carApiService.GetCarAsync(id);
            }
            else
            {
                result = App.carDatabaseService.GetCar(id);
            }

            Make = result.Make;
            Model = result.Model;
            Vin = result.Vin;

            AddEditButtonText = editButtonText;

            if (result == null)
            {
                await ShowAlert("Please insert valid data");
            }
        }

        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if (id == 0)
            {
                await ShowAlert("Please insert valid data");
                return;
            }

            //var result = App.carService.DeleteCar(id);
            //var result = await _carApiService.DeleteCarAsync(id);
            bool result;

            if (accessType == NetworkAccess.Internet)
            {
                result = await _carApiService.DeleteCarAsync(id);
            }
            else
            {
                result = App.carDatabaseService.DeleteCar(id);
            }

            if (result == false)
            {
                await ShowAlert("Please insert valid data");
            }
            else
            {
                await ShowAlert("Car deleted");
                await GetCarListAsync();
            }
        }

        private async Task ShowAlert(string message)
        {
            await Shell.Current.DisplayAlert("Info", message, "Ok");
        }

        [RelayCommand]
        public async Task ClearForm()
        {
            AddEditButtonText = addButtonText;

            Make = "";
            Model = "";
            Vin = "";
        }
    }
}
