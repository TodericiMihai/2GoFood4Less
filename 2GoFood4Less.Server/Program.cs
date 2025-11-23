using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.AuthObjects;
using _2GoFood4Less.Server.Services;
using _2GoFood4Less.Server.Services.MenuServices;
using _2GoFood4Less.Server.Services.RestaurantServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _2GoFood4Less.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddScoped<RestaurantService>();
            builder.Services.AddScoped<MenuService>();
            builder.Services.AddScoped<MenuItemService>();

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            var app = builder.Build();

            // Remove static + default files (you have no frontend)
            // app.UseDefaultFiles();
            // app.UseStaticFiles();
            // app.MapFallbackToFile("index.html");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
