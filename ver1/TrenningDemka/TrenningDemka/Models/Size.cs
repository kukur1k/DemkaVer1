using System;
using System.Collections.Generic;

namespace TrenningDemka.Models;

public partial class Size
{
    public int Id { get; set; }

    public decimal Size1 { get; set; }

    public virtual ICollection<OrdersProduct> OrdersProducts { get; set; } = new List<OrdersProduct>();

    public virtual ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
}
