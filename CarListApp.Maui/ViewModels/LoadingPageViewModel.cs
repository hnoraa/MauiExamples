using CarListApp.Maui.Views;

namespace CarListApp.Maui.ViewModels
{
    public partial class LoadingPageViewModel : BaseViewModel
    {
        public LoadingPageViewModel()
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
                // check for expiration
                /*
                if ()
                {
                    await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
                }
                else
                {

                }
                */
                await GoToMainPage();
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
