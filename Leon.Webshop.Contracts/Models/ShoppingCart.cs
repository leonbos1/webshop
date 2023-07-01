namespace Leon.Webshop.Contracts.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }

        public Guid VisitorId { get; set; }

        public Visitor Visitor { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}