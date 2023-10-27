using CarListApp.Maui.Helpers;
using CarListApp.Maui.Models;
using CarListApp.Maui.Views;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarListApp.Maui.ViewModels
{
    public partial class LoadingViewModel : BaseViewModel
    {
        public LoadingViewModel()
        {
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            // get token from internal storage (secure storage, hold token there for future use until its expired)
            var token = await SecureStorage.GetAsync("Token");

            if (string.IsNullOrEmpty(token))
            {
                await GoToLoginPage();
            }
            else
            {
                var jsonToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

                // ValidTo is the end date (expiration date), check the expiration date
                if(jsonToken.ValidTo < DateTime.UtcNow)
                {
                    // remove the token
                    SecureStorage.Remove("Token");

                    // redirect back to the login page
                    await GoToLoginPage();
                }
                else
                {
                    // refresh the user info
                    App.userInfo = new UserInfo()
                    {
                        UserName = jsonToken.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value,    // in this case the email is the same as the username, we could add a custom claim to store the username in the api...
                        Role = jsonToken.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value
                    };

                    // create the menu
                    MenuBuilder.BuildMenu();

                    // redirect to main page
                    await GoToMainPage();
                }
            }
        }

        private async Task GoToMainPage()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }

        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }
}
