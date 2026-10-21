using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrenningDemka.Models
{
    public partial class Order
    {
        [NotMapped]
        public DateTimeOffset? newDate {  get; set; } = null;
    }
}
