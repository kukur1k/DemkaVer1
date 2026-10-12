using System;
using System.Collections.Generic;

namespace TrenningDemka.Models;

public partial class OrdersProduct
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int SizeId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}
