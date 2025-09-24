using Microsoft.AspNetCore.Identity;
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
            builder.Services.AddControllersWithViews();
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
            //Session and Cookies
            builder.Services.AddSession();
            builder.Services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                });
            //For IdentityUser
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
            {
                option.SignIn.RequireConfirmedPhoneNumber = false;
                option.SignIn.RequireConfirmedEmail = true;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 4;
                option.Password.RequiredUniqueChars = 0;

            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserContext>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();
            app.UseCors("AllowAll");
            //Example
            //https://localhost:port/about
            //AboutController
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
