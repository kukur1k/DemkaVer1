using System;
using System.Collections.Generic;

namespace TrenningDemka.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<OrdersProduct> OrdersProducts { get; set; } = new List<OrdersProduct>();

    public virtual User User { get; set; } = null!;
}
