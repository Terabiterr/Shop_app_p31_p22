﻿using Microsoft.EntityFrameworkCore;
using Shop_app.Models;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
}
