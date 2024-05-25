using KropkaNetApi.Exceptions;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Entities.Objects.Shared;
using KropkaNetApi.X_Models.CompanySide.Employee;
using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.X_Models.Shared.Account;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IEmployeeService
    {
        public int Create(CreateEmployeeDto dto);


    }
    public class EmployeeService : IEmployeeService
    {
        private readonly StocktakingContext _context;
        private readonly IPasswordHasher<Account> _passwordHasher;
        private readonly IMapper _mapper;

        public EmployeeService(StocktakingContext context, IPasswordHasher<Account> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public int Create(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);

            var position = new Position()
            {
                Name = dto.Name
            };

            _context.Positions.Add(position);
            _context.SaveChanges();

            employee.PositionId=position.Id;

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee.Id;
        }

        public ReturnResult<Employee>
    }
}
