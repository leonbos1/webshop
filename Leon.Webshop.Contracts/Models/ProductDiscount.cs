using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Webshop.Contracts.Models
{
    public class ProductDiscount
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid DiscountId { get; set; }

        public Product? Product { get; set; }

        public Discount? Discount { get; set; }
    }
}
