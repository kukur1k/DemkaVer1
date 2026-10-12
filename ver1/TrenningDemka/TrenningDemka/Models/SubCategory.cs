using System;
using System.Collections.Generic;

namespace TrenningDemka.Models;

public partial class SubCategory
{
    public int Id { get; set; }

    public string SubCategory1 { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
