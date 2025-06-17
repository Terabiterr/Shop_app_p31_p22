using Microsoft.EntityFrameworkCore;
using Shop_app.Models;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions options) : base(options)
    {
    }
    DbSet<Product> Products { get; set; }
}
