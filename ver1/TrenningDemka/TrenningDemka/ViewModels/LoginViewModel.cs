using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TrenningDemka.Models;

namespace TrenningDemka.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _main;

        public override string PageTitle => "Авторизация";

        public LoginViewModel(MainWindowViewModel main)
        {
            _main = main;
        }

        //==============Вход=================

        [ObservableProperty] public string _enterLogin = "";
        [ObservableProperty] public string _errorsLogin = "";



        [RelayCommand]
        public void LogIn()
        {
            var valUser = db.Users.Where(u => u.Login == EnterLogin).FirstOrDefault();
            if (valUser != null)
            {
                _main.CurrentUser = valUser;
                _main.GoToData1();
                _main.IsLoginPage = false;
            }
            else
            {
                ErrorsLogin = "User not found";
            }
        }

    }
}
