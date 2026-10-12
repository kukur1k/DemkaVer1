using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrenningDemka.Models
{
    public partial class Product
    {
        public int MyTotalQuantity => StockItems.Sum(x => x.Quantity);

        public string QuantityString => (MyTotalQuantity > 5) ? "много" : "мало";

        [NotMapped]
        public decimal NewCost { get; set; } = 0;

        [NotMapped]
        public bool HasDiscount { get; set; } = false;
    }
}
