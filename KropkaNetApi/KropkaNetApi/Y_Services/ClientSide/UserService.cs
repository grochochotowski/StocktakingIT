using KropkaNetApi.Exceptions;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.CompanySide.Employee;
using Microsoft.AspNetCore.Identity;
using KropkaNetApi.X_Entities.Objects.Shared;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using KropkaNetApi.X_Models.Shared.Account;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface IUserService
    {
        
    }
    public class UserService : IUserService
    {
        private readonly StocktakingContext _context;
        private readonly IPasswordHasher<Account> _passwordHasher;

        public UserService(StocktakingContext context, IPasswordHasher<Account> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
    }
}
