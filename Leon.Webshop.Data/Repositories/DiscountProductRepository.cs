using Leon.Webshop.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Webshop.Data.Repositories
{
    public class DiscountProductRepository
    {
        private readonly ShopContext _context;

        public DiscountProductRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDiscount>> GetAllByProductId(Guid productId)
        {
            var discounts = await _context.ProductDiscount.Where(dp => dp.ProductId == productId).ToListAsync();

            return discounts;
        }

        public async Task<List<ProductDiscount>> GetAll()
        {
            var discounts = await _context.ProductDiscount.ToListAsync();

            return discounts;
        }
    }
}
