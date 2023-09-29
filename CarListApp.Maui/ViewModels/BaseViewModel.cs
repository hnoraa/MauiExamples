using CommunityToolkit.Mvvm.ComponentModel;

namespace CarListApp.Maui.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(IsNotLoading))]
        private bool _isLoading;   // if the view model is busy doing some action

        public bool IsNotLoading => !_isLoading;
    }
}