using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TrenningDemka.Models;
using static TrenningDemka.ViewModels.OrderDetailsViewModel;

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

        // корзина
        [ObservableProperty]
        public ObservableCollection<OrdersProduct> _busket = new();
        [ObservableProperty]
        public int _busketCount = 0;


        partial void OnCurrentUserChanged(User? oldValue, User? newValue)
        {
            OnPropertyChanged(nameof(Username));
        }


        [RelayCommand]
        public void GoToData1() => CurrentPage = new Data1ViewModel(this);

        [RelayCommand]
        public void GoToBasket() => CurrentPage = new BasketViewModel(this);

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
