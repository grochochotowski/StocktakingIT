using Microsoft.EntityFrameworkCore;

namespace KropkaNet.Models.system
{
    public class StocktakingContext : DbContext
    {
        public StocktakingContext(DbContextOptions options) : base(options) { }
    }
}
