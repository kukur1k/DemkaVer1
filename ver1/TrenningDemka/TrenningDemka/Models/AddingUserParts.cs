using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrenningDemka.Models
{
    public partial class User
    {
        public string FullName => Id + " | " + Surname + " " + Name  + " " + Patronymic;
    }
}
