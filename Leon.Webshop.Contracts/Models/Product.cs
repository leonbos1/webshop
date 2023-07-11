using System.ComponentModel.DataAnnotations.Schema;

namespace Leon.Webshop.Contracts.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal FinalPrice = 0;

        public string? Description { get; set; }

        public Guid? CategoryId { get; set; }

        public Category? Category { get; set; }

        public int Stock { get; set; }

        public List<ProductDiscount> discounts { get; set; } = new List<ProductDiscount>();
    }
}
