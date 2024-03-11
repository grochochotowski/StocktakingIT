using Microsoft.EntityFrameworkCore;
using KropkaNet.Models.Objects;
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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stocktaking> Stocktakings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }



    }
}
