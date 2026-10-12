using System;
using System.Collections.Generic;

namespace TrenningDemka.Models;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public string? Image { get; set; }

    public string Name { get; set; } = null!;

    public int ManufactureId { get; set; }

    public string? Description { get; set; }

    public string? Structure { get; set; }

    public decimal Cost { get; set; }


    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacture { get; set; } = null!;

    public virtual ICollection<OrdersProduct> OrdersProducts { get; set; } = new List<OrdersProduct>();

    public virtual ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();

    public virtual SubCategory SubCategory { get; set; } = null!;
}
