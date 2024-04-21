using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.CompanySide;

namespace KropkaNetApi
{
    public class PositionSeeder
    {
        private readonly StocktakingContext _context;

        public PositionSeeder(StocktakingContext context)
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
                new Position { Name = "Employee"},
                new Position { Name = "Moderator"},
                new Position { Name = "Admin"}
            };

            return postions;
        }
    }
}
