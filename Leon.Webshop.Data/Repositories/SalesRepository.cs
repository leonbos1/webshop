using Leon.Webshop.Contracts.Models;

namespace Leon.Webshop.Data.Repositories
{
    public class SalesRepository
    {
        private readonly ShopContext _context;

        public SalesRepository(ShopContext context)
        {
            _context = context;
        }

        public void AddSale(Sale sale)
        {
            _context.Sale.Add(sale);
            _context.SaveChanges();
        }
    }
}
