using Avalonia.Controls;
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
    public partial class OrdersViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _main;

        public override string PageTitle => "Orders list"; 

        public OrdersViewModel(MainWindowViewModel main)
        {
            _main = main;
            LoadOrders();
        }

        [ObservableProperty] ObservableCollection<Order> _orderDetails = new();


        [RelayCommand]
        public void DeleteOrder(Order order)
        {

            var op = _main.db.OrdersProducts.Where(x => x.OrderId == order.Id).ToList();
            _main.db.OrdersProducts.RemoveRange(op);

            _main.db.Orders.Remove(order);
            _main.db.SaveChanges();
            LoadOrders();
            
        }

        [RelayCommand]
        public void DeleteOrdersItem(OrdersProduct item)
        {
            var order = _main.db.Orders.Where(x => x.Id == item.OrderId).FirstOrDefault();
            _main.db.OrdersProducts.Remove(item);

            _main.db.SaveChanges();
            LoadOrders();

            var count = _main.db.OrdersProducts.Count(x => x.OrderId == order.Id);
            if (count == 0)
            {
                _main.db.Orders.Remove(order);
            }

            _main.db.SaveChanges();
            LoadOrders();
        }


        [RelayCommand]
        public void EditDate(Order order)
        {
            if (order.newDate != null)
            {
                order.Date = DateOnly.FromDateTime(order.newDate.Value.DateTime);
            }
            _main.db.SaveChanges();
            LoadOrders();
            
        }

       
        private void LoadOrders()
        {
            var orders = _main.db.Orders
                .Include(o => o.User)
                .Include(o => o.OrdersProducts)
                    .ThenInclude(op => op.Product).ThenInclude(p => p.Manufacture)
                .ToList();
                    

            OrderDetails = new ObservableCollection<Order>(orders);

        }    


    }
}
