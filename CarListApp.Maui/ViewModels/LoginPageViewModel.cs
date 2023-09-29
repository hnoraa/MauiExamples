using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListApp.Maui.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        public LoginPageViewModel()
        {
            
        }

        [RelayCommand]
        public async Task Login()
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayLoginError();
            }
            else
            {
                // call api to attempt login
                var loginSuccessful = false;

                // get response
                if (loginSuccessful)
                {
                    // display welcome message

                    // build a menu based on the user role

                    // navigate to app main page (MainPage)
                }
                else
                {
                    await DisplayLoginError();
                }
            }
        }

        private async Task DisplayLoginError()
        {
            await Shell.Current.DisplayAlert("Invalid Login Attempt", "Invalid User Name or Password", "Ok");
            Password = string.Empty;
        }
    }
}