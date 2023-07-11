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

        public async Task AddSale(Sale sale)
        {
            _context.Sale.Add(sale);

            await _context.SaveChangesAsync();
        }
    }
}
