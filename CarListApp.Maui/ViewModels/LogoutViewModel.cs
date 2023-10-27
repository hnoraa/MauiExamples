using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.ViewModels
{
    public partial class LogoutViewModel : BaseViewModel
    {
        public LogoutViewModel()
        {
            Logout();
        }

        [RelayCommand]
        private async void Logout()
        {
            // clear
            SecureStorage.Remove("Token");
            App.userInfo = null;

            // navigate
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }
}
