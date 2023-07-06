namespace Leon.Webshop.Contracts.Models
{
    public class Discount
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int Percentage { get; set; }

        public decimal Amount { get; set; }
    }
}
