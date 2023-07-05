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

        public async Task<Discount> GetDiscountByGuid(Guid guid)
        {
            return await _context.Discount.FirstOrDefaultAsync(d => d.Id == guid);
        }

        public async Task<List<Discount>> GetDiscounts()
        {
            return await _context.Discount.ToListAsync();
        }

        public async Task<Discount> AddDiscount(Discount discount)
        {
            _context.Discount.Add(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task<Discount> UpdateDiscount(Discount discount)
        {
            _context.Discount.Update(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task DeleteDiscount(Discount discount)
        {
            _context.Discount.Remove(discount);
            await _context.SaveChangesAsync();
        }
    }
}
