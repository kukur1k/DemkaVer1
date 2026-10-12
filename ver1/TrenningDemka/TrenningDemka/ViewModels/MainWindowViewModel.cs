using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrenningDemka.Models;

namespace TrenningDemka.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private ViewModelBase _currentPage;
        [ObservableProperty] private bool _isLoginPage = false;

        [ObservableProperty] public User? _currentUser = null;
        public string Username => CurrentUser == null
            ? "Гость"
            : CurrentUser.Surname + " " + CurrentUser.Name;


        partial void OnCurrentUserChanged(User? oldValue, User? newValue)
        {
            OnPropertyChanged(nameof(Username));
        }


        [RelayCommand]
        public void GoToData1() => CurrentPage = new Data1ViewModel(this);

        [RelayCommand]
        public void GoToOrderDetail(Product product) => CurrentPage = new OrderDetailsViewModel(this, product);

        [RelayCommand]
        public void GoToLogIn()
        {
            CurrentPage = new LoginViewModel(this);
            IsLoginPage = true;
        }

    }
}
