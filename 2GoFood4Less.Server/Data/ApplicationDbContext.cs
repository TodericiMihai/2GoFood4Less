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
    public class ApplicationDbContext : IdentityDbContext<Client>
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

            // ---------- Client → Cart 1:1 ----------
            builder.Entity<Client>()
                .HasOne(c => c.Cart)
                .WithOne(c => c.Client)
                .HasForeignKey<Cart>(c => c.ClientId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade); 

            // ---------- Client -> Order 1:many ----------
            builder.Entity<Client>()
                .HasMany(o => o.Orders)
                .WithOne(c => c.Client)
                .HasForeignKey(c => c.ClientId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------- Cart -> CartItem 1:many ----------
            builder.Entity<Cart>()
                .HasMany(i => i.CartItems)
                .WithOne(c => c.Cart)
                .HasForeignKey(c => c.CartId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // ---------- CartItem -> MenuItem many:1 ---------- 
            builder.Entity<CartItem>()
                .HasOne(m => m.MenuItem)
                .WithMany()
                .HasForeignKey(c => c.MenuItemId)
                .IsRequired();  

            // ---------- Restaurant → Menu 1:many----------
            builder.Entity<Restaurant>()
                .HasMany(m => m.Menus)
                .WithOne(r => r.Restaurant)
                .HasForeignKey(r => r.RestaurantId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // ---------- Menu → MenuItem 1:many ----------
            builder.Entity<Menu>()
                .HasMany(m => m.Items)
                .WithOne(i => i.Menu)
                .HasForeignKey(i => i.MenuId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------- MenuItem → ItemPhoto 1:1 ----------
            builder.Entity<MenuItem>()
                .HasOne(p => p.Photo)
                .WithOne(m => m.MenuItem)
                .HasForeignKey<MenuItemPhoto>(m => m.MenuItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // ---------- Restaurant → Order 1:many ----------
            builder.Entity<Restaurant>()
                .HasMany(o => o.Orders)
                .WithOne(r => r.Restaurant)
                .HasForeignKey(r => r.RestaurantId)
                .IsRequired(false);

            // ---------- Order -> OrderItem 1:many ----------
            builder.Entity<Order>()
                .HasMany(i => i.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId)
                .IsRequired();

            // ---------- OrderItem -> MenuItem many:1 ----------
            builder.Entity<OrderItem>()
                .HasOne(m => m.MenuItem)
                .WithMany()
                .HasForeignKey(c => c.MenuItemId)
                .IsRequired();

            // ---------- Order → Payment 1:1 ----------
            builder.Entity<Order>()
                .HasOne(p => p.Payment)
                .WithOne(o => o.Order)
                .HasForeignKey<Payment>(p => p.OrderId)
                .IsRequired();

            // ---------- Restaurant → RestaurantPhoto 1:1 ----------
            builder.Entity<Restaurant>()
                .HasOne(p => p.Photo)
                .WithOne(r => r.Restaurant)
                .HasForeignKey<RestaurantPhoto>(r => r.RestaurantId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

