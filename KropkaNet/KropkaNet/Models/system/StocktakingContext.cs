using Microsoft.EntityFrameworkCore;
using KropkaNet.Models.Objects.ClientSide;
using KropkaNet.Models.Objects.CompanySide;

namespace KropkaNet.Models.system
{
    public class StocktakingContext : DbContext
    {
        public StocktakingContext(DbContextOptions options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stocktaking> Stocktakings { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseProduct> WarehouseProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WarehouseProduct>()
                 .HasKey(wp => new { wp.WarehouseId, wp.ProductId });


            modelBuilder.Entity<Stocktaking>()
               .HasOne(s => s.Warehouse)
               .WithOne(w => w.Stocktaking)
               .HasForeignKey<Warehouse>(w => w.StocktakingId);

            modelBuilder.Entity<Warehouse>()
                .HasOne(w => w.Stocktaking)
                .WithOne(s => s.Warehouse)
                .HasForeignKey<Stocktaking>(s => s.WarehouseId);



            modelBuilder.Entity<Order>()
               .HasOne(o => o.Stocktaking)
               .WithOne(s => s.Order)
               .HasForeignKey<Stocktaking>(s => s.OrderId);

            modelBuilder.Entity<Stocktaking>()
                .HasOne(s => s.Order)
                .WithOne(o => o.Stocktaking)
                .HasForeignKey<Order>(o => o.StocktakingId);
        }

    }
}
