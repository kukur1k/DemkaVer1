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


        public record SizeDTO(int size, int quantity);
        private void loadSizes()
        {
            var stockItems = db.StockItems
                .Where(si => si.ProductId == _thisProduct.Id)
                .Select(x => new SizeDTO(
                    Convert.ToInt32(x.Size.Size1),
                    x.Quantity)
                ).ToList();

            Sizes = new ObservableCollection<SizeDTO>(stockItems);
        }


        public OrderDetailsViewModel(MainWindowViewModel main, Product product)
        {
            _main = main;
            _thisProduct = product;
            loadSizes();
            
        }




    }
}
