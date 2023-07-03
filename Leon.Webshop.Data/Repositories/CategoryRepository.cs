using Leon.Webshop.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Leon.Webshop.Data.Repositories
{
    public class CategoryRepository
    {
        public readonly ShopContext _context;

        public CategoryRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAll()
        {
            var categories = await _context.Category.ToListAsync();

            return categories;
        }

        public async Task<Category> GetById(Guid id)
        {
            var category = await _context.Category.FindAsync(id);

            return category;
        }
    }
}
