using Leon.Webshop.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Leon.Webshop.Data.Repositories
{
    public class ShoppingCartRepository
    {
        private readonly ShopContext _context;

        public ShoppingCartRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<ShoppingCart>> GetAll()
        {
            var shoppingCarts = await _context.ShoppingCart.ToListAsync();

            return shoppingCarts;
        }

        public async Task<ShoppingCart> GetById(int id)
        {
            var shoppingCart = await _context.ShoppingCart.FindAsync(id);

            shoppingCart.Product = await _context.Product.FindAsync(shoppingCart.ProductId);

            return shoppingCart;
        }

        public async Task<List<ShoppingCart>> GetProductsByVisitorId(Guid visitorId)
        {
            var shoppingCarts = await _context.ShoppingCart.Where(x => x.VisitorId == visitorId).ToListAsync();

            foreach (var shoppingCart in shoppingCarts)
            {
                shoppingCart.Product = await _context.Product.FindAsync(shoppingCart.ProductId);
            }

            return shoppingCarts;
        }

        public async Task<List<ShoppingCart>> GetShoppingCartsByVisitorId(Guid visitorId)
        {
            var shoppingCarts = await _context.ShoppingCart.Where(x => x.VisitorId == visitorId).ToListAsync();

            foreach (var shoppingCart in shoppingCarts)
            {
                shoppingCart.Product = await _context.Product.FindAsync(shoppingCart.ProductId);
            }

            return shoppingCarts;
        }

        public async Task<ShoppingCart?> GetByVisitorAndProduct(Visitor visitor, Product product)
        {
            var shoppingCart = await _context.ShoppingCart.FirstOrDefaultAsync(x => x.VisitorId == visitor.Id && x.ProductId == product.Id);

            return shoppingCart;
        }

        public async Task<ShoppingCart> Create(ShoppingCart shoppingCart)
        {
            _context.ShoppingCart.Add(shoppingCart);

            await _context.SaveChangesAsync();

            return shoppingCart;
        }

        public async Task<ShoppingCart> Update(ShoppingCart shoppingCart)
        {
            _context.Entry(shoppingCart).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return shoppingCart;
        }

        public async Task Delete(int id)
        {
            var shoppingCart = await _context.ShoppingCart.FindAsync(id);

            _context.ShoppingCart.Remove(shoppingCart);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(ShoppingCart shoppingCart)
        {
            _context.ShoppingCart.Remove(shoppingCart);

            await _context.SaveChangesAsync();
        }
    }
}
