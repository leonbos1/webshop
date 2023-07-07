using Leon.Webshop.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Webshop.Data.Repositories
{
    public class DiscountRepository
    {
        private readonly ShopContext _context;

        public DiscountRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<Discount>> GetAll()
        {
            var discounts = await _context.Discount.ToListAsync();

            return discounts;
        }

        public async Task<Discount> GetById(Guid id)
        {
            var discount = await _context.Discount.FindAsync(id);

            return discount;
        }
    }
}
