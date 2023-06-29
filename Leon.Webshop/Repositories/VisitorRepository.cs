using Leon.Webshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Leon.Webshop.Repositories
{
    public class VisitorRepository
    {
        ShopContext _context;

        public VisitorRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task Add(Visitor visitor)
        {
            await _context.Visitor.AddAsync(visitor);

            await _context.SaveChangesAsync();
        }

        public async Task<Visitor> GetBySessionId(string sessionId)
        {
            return await _context.Visitor.FirstOrDefaultAsync(v => v.SessionId == sessionId);
        }

        public async Task<Visitor> GetById(Guid id)
        {
            return await _context.Visitor.FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
