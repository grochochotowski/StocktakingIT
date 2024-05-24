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
        int Create(CreateUserDto dto);
        IEnumerable<UserDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection);
        UserDto GetDetails(int id);
        void Update(int id, UpdateUserDto dto);
        void Delete(int id);
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
