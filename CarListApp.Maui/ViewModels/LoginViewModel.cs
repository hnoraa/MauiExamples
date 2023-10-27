using CarListApp.Maui.Helpers;
using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarListApp.Maui.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        private CarApiService _carApiService;

        public LoginViewModel(CarApiService carApiService)
        {
            _carApiService = carApiService;
        }

        [RelayCommand]
        public async Task Login()
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayLoginMessage("Username or Password is empty");
            }
            else
            {
                // call api to attempt login
                var loginModel = new LoginModel(username, password);
                var response = await _carApiService.Login(loginModel);

                // check for a token
                if (response != null)// (!string.IsNullOrEmpty(response.AccessToken))
                {
                    // store token in secure local storage
                    await SecureStorage.SetAsync("Token", response.AccessToken);

                    // display welcome message
                    await DisplayLoginMessage(_carApiService.StatusMessage);

                    // build a menu based on the user role - determine this from the token
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(response.AccessToken) as JwtSecurityToken;
                    var role = jsonToken.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value;

                    //var userInfo = new UserInfo()
                    //{
                    //    UserName = Username,   // assign the property Username (observable property)
                    //    Role = role             // claims from the token
                    //};

                    // can add the serialized userInfo object to the device Preferences
                    //if(Preferences.ContainsKey(nameof(UserInfo)))
                    //{
                    //    Preferences.Remove(nameof(UserInfo));
                    //}
                    //Preferences.Set(nameof(UserInfo), JsonConvert.SerializeObject(userInfo));

                    // globally accessible App storage that we set in App.xaml.cs
                    App.userInfo = new UserInfo()
                    {
                        UserName = Username,   // assign the property Username (observable property)
                        Role = role            // claims from the token
                    };

                    // navigate to app main page (MainPage)
                    MenuBuilder.BuildMenu();
                    await Shell.Current.GoToAsync($"{nameof(MainPage)}");
                }
                else
                {
                    await DisplayLoginMessage("Invalid Username or Password");
                }
            }
        }

        private async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Login Attempt", message, "Ok");
            Password = string.Empty;
        }
    }
}