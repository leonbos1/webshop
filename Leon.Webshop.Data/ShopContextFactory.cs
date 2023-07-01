using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Leon.Webshop.Data
{
    public class ShopContextFactory : IDesignTimeDbContextFactory<ShopContext>
    {
        public ShopContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();

            optionsBuilder.UseSqlServer("Server=localhost,1436;Database=shop;User Id=sa;Password=1Secure*Password1;TrustServerCertificate=true");

            return new ShopContext(optionsBuilder.Options);
        }
    }
}
