using KropkaNetApi.Exceptions;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Entities.Objects.Shared;
using KropkaNetApi.X_Models.CompanySide.Employee;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using KropkaNetApi.X_Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using KropkaNetApi.X_Models.Shared.Account;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IEmployeeService
    {
        public ReturnResult<EmployeeListDto> GetList(int page, string filter, string sortBy, SortDirection sortDireciton);
        public ReturnResult<EmployeeListDto> GetById(int employeeId, int page, string filter, string sortBy, SortDirection sortDireciton);
        public int Update(int id, UpdateEmployeeDto dto);
        public void ChangePosition(int employeeId, int positionId);
        public int Delete(int id);
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

        public ReturnResult<EmployeeListDto> GetList(int page, string filter, string sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.Employees
               .Include(e => e.Position)
               .Where(e => (string.IsNullOrEmpty(filter) || (
                      e.Name.ToLower().Contains(filter.ToLower()) ||
                      e.Surname.ToLower().Contains(filter.ToLower()) ||
                      e.Email.ToLower().Contains(filter.ToLower()) ||
                      e.Id.ToString().Contains(filter))
                      ));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Employee, object>>>
                {
                    { "id", e => e.Id},
                    { "Name", e => e.Name},
                    { "Surname", e => e.Surname},
                    { "Position", e => e.Position}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDireciton == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .OrderBy(p => p.Surname)
                .Select(p => new EmployeeListDto
                {
                    Id = p.Id,
                    Surname = p.Surname,
                    Name=p.Name
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<EmployeeListDto>(items, totalCount);

            return result;
        }
        // GET: get list of employees by id
        public ReturnResult<EmployeeListDto> GetById(int employeeId, int page, string filter, string sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.Employees
               .Where(p => p.Id == employeeId);

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                baseQuery = baseQuery.Where(e =>
                       e.Name.ToLower().Contains(filter.ToLower()) ||
                       e.Surname.ToLower().Contains(filter.ToLower()) ||
                       e.Id.ToString().Contains(filter));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Employee, object>>>
                {
                    { "id", c => c.Id},
                    { "Surname", c => c.Surname},
                    { "Name", c => c.Name},
                    { "Position", c => c.Position}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDireciton == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .OrderBy(p => p.Surname)
                .Select(p => new EmployeeListDto
                {
                    Id = p.Id,
                    Surname = p.Surname,
                    Name = p.Name
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<EmployeeListDto>(items, totalCount);

            return result;
        }

        // PUT: update employee
        public int Update(int id, UpdateEmployeeDto dto)
        {
            var employee = _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Account)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null) throw new NotFoundException("Employee not found");

            employee.Name = dto.Name;
            employee.Surname = dto.Surname;
            employee.PersonalNumber = dto.PersonalNumber;
            employee.Email = dto.Email;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.Note = dto.Note;

            _context.SaveChanges();

            return employee.Id;
        }

        //PATCH : change position
        public void ChangePosition(int employeeId, int positionId)
        {
            var employee = _context.Employees.FirstOrDefault(p => p.Id == employeeId);
            if (employee == null) throw new NotFoundException("Employee not found");

            employee.PositionId = positionId;

            _context.SaveChanges();
            
        }

        // DELETE : delete employee with id
        public int Delete(int id)
        {
            var employee = _context.Employees.FirstOrDefault(p => p.Id == id);
            if (employee == null) throw new NotFoundException("Employee not found");

            _context.Remove(employee);
            _context.SaveChanges();

            return 0;
        }
    }
}
