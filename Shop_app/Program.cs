using Microsoft.EntityFrameworkCore;
using Shop_app.DbContext;
using Shop_app.Services;

namespace Shop_app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IServiceProducts, ServiceProducts>();
            //For Shop
            builder.Services.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //For Identity
            builder.Services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            var app = builder.Build();

            app.Run();
        }
    }
}
