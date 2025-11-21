using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Models.User.UserObjects;
using _2GoFood4Less.Server.Models.UserObjects;
using _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection.Emit;

namespace _2GoFood4Less.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       
        // --- Cart Related Entities
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<CartItem> CartItems { get; set; } = default!;

        // --- Restaurant Related Entities  ---
        public DbSet<Restaurant> Restaurants { get; set; } = default!;
        public DbSet<Menu> Menus { get; set; } = default!;
        public DbSet<MenuItem> MenuItems { get; set; } = default!;

        // --- Order Related Entities  ---
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;

        // --- Utility/Photo Entities  ---
        public DbSet<Photo> Photos { get; set; } = default!;
        public DbSet<MenuItemPhoto> MenuItemPhotos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            // ---------- User → Cart 1:1 ----------
            builder.Entity<User>()
                .HasOne(c => c.Cart)
                .WithOne()
                .HasForeignKey<Cart>("UserId")
                .IsRequired(false);

            // ---------- Cart -> CartItem 1:many ----------
            builder.Entity<Cart>()
                .HasMany(i => i.CartItems)
                .WithOne(c => c.Cart)
                .HasForeignKey(c => c.CartId)
                .IsRequired();

            // ---------- CartItem -> MenuItem many:1 ----------
            builder.Entity<CartItem>()
                .HasOne(m => m.MenuItem)
                .WithMany()
                .HasForeignKey(c => c.MenuItemId)
                .IsRequired();

            // ---------- Cart → Payment 1:1 ----------
            builder.Entity<Cart>()
                .HasOne(p => p.Payment)
                .WithOne()
                .HasForeignKey<Payment>("Payment")
                .IsRequired(false);

            // ---------- Restaurant → Menu 1:many----------
            builder.Entity<Restaurant>()
                .HasMany(m => m.Menus)
                .WithOne(r => r.Restaurant)
                .HasForeignKey(m => m.RestaurantId)
                .IsRequired(false);

            // ---------- Menu → MenuItem 1:many ----------
            builder.Entity<Menu>()
                .HasMany(m => m.Items)
                .WithOne(mi => mi.Menu)
                .HasForeignKey(mi => mi.MenuId)
                .IsRequired(false);

            // ---------- MenuItem → ItemPhoto 1:1 ----------
            builder.Entity<MenuItem>()
                .HasOne(p => p.Photo)
                .WithOne()
                .HasForeignKey<MenuItemPhoto>("MenuItemId")
                .IsRequired();

            // ---------- Restaurant → Order 1:many ----------
            builder.Entity<Restaurant>()
                .HasMany(o => o.Orders)
                .WithOne()
                .HasForeignKey("RestaurantId")
                .IsRequired();

            // ---------- Oder -> OrderItem 1:many ----------
            builder.Entity<Order>()
                .HasMany(i => i.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId)
                .IsRequired();


        }
    }
}

