using KropkaNetApi.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using KropkaNet.Objects.Dtos.CompanySide.Employee;
using KropkaNet.Objects.Entities.Enum;
using KropkaNet.Objects.Entities;
using KropkaNet.Objects.Entities.Models.CompanySide;
using KropkaNet.Objects.Dtos.ClientSide.User;
using System.Globalization;
using KropkaNet.Objects.Entities.Models.ClientSide;

namespace KropkaNet.Api.Services.CompanySide
{
    public interface IEmployeeService
    {
        ReturnResult<EmployeeListDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection);
        List<EmployeeListDto> NotInStocktaking(int stocktakingId);
        List<EmployeeListDto> GetFromStocktaking(int stocktakingId, string? sortBy, SortDirection sortDirection);
        EmployeeDto GetById(int employeeId);
        int Update(int id, UpdateEmployeeDto dto);
        void ChangePosition(int employeeId, int positionId);
        int Delete(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly StocktakingContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: get list of employees
        public ReturnResult<EmployeeListDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Employees
               .Include(e => e.Position)
               .Where(e => (string.IsNullOrEmpty(filter) || (
                      e.Name.ToLower().Contains(filter.ToLower()) ||
                      e.Surname.ToLower().Contains(filter.ToLower()) ||
                      e.Email.ToLower().Contains(filter.ToLower()) ||
                      e.Id.ToString().Contains(filter) ||
                      e.Position.Name.ToString().Contains(filter.ToLower()))
                      ));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Employee, object>>>
                {
                    { "id", e => e.Id},
                    { "name", e => e.Name},
                    { "surname", e => e.Surname},
                    { "position", e => e.PositionId}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .Select(p => new EmployeeListDto
                {
                    Id = p.Id,
                    Surname = p.Surname,
                    Name = p.Name,
                    PositionName = p.Position.Name
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<EmployeeListDto>(items, totalCount);

            return result;
        }

        // GET: get list of employees that are not in stocktaking
        public List<EmployeeListDto> NotInStocktaking(int stocktakingId)
        {
            var employees = _context.Employees
               .Where(u => !u.Stocktakings.Any(c => c.Id == stocktakingId))
               .ToList();

            var employeeDtos = _mapper.Map<List<EmployeeListDto>>(employees);

            return employeeDtos;
        }

        // GET: get list of employees from stocktaking
        public List<EmployeeListDto> GetFromStocktaking(int stocktakingId, string? sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Employees
                 .Include(u => u.Stocktakings)
                 .Where(u => u.Stocktakings.Any(o => o.Id == stocktakingId));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Employee, object>>>
                {
                    { "id", u => u.Id},
                    { "name", u => u.Name},
                    { "surname", u => u.Surname}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var employees = baseQuery
                .ToList();

            var employeesDto = _mapper.Map<List<EmployeeListDto>>(employees);
            return employeesDto;
        }
        
        // GET: get list of employees by id - to fix
        public EmployeeDto GetById(int employeeId)
        {
            var employee = _context.Employees
                .Include(e => e.Position)
                .FirstOrDefault(e => e.Id == employeeId);

            if (employee == null) throw new NotFoundException("Employee not found");

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
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

            var account = _context.Accounts.FirstOrDefault(a => a.Id == employee.AccountId);
            
            _context.Remove(account!);
            _context.Remove(employee);
            _context.SaveChanges();

            return 0;
        }
    }
}
