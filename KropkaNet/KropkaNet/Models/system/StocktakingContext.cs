using Microsoft.EntityFrameworkCore;
using KropkaNet.Models.Objects;

namespace KropkaNet.Models.system
{
    public class StocktakingContext : DbContext
    {
        public StocktakingContext(DbContextOptions options) : base(options) { }
    }

    
}
