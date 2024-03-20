using KropkaNet.Models.Objects.CompanySide;
using KropkaNet.Models.system;

namespace KropkaNet
{
    public class AppSeeder
    {
        private readonly StocktakingContext _context;

        public AppSeeder(StocktakingContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Positions.Any())
                {
                    var positions = GetPositions();
                    _context.Positions.AddRange(positions);
                    _context.SaveChanges();
                }
            }
        }

        public IEnumerable<Position> GetPositions()
        {
            var postions = new List<Position>()
            {
                new Position { Name = "COO"},
                new Position { Name = "Accountant"},
                new Position { Name = "Manager"},
                new Position { Name = "Deputy Manager"},
                new Position { Name = "Decorator"},
                new Position { Name = "Shop Assistant"}
            };

            return postions;
        }
    }
}
