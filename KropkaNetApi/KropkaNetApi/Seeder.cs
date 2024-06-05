using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Entities.Objects.Shared;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace KropkaNetApi
{
    public class Seeder
    {
        private readonly StocktakingContext _context;
        private readonly IPasswordHasher<Account> _passwordHasher;

        public Seeder(StocktakingContext context, IPasswordHasher<Account> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
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
                if(_context.Accounts.FirstOrDefault(a => a.Login == "root") == null)
                {
                    var account = GetAccount();
                    _context.Accounts.Add(account);
                    _context.SaveChanges();

                    var employee = GetEmployee(account.Id);
                    _context.Employees.Add(employee);
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
        public Account GetAccount()
        {
            var account = new Account
            {
                Login = "ROOT"
            };
            var hashedPassword = _passwordHasher.HashPassword(account, "root");
            account.HashedPassword = hashedPassword;

            return account;
        }
        public Employee GetEmployee(int id)
        {
            var employee = new Employee
            {
                Name = "ROOT",
                Surname = "",
                Email = "",
                PhoneNumber = "",
                Note = "",
                PositionId = 6,
                AccountId = id
            };
            
            return employee;
        }
    }
}
