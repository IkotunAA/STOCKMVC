using Microsoft.EntityFrameworkCore;
using STOCKMVC.Entities;

namespace STOCKMVC.Context
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = "1", Name = "Admin" },
                new Role { Id = "2", Name = "BusinessOwner" },
                new Role { Id = "3", Name = "Customer" }
            );
        }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
