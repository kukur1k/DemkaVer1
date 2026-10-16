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
    public partial class Data1ViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _main;

        public override string PageTitle => "Каталог товаров";



        public Data1ViewModel(MainWindowViewModel main)
        {
            _main = main;
            LoadProducts();
            ProcessNewCost();
        }

        [ObservableProperty]
        public ObservableCollection<Product> _products = new ObservableCollection<Product>();



        [RelayCommand]
        public void LoadProducts()
        {
            var productsList = db.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacture)
                .Include(p => p.StockItems)
                .Include(p => p.OrdersProducts)
                .ToList();

            Products = new ObservableCollection<Product>(productsList);

            var categories = db.Categories.Select(x => x.Category1).ToList();
            Categories = new ObservableCollection<string>(categories);
        }

        public void ProcessNewCost()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var endDate = new DateOnly(today.Year, today.Month, 1);
            var startDate = endDate.AddMonths(-1);


            var products = db.Products
                .Where(p => p.OrdersProducts.Any(op =>
                    op.Order.Date < endDate && op.Order.Date >= startDate)
                ).ToList();

            foreach (var product in Products)
            {
                if (!products.Contains(product))
                {
                    product.NewCost = product.Cost - ((product.Cost / 100) * 25);
                    product.HasDiscount = true;
                }
                else
                {
                    product.NewCost = product.Cost;
                }

            }


        }

        //==============Вход=================

        [ObservableProperty] public string _enterLogin = "";
        [ObservableProperty] public string _errorsLogin = "";

        [ObservableProperty] public User? _currentUser = null;


        [RelayCommand]
        public void LogIn()
        {
            var valUser = db.Users.Where(u => u.Login == EnterLogin).FirstOrDefault();
            if (valUser != null)
            {
                CurrentUser = valUser;
            }
            else
            {
                ErrorsLogin = "Пользователь не найден";
            }
        }


        //=================Сортировка================

        public ObservableCollection<string> SortedTypes { get; } = new()
        {
            "По возрастанию", "По убыванию"
        };

        [ObservableProperty] public string _selectedOrder = "";
        [ObservableProperty] public string _searchStr = "";

        [ObservableProperty] public ObservableCollection<String> _categories = new();

        [ObservableProperty] public string _selectedCategory = "";

        partial void OnSelectedCategoryChanged(string value)
        {
            AllSorted();
        }

        partial void OnSelectedOrderChanged(string value)
        {
            AllSorted();
        }

        partial void OnSearchStrChanged(string value)
        {
            AllSorted();
        }

        [RelayCommand]
        public void CleanFilters()
        {
            LoadProducts();
            SelectedCategory = "";
            SelectedOrder = "";
            SearchStr = "";
        }
        


        public void AllSorted()
        {
            var productsList = db.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacture)
                .Include(p => p.StockItems)
                .ToList();

            if (!String.IsNullOrEmpty(SelectedOrder))
            {
                if (SelectedOrder == "По возрастанию")
                {
                    productsList = productsList.OrderBy(x => x.NewCost).ToList();
                }
                if (SelectedOrder == "По убыванию")
                {
                    productsList = productsList.OrderByDescending(x => x.NewCost).ToList();
                }
            }

            if (!String.IsNullOrEmpty(SearchStr))
            {
                productsList = productsList.Where(x => x.Name.Contains(SearchStr) || x.Description.Contains(SearchStr)).ToList();
                
            }

            if (!String.IsNullOrEmpty(SelectedCategory))
            {
                productsList = productsList.Where(x => x.Category.Category1.Contains(SelectedCategory)).ToList();

            }

            Products = new ObservableCollection<Product>(productsList);
        }


        // ==============выбор товара в заказ============
        [RelayCommand]
        public void GoToOrder(Product product)
        {
            _main.GoToOrderDetail(product);
        }
    }
}
