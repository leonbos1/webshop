using Leon.Webshop.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Leon.Webshop.Data.Repositories
{
    public class ProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _context.Product.ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetAllByCategoryId(Guid categoryId)
        {
            var products = await _context.Product.Where(p => p.CategoryId == categoryId).ToListAsync();

            return products;
        }

        public async Task<Product> GetById(Guid id)
        {
            var product = await _context.Product.FindAsync(id);

            product.Category = await _context.Category.FindAsync(product.CategoryId);

            return product;
        }

        public async Task<Product> Create(Product product)
        {
            _context.Product.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task Delete(int id)
        {
            var product = await _context.Product.FindAsync(id);

            _context.Product.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
}
