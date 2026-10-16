using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using TrenningDemka.Models;

namespace TrenningDemka.ViewModels
{
    public partial class BasketViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _main;

        public override string PageTitle => "My Basket";

        public ObservableCollection<OrdersProduct> Basket => _main.Busket;

        public BasketViewModel(MainWindowViewModel main)
        {
            _main = main;
        }

        [RelayCommand]
        public void CompleteOrder()
        {

            var newOrder = new Order
            {
                UserId = _main.CurrentUser.Id,
                Date = DateOnly.FromDateTime(DateTime.Now)
            };

            foreach (var item in Basket)
            {
                newOrder.OrdersProducts.Add(item);
            }

            foreach (var item in Basket.ToList())
            {
                // Отвязываем объекты (они были нужны для UI) чтобы не сбивать EF
                item.Product = null!;
                item.Size = null!;
            }

            db.Orders.Add(newOrder);
            db.SaveChanges();

            Basket.Clear();
            

            
        }
    }
}
