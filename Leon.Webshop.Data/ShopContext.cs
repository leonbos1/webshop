namespace Leon.Webshop.Data;

using Leon.Webshop.Contracts.Models;
using Microsoft.EntityFrameworkCore;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {
    }

    // DbSet properties for your database tables
    public DbSet<Product> Product { get; set; }

    public DbSet<Category> Category { get; set; }

    public DbSet<ShoppingCart> ShoppingCart { get; set; }

    public DbSet<Visitor> Visitor { get; set; }

    public DbSet<Discount> Discount { get; set; }

    public DbSet<ProductDiscount> ProductDiscount { get; set; }

    public DbSet<Sale> Sale { get; set; }
}
