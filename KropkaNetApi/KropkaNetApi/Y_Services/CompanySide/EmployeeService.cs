using KropkaNetApi.Exceptions;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Entities.Objects.Shared;
using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.X_Models.Shared.Account;
using Microsoft.AspNetCore.Identity;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IEmployeeService
    {
        
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly StocktakingContext _context;
        private readonly IPasswordHasher<Account> _passwordHasher;

        public EmployeeService(StocktakingContext context, IPasswordHasher<Account> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public int Create()
    }
}
