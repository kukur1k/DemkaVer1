using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TrenningDemka.Models;

namespace TrenningDemka.ViewModels
{
    public partial class OrderDetailsViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _main;

        [ObservableProperty] public Product _thisProduct;

        public override string PageTitle => "Страница товара";


        public OrderDetailsViewModel(MainWindowViewModel main, Product product)
        {
            _main = main;
            _thisProduct = product;
            
        }




    }
}
