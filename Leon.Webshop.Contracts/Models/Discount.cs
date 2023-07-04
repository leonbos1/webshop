using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Webshop.Contracts.Models
{
    public class Discount
    {
        public Guid Guid { get; set; }

        public int Percentage { get; set; }

        public int Amount { get; set; }
    }
}
