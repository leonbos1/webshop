using Leon.Webshop.Data;
using Leon.Webshop.Data.Repositories;
using Leon.Webshop.Logic.Helpers;
using Leon.Webshop.Logic.Services;
using Leon.Webshop.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ProductRepository>();

builder.Services.AddScoped<ShoppingCartRepository>();

builder.Services.AddScoped<VisitorRepository>();

builder.Services.AddScoped<CategoryRepository>();

builder.Services.AddScoped<DiscountRepository>();

builder.Services.AddScoped<DiscountProductRepository>();

builder.Services.AddScoped<SalesRepository>();

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddScoped<DiscountService>();

builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<VisitorService>();

builder.Services.AddScoped<SalesService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();