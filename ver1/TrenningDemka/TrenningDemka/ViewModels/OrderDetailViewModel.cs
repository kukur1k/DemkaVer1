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
    public partial class OrderDetailsViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _main;

        [ObservableProperty] public Product _thisProduct;

        public override string PageTitle => "Страница товара";

        [ObservableProperty] public int count = 0;

        public ObservableCollection<SizeDTO> Sizes { get; set; } = new();

        [ObservableProperty] public SizeDTO? _selectedSize = null;

        [ObservableProperty] public int _selectedCount = 1;


        public record SizeDTO(int size, Product product, int sizeId, int quantity)
        {
            public int quantity {  get; set; } = quantity;
        };
        private void loadSizes()
        {
            var stockItems = db.StockItems
                .Where(si => si.ProductId == _thisProduct.Id)
                .Select(x => new SizeDTO(
                    Convert.ToInt32(x.Size.Size1),
                    x.Product,
                    x.SizeId,
                    x.Quantity)
                ).ToList();

            Sizes = new ObservableCollection<SizeDTO>(stockItems);
        }


        [RelayCommand]
        public void MakeOrder()
        {
            var data = new OrdersProduct();
            data.ProductId = _thisProduct.Id;
            data.SizeId = SelectedSize.sizeId;
            data.Quantity = SelectedCount;
            data.Product = _thisProduct;  // продукт запоминаем чтоб в корзине вывести карточки, а при сохранении обнулим чтобы не сбивать EF
            _main.Busket.Add(data);

            _main.BusketCount+=SelectedCount;
            SelectedSize.quantity-=SelectedCount;

        }


        public OrderDetailsViewModel(MainWindowViewModel main, Product product)
        {
            _main = main;
            _thisProduct = product;
            loadSizes();
        }




    }
}
