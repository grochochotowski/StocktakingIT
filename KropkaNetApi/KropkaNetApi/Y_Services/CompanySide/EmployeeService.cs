using KropkaNetApi.Exceptions;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Entities.Objects.Shared;
using KropkaNetApi.X_Models.CompanySide.Employee;
using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.X_Models.Shared.Account;
using Microsoft.AspNetCore.Identity;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IEmployeeService
    {
        void Register(RegisterEmployeeDto dto);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly StocktakingContext _context;
        private readonly IPasswordHasher<Account> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public EmployeeService(StocktakingContext context, IPasswordHasher<Account> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }


        public void Register(RegisterEmployeeDto dto)
        {
            var newAccount = new Account()
            {
                Login = dto.Login
            };
            if (dto.Password != dto.ConfirmPassword)
            {
                throw new BadRequestException("Passwords do not match");
            }
            var hashedPassword = _passwordHasher.HashPassword(newAccount, dto.Password);
            newAccount.HashedPassword = hashedPassword;

            _context.Accounts.Add(newAccount);
            _context.SaveChanges();



            var newEmployee = new Employee()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                PersonalNumber = dto.PersonalNumber,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Note = dto.Note,
                PositionId = dto.PositionId,
                AccountId = newAccount.Id
            };

            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
        }

    }
}
